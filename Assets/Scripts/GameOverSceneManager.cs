using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour {

	public Text winner;
	private SpriteRenderer sr;
	public Sprite bg;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		AddBG();
	}
	void Start () {
		
		int leftScore = GameDataManager.instance.leftScore;
		int rightScore = GameDataManager.instance.rightScore;
		SpriteRenderer sr = GetComponent<SpriteRenderer>();


		if (leftScore == rightScore) {
			winner.text = "TIE";
			sr.material.color = Color.gray;
		}
		else if (rightScore < leftScore) {
			winner.text = "BLUE WINS!";
			sr.material.color = Color.blue;
		} else {
			winner.text = "RED WINS!";
			sr.material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach (Rewired.Player p in ReInput.players.GetPlayers()) {
			
			if ( p.id == 0 && p.GetButtonDown("Start") ) {
				
				// Back to choose teams scene
				SceneManager.LoadScene("ChooseTeam");
			}
		}
	}

	private void AddBG () {

		sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sortingLayerName = "Background";
		sr.sprite = bg;

		transform.localScale = new Vector3(1,1,1);
     
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 0f);
	}
}
