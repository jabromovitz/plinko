using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

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

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		HandMan.pegPulled.AddListener(RemovePeg);
	}
	
	void Start () {
		Goal.OnGoalScored += GoalScored;
		StartCoroutine(MoveBallDropper());
		
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
