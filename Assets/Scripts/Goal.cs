using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Use this for initialization
	public Globals.Team team;
	private int teamNo;

	public delegate void OnGoalScoredDelegate(Globals.Team teamNo);
	public static OnGoalScoredDelegate OnGoalScored;
	void Start () {

		teamNo = (int) team;
		GetComponent<SpriteRenderer>().color = Globals.TEAM_COLORS[teamNo];	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Puck") {
			Ball ball = other.GetComponent<Ball>();
			if(team != ball.team && ball.team != Globals.Team.NONE) {
				OnGoalScored(ball.team);
			}
		}
	}
}
