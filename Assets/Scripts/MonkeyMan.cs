using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMan : MonoBehaviour {

	private bool isFalling = true;
	private bool isSlingshot = false;
	private HandMan leftHand;
	private HandMan rightHand;
	private Transform leftArm;
	private Transform rightArm;
	private Rigidbody2D rb;
	private Vector2 springPos;
	// Use this for initialization
	private Vector2 lastAxises = Vector2.zero;
	private float lastSpeed = 0;
	public Globals.Team teamId;
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		teamId = this.transform.parent.GetComponent<Player>().teamId;

		// new way
		foreach (Transform child in transform) {
			if (child.name == "Left Arm") {
				leftArm = child.transform;
				foreach (Transform subChild in child) {
					if (subChild.CompareTag("Hand")) {
						leftHand = subChild.GetComponent<HandMan>();
						leftHand.teamId = teamId;
					}
				}
			} else if (child.name == "Right Arm") {
				rightArm = child.transform;
				foreach (Transform subChild in child) {
					if (subChild.CompareTag("Hand")) {
						rightHand = subChild.GetComponent<HandMan>();
						rightHand.teamId = teamId;
					}
				}
			}
		}

		foreach(Transform child in transform) {
    		if(child.tag == "TeamHitter")
        		child.gameObject.layer = LayerMask.NameToLayer( Globals.TEAM_NAMES[(int)teamId] );
		}

	}
	
	// Update is called once per frame
	void Update () {

		//Fall Slower
		isFalling = !leftHand.IsHolding() && !rightHand.IsHolding() && rb.velocity.y < 0;
		rb.drag = isFalling ? 400 : 0;

	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Puck") {
			Ball ball = other.gameObject.GetComponent<Ball>();
			ball.SetTeam(teamId);
		}
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if (leftHand.IsHolding() && rightHand.IsHolding()) {
			
			if (!isSlingshot) {

			// Middle of pegs
			Vector2 left = leftHand.GetHandPosition();
			Vector2 right = rightHand.GetHandPosition();
			springPos = new Vector2 (0.5f * (left.x + right.x), 0.5f * (left.y + right.y));

			//disable arms
			// leftArm.gameObject.SetActive(false);
			// rightArm.gameObject.SetActive(false);

			isSlingshot = true;
		
			}

			//Add catapult force
			var horiz = Input.GetAxis("Horizontal Left");
			var vert = Input.GetAxis("Vertical Left");
			Vector2 axises = new Vector2(horiz, vert);
			GetComponent<Rigidbody2D>().AddForce( axises * 150);
			
		} else if (isSlingshot){

			// leftArm.gameObject.SetActive(true);
			// rightArm.gameObject.SetActive(true);

			isSlingshot = false;

		}
	}

	//Find Closest Peg to Hand
	
}
