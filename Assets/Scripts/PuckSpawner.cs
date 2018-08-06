using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckSpawner : MonoBehaviour {

	public GameObject puckPreb;
	private Vector3 origPuckPos;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		InvokeRepeating("DropPuck", 0, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetButtonDown("Fire1")) {
		// 	puck.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		// 	puck.GetComponent<Rigidbody2D>().angularVelocity = 0;
		// 	puck.transform.position = origPuckPos;

		// 	Vector3 mousePosition = Input.mousePosition;
 
        // 	//Convert the mousePosition according to World position
        // 	Vector3 targetPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
 
        // 	//Set the position of targetObject
        // 	puck.transform.position = new Vector3(targetPosition.x, origPuckPos.y, 0);
		// }
	}

	void DropPuck () {
		Vector3 pos = new Vector3(Random.Range(-10f, 10f), 20f, 1f);
		GameObject peg = (GameObject) Instantiate(puckPreb, pos, Quaternion.identity);


	}
}
