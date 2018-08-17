using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour {

	public Text winner;
	// Use this for initialization
	void Start () {
		
		int leftScore = GameDataManager.instance.leftScore;
		int rightScore = GameDataManager.instance.rightScore;

		if (leftScore == rightScore)
			winner.text = "TIE";
		else if (leftScore > rightScore)
			winner.text = "BLUE WINS!";
		else
			winner.text = "RED WINS!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
