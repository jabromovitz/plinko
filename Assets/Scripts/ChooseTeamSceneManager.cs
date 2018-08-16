using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public class ChooseTeamSceneManager : MonoBehaviour {

	public RectTransform leftTeam;
	public RectTransform rightTeam;
	public Text pressStart;
	private bool canStartGame = false;
	public List<Image> cons;
	private Coroutine startFlashCo;
	private List<Dictionary<Globals.Team, Vector3>> controlPositions = new List<Dictionary<Globals.Team, Vector3>>();
	private List<Globals.Team> controlPosition = new List<Globals.Team>();

	private int conConnected = 0;
	List<Rewired.Player> players = new List<Rewired.Player>();
	
	
	void Awake()
	{
		 ReInput.ControllerConnectedEvent += OnControllerConnected;
	}
	void Start () {

		// Populate controller positions
		for (int i = 0; i < cons.Count; i++) {

			RectTransform tempRect = cons[i].rectTransform;
			Dictionary<Globals.Team, Vector3> conPos = new Dictionary<Globals.Team, Vector3>();
			
			conPos.Add(Globals.Team.LEFT, new Vector3(leftTeam.position.x +  0.5f * leftTeam.rect.width, tempRect.position.y, 0f));
			conPos.Add(Globals.Team.NONE, new Vector3(tempRect.position.x, tempRect.position.y, 0f));
			conPos.Add(Globals.Team.RIGHT, new Vector3(rightTeam.position.x - 0.5f * rightTeam.rect.width, tempRect.position.y, 0f));

			controlPositions.Add(conPos);
		}

		// Disable Press Start text
		pressStart.enabled = false;
		
	}

	private void OnControllerConnected(ControllerStatusChangedEventArgs args) {
			
		Image tmp = cons[conConnected];
 		Color c = tmp.color;
		c.a = 1.0f;
		tmp.color = c;
		conConnected++;
		controlPosition.Add(Globals.Team.NONE);

	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (Rewired.Player p in ReInput.players.GetPlayers()) {
			
			if ( p.id == 0 && p.GetButtonDown("Start") && canStartGame ) {
				
				// Start Game
				GameDataManager.instance.playerTeams = controlPosition;
				SceneManager.LoadScene("Game");
			}

			if(p.GetButtonDown("D-Left")) {

				if(controlPosition[p.id] == Globals.Team.RIGHT) {
					controlPosition[p.id] = Globals.Team.NONE;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.NONE] , Quaternion.identity);

				} else if(controlPosition[p.id] == Globals.Team.NONE && CanSelectTeam(Globals.Team.LEFT)) {
					controlPosition[p.id] = Globals.Team.LEFT;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.LEFT] , Quaternion.identity);
				}
				CanStartGameCheck();

			} else if (p.GetButtonDown("D-Right")) {

				if(controlPosition[p.id] == Globals.Team.LEFT) {
					controlPosition[p.id] = Globals.Team.NONE;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.NONE] , Quaternion.identity);

				} else if(controlPosition[p.id] == Globals.Team.NONE && CanSelectTeam(Globals.Team.RIGHT)) {
					controlPosition[p.id] = Globals.Team.RIGHT;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.RIGHT] , Quaternion.identity);
				}
				CanStartGameCheck();
			}
		}
	}

	private bool CanSelectTeam (Globals.Team team) {

		// Can't have more than half players on one team
		float halfPlayers = 0.5f * controlPosition.Count;
		int teamCount = 0;

		foreach (Globals.Team t in controlPosition) {
			if (t == team) 
				teamCount++;
		}

		return teamCount < halfPlayers;
	}

	private void CanStartGameCheck () {

		// If there is at least one person per team
		// we can start a game

		bool leftTeam = false;
		bool rightTeam = false;

		foreach (Globals.Team t in controlPosition) {
			if (t == Globals.Team.LEFT) 
				leftTeam = true;
			else if (t == Globals.Team.RIGHT) 
				rightTeam = true;
		}

		if (leftTeam && rightTeam) {
			canStartGame = true;
			startFlashCo = StartCoroutine(PressStartBlink());
		} else {
			canStartGame = false;
			if (startFlashCo != null) {
				StopCoroutine(startFlashCo);
				pressStart.enabled = false;
			}
		}

	}

	IEnumerator PressStartBlink() 
	{
		while(true) {
			pressStart.enabled = true;
			yield return new WaitForSeconds(0.75f);
			pressStart.enabled = false;
			yield return new WaitForSeconds(0.75f);
		}
	}
}
