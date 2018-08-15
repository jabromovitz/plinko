using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Use this for initialization
	public Globals.Team team;
	private int teamNo;

	public delegate void OnGoalScoredDelegate(Globals.Team teamNo);
	public static OnGoalScoredDelegate OnGoalScored;
	private Color normalColor;
	private Color scoreColor;
	private SpriteRenderer rend;
	void Start () {

		teamNo = (int) team;
		normalColor = Globals.TEAM_COLORS[teamNo];	
		GetComponent<SpriteRenderer>().color = normalColor;
		rend = this.GetComponent<SpriteRenderer>();
		scoreColor = Color.white;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Puck") {
			Ball ball = other.GetComponent<Ball>();
			if(team == ball.team && ball.team != Globals.Team.NONE) {
				OnGoalScored(ball.team);
				StartCoroutine( Flasher() );
			}
		}
	}

	IEnumerator Flasher() 
         {
             for (int i = 0; i < 2; i++) {
				 
              rend.color = scoreColor;
              yield return new WaitForSeconds(0.1f);
              rend.color = normalColor;
              yield return new WaitForSeconds(0.1f);
             }
          }
}
