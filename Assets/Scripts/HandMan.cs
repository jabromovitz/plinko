using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class HandMan : MonoBehaviour {

	 public ArmGenerator.Arm hand;
	 private int handSpeed = 150;
	 private float seekMult = 5.0f;
	 private string hAxis = "Horizontal ";
	 private string vAxis = "Vertical ";
	 private string grab = "Grab ";
	 private bool isHoldingPeg = false;
	 private bool seekPeg = false;
	 private Vector3 pegTarget;
	 private Vector3 origScale;
	 private Vector3 seekScale;
	 public Rewired.Player player;
	 public Globals.Team teamId;
	// Use this for initialization
	void Start () {

		hAxis += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		vAxis += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		grab += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		origScale = transform.localScale;
		seekScale = transform.localScale * 1.5f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		var horiz = player.GetAxis(hAxis);
     	var vert = player.GetAxis(vAxis);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(horiz, vert) * handSpeed);

		//Grow Shrink Hand

	}

	void Update () {

		if(isHoldingPeg && !player.GetButton(grab)) {
			isHoldingPeg = false;
			Destroy(this.gameObject.GetComponent<HingeJoint2D>());
		} else if (!isHoldingPeg && player.GetButtonDown(grab)) {
			StartPegSeek(FindCloestPeg());
		} else if (!isHoldingPeg && ! player.GetButton(grab)) {
			StopPegSeek();
		}

		if (seekPeg) {
 
        	Vector3 dir = (pegTarget - transform.position).normalized;
			GetComponent<Rigidbody2D>().AddForce(dir * seekMult * handSpeed);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Peg")) {
			if (!isHoldingPeg && player.GetButton(grab)) {
				isHoldingPeg = true;
				HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>() as HingeJoint2D;
				hinge.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
			}
		} 
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Puck") {
			Ball ball = other.gameObject.GetComponent<Ball>();
			ball.SetTeam(teamId);
		}
	}

	public bool IsHolding() {
		return isHoldingPeg;
	}

	public Vector2 GetHandPosition () {
		return gameObject.transform.position;
	}

	Vector3 FindCloestPeg() {
		while (true) {
			float closestPegDist = float.MaxValue;;
			GameObject closestPeg = null;

			var hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 2);

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
			return closestPeg.transform.position;
		}
	}

	public void StartPegSeek (Vector3 target) {
		seekPeg = true;
		pegTarget = target;
		transform.localScale = seekScale;
	}

	public void StopPegSeek () {
		seekPeg = false;
		transform.localScale = origScale;
	}
}
