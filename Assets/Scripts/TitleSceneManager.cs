using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour {
	
	public Text pressStart;
	public Text title;
	private Color origTitleColor;
	private Color flashTitleColor = Color.cyan;
	void Start()
	{
		
		origTitleColor = title.color;
		StartCoroutine(TitleFlasher());
		StartCoroutine(PressStartBlink());
	}

	void Update () {

		foreach (Rewired.Player p in ReInput.players.GetPlayers()) {
			
			if ( p.id == 0 && p.GetButtonDown("Start") ) {
			
				// Back to choose teams scene
				SceneManager.LoadScene("ChooseTeam");
			}
		}
	}

	IEnumerator PressStartBlink() 
	{
		while(true) {
			pressStart.enabled = true;
			yield return new WaitForSeconds(0.75f);
			pressStart.enabled = false;
			yield return new WaitForSeconds(0.75f);
		}
	}

	IEnumerator TitleFlasher() 
	{
		while(true) {
			
			title.color = flashTitleColor;
			yield return new WaitForSeconds(0.1f);
			title.color = origTitleColor;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
