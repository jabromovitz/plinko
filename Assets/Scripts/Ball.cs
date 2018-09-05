using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	// Use this for initialization
	public Globals.Team team = Globals.Team.NONE;
	SpriteRenderer sr;
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	

	public void SetTeam(Globals.Team playerTeam) {
		team = playerTeam;

		if(team == Globals.Team.NONE)
			sr.color = Color.white;
		if(team == Globals.Team.LEFT)
			sr.color = Color.blue;
		if(team == Globals.Team.RIGHT)
			sr.color= Color.red;

	}
}
