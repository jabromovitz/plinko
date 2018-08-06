using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Player : MonoBehaviour {

	// Use this for initialization
	public int playerID;
	public Globals.Team teamId;
	private Rewired.Player player;

	void Start()
	{
		foreach (HandMan hand in GetComponentsInChildren<HandMan>()) {
				player = ReInput.players.GetPlayer(playerID);
				hand.player = player;
		}
	}
}
