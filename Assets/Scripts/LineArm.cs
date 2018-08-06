using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineArm : MonoBehaviour {


	public ArmGenerator.Arm hand;
	private int handSpeed = 10;
	private string hAxis = "Horizontal ";
	private string vAxis = "Vertical ";
	private float minDist;
	private float maxDist;
	private DistanceJoint2D distanceJoint;
	// Use this for initialization
	void Start () {
		hAxis += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		vAxis += ArmGenerator.Arm.Left == hand ? "Left" : "Right";
		distanceJoint = GetComponent<DistanceJoint2D>();
		minDist = distanceJoint.distance;
		maxDist = 2 * minDist;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		var horiz = Input.GetAxis(hAxis);
     	var vert = Input.GetAxis(vAxis);
		float angleRange = 15;

		//Grow Shrink Hand
		// Hand
		float handAngle = Mathf.Sign(transform.localPosition.y/transform.localPosition.magnitude);
		// Input
		Vector2 inputVec = new Vector2(horiz, vert);
		float inputMag = inputVec.magnitude;
		float inputAngle;

		
		if (inputMag >= 0.9) {
			inputAngle = Mathf.Asin(inputVec.y / inputVec.magnitude);
			float angleBetween = Vector2.Angle(transform.localPosition, inputVec);
			if(angleBetween < angleRange && distanceJoint.distance < maxDist) {
				// Grow
				 distanceJoint.distance = Mathf.Lerp(distanceJoint.distance, maxDist, Time.deltaTime * 5);
			} 
		} else if (distanceJoint.distance > minDist) {
			// Shrink
			distanceJoint.distance = Mathf.Lerp(distanceJoint.distance, minDist, Time.deltaTime * 5);
		}
	}
}
