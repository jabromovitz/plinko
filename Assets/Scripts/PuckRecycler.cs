using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckRecycler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Puck") {
			Destroy(other.gameObject);
		} else if (other.gameObject.tag == "Body") {
			StartCoroutine( RelaunchCharacter(0.2f, other.GetComponent<Rigidbody2D>()));
		}
	}

	IEnumerator RelaunchCharacter(float launchDuration, Rigidbody2D rb) {
	
		rb.isKinematic = true;
		rb.velocity = 10 * Vector2.up;
		yield return new WaitForSeconds(launchDuration);

		rb.isKinematic = false;
		rb.velocity = Vector2.zero;
	}
}
