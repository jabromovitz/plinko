using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	public Text leftScoreText;
	public Text rightScoreText;
	private int leftScore = 0;
	private int rightScore = 0;
	//Defining Delegate
	public delegate void OnGoalScoredDelegate();
	public static OnGoalScoredDelegate OnGoalScored;
	void Start () {
		Goal.OnGoalScored += GoalScored;
	}
	
	// Update is called once per frame
	void Update () {
		
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

	public void RemovePeg (GameObject peg) {
		StartCoroutine(RemovePegCo(peg));
	}
	IEnumerator RemovePegCo (GameObject peg) {
	
		peg.SetActive(false);
		yield return new WaitForSeconds(5.0f);
		peg.SetActive(true);
	}

}
