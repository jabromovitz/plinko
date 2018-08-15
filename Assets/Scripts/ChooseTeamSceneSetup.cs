using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class ChooseTeamSceneSetup : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer sr;
	public Sprite bg;	
	void Start () {

		AddBG();
	}

	private void AddBG () {

		sr = gameObject.AddComponent<SpriteRenderer>();
		sr.color = new Color(0.3f, 0.7f, 0.1f, 1.0f);
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
