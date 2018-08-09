using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDropper : MonoBehaviour {

	public GameObject puckPrefab;	
	private float ballDropperSpeed = 0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(MoveBallDropper());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator MoveBallDropper () {
		while (true) {
			while (Globals.MAX_BALL_DROPPER_SPEED > ballDropperSpeed) {
				ballDropperSpeed += 0.2f;
				yield return null;
			}
			
			yield return new WaitForSeconds(Globals.BALL_DROP_FREQ);

			while (0 <= ballDropperSpeed) {
				ballDropperSpeed -= 0.2f;
				yield return null;
			}

			//Stop dropper from drifting;
			ballDropperSpeed = 0f;

			yield return StartCoroutine( DropPucks() );

			yield return new WaitForSeconds(Globals.BALL_DROPPER_WAIT_TIME);
		}
	}
	IEnumerator DropPucks () {

		for (int i = 0; i < Globals.BALLS_PER_DROP; i++) {
			Vector3 pos = new Vector3(Random.Range(transform.position.x - 2f,
		 							transform.position.x + 2f), 10f, 1f);
			GameObject peg = (GameObject) Instantiate(puckPrefab, pos, Quaternion.identity);
			
			yield return new WaitForSeconds(Globals.TIME_BETWEEN_BALL_DROPS);
		}
	}
}
