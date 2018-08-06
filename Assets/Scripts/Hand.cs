using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	private int speed = 25;
	public KeyCode key;
	private Vector3 pegTarget;
	bool seekPeg = false;
	bool isHoldingPeg = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (seekPeg) {
 
        	Vector3 dir = (pegTarget - transform.position).normalized * speed;
			GetComponent<Rigidbody2D>().velocity = dir;
		}
		
	}
	

	public void StartPegSeek (Vector3 target) {
		seekPeg = true;
		pegTarget = target;
	}

	public void StopPegSeek () {
		seekPeg = false;
		if(isHoldingPeg) {
			Destroy(this.gameObject.GetComponent<HingeJoint2D>());
			isHoldingPeg = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Peg") {
			isHoldingPeg = true;
			seekPeg = false;
			HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>() as HingeJoint2D;
			hinge.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
		} 
	}
}
