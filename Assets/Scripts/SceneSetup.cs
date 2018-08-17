using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class SceneSetup : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer sr;
	public Sprite bg;
	public GameObject pegPreFab;
	private Rewired.Player player0;
	private Rewired.Player player1;

	
	void Start () {

		AddBG();
		SetupPegs();
	}

	private void AddBG () {

		sr = gameObject.AddComponent<SpriteRenderer>();
		//sr.color = new Color(0.3f, 0.7f, 0.1f, 1.0f);
		sr.sortingLayerName = "Background";
		sr.sprite = bg;

		transform.localScale = new Vector3(1,1,1);
     
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		
		transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 0f);
	}

	private void SetupPegs () {

		float wH = Camera.main.orthographicSize;
		float wW = wH / Screen.height * Screen.width;

		int horizPegs = 12;
		int vertPegs = 6;
		float hSpacing = wW * 1/(horizPegs+1.0f);
		float vSpacing = wH * 1/(vertPegs+1.0f);

		for(int i = 1; i <= vertPegs; i++) {
			int shiftPeg = i % 2 == 0 ? 0 : 1;
			for(int j = 1; j <= horizPegs; j++) {
				Vector3 pos = new Vector3(-0.5f * wW - (0.5f * shiftPeg * hSpacing) + j * hSpacing, -0.5f * wH + i * vSpacing, 0f);
				GameObject peg = (GameObject) Instantiate(pegPreFab, Vector3.zero, Quaternion.identity);
				peg.transform.parent = transform;
				peg.transform.localPosition = pos;
			}	
		}

	}

	
}
