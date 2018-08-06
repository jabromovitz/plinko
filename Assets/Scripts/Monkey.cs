using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour {

	// Use this for initialization
	public KeyCode leftKey;
	public KeyCode rightKey;
	public Hand leftHand;
	public Hand rightHand;
	private Coroutine leftCo = null;
	private Coroutine rightCo = null;

	void Start () {
	
	}

	IEnumerator FindCloestPeg(KeyCode hand) {
		while (true) {
			float closestPegDist = float.MaxValue;;
			GameObject closestPeg = null;

			var hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1);

    		for (var i = 0; i < hitColliders.Length; i++) {
        		if (hitColliders[i].gameObject.tag == "Peg") {
				
					float dist = Vector2.Distance( hitColliders[i].gameObject.transform.position ,
									  transform.position);
					if (dist < closestPegDist) {
						closestPegDist = dist;
						closestPeg = hitColliders[i].gameObject;
					}
				}
    		}

			if (hand == leftKey) {
				leftHand.StartPegSeek(closestPeg.transform.position);
			} else if (hand == rightKey) {
				rightHand.StartPegSeek(closestPeg.transform.position);
			}

			yield return new WaitForSeconds(1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(leftKey)) {
			leftCo = StartCoroutine (FindCloestPeg(leftKey));
		} else if (Input.GetKeyDown(rightKey)) {
			rightCo = StartCoroutine (FindCloestPeg(rightKey));
		} else if (Input.GetKeyUp(leftKey)) {
			StopCoroutine (leftCo);
			leftHand.StopPegSeek();
		} else if (Input.GetKeyUp(rightKey)) {
			StopCoroutine (rightCo);
			rightHand.StopPegSeek();
		}
	}
}
