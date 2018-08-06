using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmGenerator : MonoBehaviour {

	// Use this for initialization
	private List<Color> randColors= new List<Color> {Color.red, Color.green, Color.yellow, Color.blue, Color.grey,
													Color.magenta, Color.cyan, Color.black};
	public enum Arm { Left, Right };
	public Arm arm;
	public Rigidbody2D subArmPreFab;
	public Rigidbody2D handPreFab;
	private List <Rigidbody2D> subArms = new List<Rigidbody2D>();
	public int links;
	public float armLength;
	void Awake () {

		float linkLength = armLength / links;
		float y = linkLength * subArmPreFab.transform.localScale.y;
		for (int i = 0; i < links; i++) {
			Vector3 pos = new Vector3(0f, -y * i, 0f);
			Rigidbody2D subArm = (Rigidbody2D) Instantiate(subArmPreFab, pos, Quaternion.identity);
			Vector3 size = subArm.transform.localScale;
			//Testing for arm collisions
			subArm.transform.localScale = new Vector3(size.x, linkLength * size.y, 0f);
			subArm.transform.parent = this.transform;
			// subArm.GetComponent<Rigidbody2D>().useAutoMass = false;
			// subArm.GetComponent<Rigidbody2D>().mass = 0.1f;			
			// subArm.GetComponent<Renderer> ().material.color = randColors[Random.Range(0, randColors.Count-1)];

			//Attach hinge joint
			HingeJoint2D hinge = subArm.GetComponent<HingeJoint2D>();
			if (i == 0) {
				//Attach to body
				hinge.connectedBody = transform.parent.GetComponent<Rigidbody2D>();
				float xAnchor = Arm.Left == arm ? -0.5f : 0.5f;
				hinge.connectedAnchor = new Vector2(xAnchor, 0);
			} else if (i < links) {
				//Attach to prev subarm
				hinge.connectedBody = subArms[i-1].transform.GetComponent<Rigidbody2D>();
			} 

			if(i == links - 1) {
				//Also attach hand
				pos = new Vector3(0f, -y * (i+0.5f), 0f);
				Rigidbody2D hand = (Rigidbody2D) Instantiate(handPreFab, pos, Quaternion.identity);
				HandMan script = hand.GetComponent<HandMan>();
				script.hand = arm;
				hand.transform.parent = this.transform;
				FixedJoint2D handHinge = hand.gameObject.AddComponent<FixedJoint2D>();
				handHinge.connectedBody = subArm.GetComponent<Rigidbody2D>();
				handHinge.autoConfigureConnectedAnchor = false;
			}

			subArms.Add(subArm);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
