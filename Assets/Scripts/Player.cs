﻿using System.Collections;
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
		// foreach (HandMan hand in GetComponentsInChildren<HandMan>()) {
		// 		player = ReInput.players.GetPlayer(playerID);
		// 		hand.player = player;
		// }
	}

	public void Init (int playerId, Globals.Team teamId, Sprite bodySprite) {
		
		this.playerID = playerId;
		this.teamId = teamId;

		foreach (HandMan hand in GetComponentsInChildren<HandMan>()) {
				player = ReInput.players.GetPlayer(playerID);
				hand.player = player;
		}

		foreach (Transform child in transform) {
			if (child.tag == "Body") {
				child.GetComponent<SpriteRenderer>().sprite = bodySprite;
			} 
		}

	}
}
