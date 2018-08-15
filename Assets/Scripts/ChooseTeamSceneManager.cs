using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ChooseTeamSceneManager : MonoBehaviour {

	public RectTransform leftTeam;
	public RectTransform rightTeam;
	public List<Image> cons;
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
			
			if(p.GetButtonDown("D-Left")) {
				if(controlPosition[p.id] == Globals.Team.RIGHT) {
					controlPosition[p.id] = Globals.Team.NONE;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.NONE] , Quaternion.identity);

				} else if(controlPosition[p.id] == Globals.Team.NONE && CanSelectTeam(Globals.Team.LEFT)) {
					controlPosition[p.id] = Globals.Team.LEFT;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.LEFT] , Quaternion.identity);
				}
			} else if (p.GetButtonDown("D-Right")) {
				if(controlPosition[p.id] == Globals.Team.LEFT) {
					controlPosition[p.id] = Globals.Team.NONE;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.NONE] , Quaternion.identity);

				} else if(controlPosition[p.id] == Globals.Team.NONE) {
					controlPosition[p.id] = Globals.Team.RIGHT;
					cons[p.id].rectTransform.SetPositionAndRotation(controlPositions[p.id][Globals.Team.RIGHT] , Quaternion.identity);
				}
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
}
