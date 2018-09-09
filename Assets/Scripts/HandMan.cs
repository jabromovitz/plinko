using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Rewired;

public class HandMan : MonoBehaviour {

	[System.Serializable]
	public class OnPegPulled : UnityEvent<GameObject> { };
	 public ArmGenerator.Arm hand;
	 private int handSpeed = 150;
	 private float seekMult = 5.0f;
	 private string hAxis = "Horizontal ";
	 private string vAxis = "Vertical ";
	 private string grab = "Grab ";
	 private string pull = "Pull ";
	 private bool isHoldingPeg = false;
	 private bool seekPeg = false;
	 private Vector3 pegTarget;
	 private Vector3 origScale;
	 private Vector3 seekScale;
	 public Rewired.Player player;
	 public Globals.Team teamId;
	 private Coroutine pullPegCo;
	 public static OnPegPulled pegPulled = new OnPegPulled();
	 private GameObject heldPeg;

	 public Sprite openHand;
	 public Sprite closedHand;
	 
	// Use this for initialization
	void Start () {

		// Are we the left or right hand?
		hAxis += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		vAxis += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		grab += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		pull += ArmGenerator.Arm.Left == hand ? "Left" : "Right";

		// Scale hand when seeking/holding a peg
		origScale = transform.localScale;
		seekScale = transform.localScale * 1.5f;

		// Set layer for hitting opponent team
		foreach(Transform child in transform) {
    		if(child.tag == "TeamHitter")
        		child.gameObject.layer = LayerMask.NameToLayer( Globals.TEAM_NAMES[(int)teamId] );
		}

		// Add open hand sprite to renderer
		GetComponent<SpriteRenderer>().sprite = openHand;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		var horiz = player.GetAxis(hAxis);
     	var vert = player.GetAxis(vAxis);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(horiz, vert) * handSpeed);
	}

	void Update () {

		// Grab peg
		if(isHoldingPeg && player.GetButtonUp(grab)) {
			isHoldingPeg = false;
			Destroy(this.gameObject.GetComponent<HingeJoint2D>());
			// Open hand sprite
			GetComponent<SpriteRenderer>().sprite = openHand;;
		} else if (!isHoldingPeg && player.GetButtonDown(grab)) {
			StartPegSeek(FindCloestPeg());
		} else if (!isHoldingPeg && player.GetButtonUp(grab)) {
			StopPegSeek();
		}

		// Pull peg
		if (isHoldingPeg) {
			if (player.GetButtonDown(pull)) {
				pullPegCo = StartCoroutine( PullPeg() );
			}
			if (player.GetButtonUp(pull)) {
				StopCoroutine(pullPegCo);
			}
		}

		if (seekPeg) {
 
        	Vector3 dir = (pegTarget - transform.position).normalized;
			GetComponent<Rigidbody2D>().AddForce(dir * seekMult * handSpeed);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		// Grab a peg
		if (other.gameObject.CompareTag("Peg")) {
			if (!isHoldingPeg && player.GetButton(grab)) {
				StopPegSeek();
				isHoldingPeg = true;
				HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>() as HingeJoint2D;
				heldPeg = other.gameObject;
				hinge.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
				
				// Close hand sprite
				GetComponent<SpriteRenderer>().sprite = closedHand;
			}
		} 
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		// Turn ball your teams color
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
		//while (true) {
			float closestPegDist = float.MaxValue;
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
		//}
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

	IEnumerator PullPeg () {

		yield return new WaitForSeconds (Globals.PEG_PULL_TIME);

		isHoldingPeg = false;
		Destroy(this.gameObject.GetComponent<HingeJoint2D>());
		pegPulled.Invoke(heldPeg);
	}
}
