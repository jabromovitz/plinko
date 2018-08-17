using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {


	public GameObject playerPrefab;
	public List<Sprite> faceSprites;
	public Text countdownText;
	public Text clock;
	public const int BALLS_PER_DROP = 50;
	public const float BALL_DROP_FREQ = 1.0f;
	public GameObject puckPrefab;
	public Text leftScoreText;
	public Text rightScoreText;
	private int leftScore = 0;
	private int rightScore = 0;
	//Defining Delegate
	public delegate void OnGoalScoredDelegate();
	public static OnGoalScoredDelegate OnGoalScored;
	public GameObject ballDropper;
	private float ballDropperSpeed = 0f;
	private const float MAX_BALL_DROPPER_SPEED = 30;
	private const float BALL_DROPPER_WAIT_TIME = 3.0f;
	private int gameTime = 1 * 60;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		HandMan.pegPulled.AddListener(RemovePeg);
		countdownText.enabled = false;
	}
	
	void Start () {

		clock.text = string.Format("{0:0}:{1:00}", Mathf.Floor(gameTime/60), gameTime % 60);
		StartCoroutine( Countdown() );
		Goal.OnGoalScored += GoalScored;
		//StartCoroutine(MoveBallDropper());
		
	}

	IEnumerator ClockTick () {

		while(gameTime > 0) {

			yield return new WaitForSeconds(1.0f);
			gameTime--;
			clock.text = string.Format("{0:0}:{1:00}", Mathf.Floor(gameTime/60), gameTime % 60);
		}

		StartCoroutine(GameOver());
	}

	IEnumerator GameOver () {

		// Game over text
		Vector3 origTextScale = countdownText.rectTransform.localScale;
		countdownText.text = "GAME OVER";
		countdownText.enabled = true;
		yield return StartCoroutine(Helpers.ScaleText(countdownText, 1f, 3f, 0.75f));
		yield return new WaitForSeconds(0.75f);
		countdownText.enabled = false;
		countdownText.rectTransform.localScale = origTextScale;

		// To Game Over scene	
		GameDataManager.instance.leftScore = leftScore;
		GameDataManager.instance.rightScore = rightScore;
		SceneManager.LoadScene("GameOver");
	}
	IEnumerator Countdown () {
		
		Vector3 origTextScale = countdownText.rectTransform.localScale;
		yield return new WaitForSeconds(1.0f);

		for (int i = 3; i >= 1; i--) {

			countdownText.text = i.ToString();
			countdownText.enabled = true;
			yield return StartCoroutine(Helpers.ScaleText(countdownText, 1f, 3f, 0.3f));
			yield return new WaitForSeconds(1f);
			countdownText.enabled = false;
			countdownText.rectTransform.localScale = origTextScale;
		}

		//Start!
		countdownText.text = "GO!";
		countdownText.enabled = true;
		yield return StartCoroutine(Helpers.ScaleText(countdownText, 1f, 3f, 0.75f));
		yield return new WaitForSeconds(0.75f);
		countdownText.enabled = false;
		countdownText.rectTransform.localScale = origTextScale;

		// Start game stuff, prob move to its on method
		//CreatePlayers();
		//StartCoroutine(MoveBallDropper());
		StartCoroutine(ClockTick());

	}

	private void CreatePlayers () {

		// Create Players
		List<Globals.Team> teams = GameDataManager.instance.playerTeams;
		for (int i = 0; i < teams.Count; i++) {
			
			if (teams[i] != Globals.Team.NONE) {
				GameObject player = 
					(GameObject) Instantiate(playerPrefab,
											new Vector3(Random.Range(-10f, 10f), 0f, 0f),
											Quaternion.identity);
				Player playerScript = player.GetComponent<Player>();
				playerScript.Init(i, teams[i], faceSprites[(int)teams[i]]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		ballDropper.transform.Translate(ballDropperSpeed * Vector3.right * Time.deltaTime);
		if(ballDropper.transform.position.x > 20f) {
			ballDropper.transform.position = new Vector3(-20f, 10f, 0f);
		}
	}

	private void GoalScored (Globals.Team team) {
		switch (team) {		
			case Globals.Team.LEFT:
				leftScore++;
				leftScoreText.text = leftScore.ToString();
				break;
			case Globals.Team.RIGHT:
				rightScore++;
				rightScoreText.text = rightScore.ToString();
				break;
		}
	}

	//Remove peg for set amount of time
	public void RemovePeg (GameObject peg) {
		StartCoroutine(RemovePegCo(peg));
	}
	IEnumerator RemovePegCo (GameObject peg) {
	
		peg.SetActive(false);
		yield return new WaitForSeconds(Globals.PEG_REGEN_TIME);
		peg.SetActive(true);
	}

	IEnumerator MoveBallDropper () {
		while (true) {
			while (MAX_BALL_DROPPER_SPEED > ballDropperSpeed) {
				ballDropperSpeed += 0.2f;
				yield return null;
			}
			
			yield return new WaitForSeconds(BALL_DROP_FREQ);

			while (0 <= ballDropperSpeed) {
				ballDropperSpeed -= 0.2f;
				yield return null;
			}

			//Stop dropper from drifting;
			ballDropperSpeed = 0f;

			yield return StartCoroutine( DropPucks() );

			yield return new WaitForSeconds(BALL_DROPPER_WAIT_TIME);
		}
	}

	
	IEnumerator DropPucks () {

		for (int i = 0; i < BALLS_PER_DROP; i++) {
			Vector3 pos = new Vector3(Random.Range(ballDropper.transform.position.x - 2f,
		 							ballDropper.transform.position.x + 2f), 10f, 1f);
			GameObject peg = (GameObject) Instantiate(puckPrefab, pos, Quaternion.identity);
			
			yield return new WaitForSeconds(0.3f);
		}
	}

}
