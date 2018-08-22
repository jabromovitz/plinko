using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSetup : MonoBehaviour {

	public Sprite bg;	
	// Use this for initialization
	void Start () {
		
		AddBG();
	}
	
	private void AddBG () {

		SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
		sr.color = new Color(0.31f, 0.31f, 0.31f, 1.0f);
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
