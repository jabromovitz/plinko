using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

	// Use this for initialization
	public enum ArmColor { Left, Right };
	public ArmColor arm;
	private Transform hand;
	private List<Transform> subArms = new List<Transform>();
	private LineRenderer lineRenderer;
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		Color clr = arm == ArmColor.Left ? Color.blue : Color.red;
		lineRenderer.startColor = clr;
		lineRenderer.endColor = clr;
		lineRenderer.positionCount = transform.childCount;
		foreach (Transform child in transform) {
			if(child.CompareTag("Hand")) {
				hand = child;
			} else if (child.CompareTag("SubArm")) {
				subArms.Add(child);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//From Shoulder
		//lineRenderer.SetPosition(0, transform.position);
		//lineRenderer.SetPosition(1, subArms[0].position);

		int cnt = lineRenderer.positionCount;
		for (int i = 0; i < cnt - 2; i++) {
			
			lineRenderer.SetPosition(i, subArms[i].GetChild(0).position);
			lineRenderer.SetPosition(i + 1, subArms[i + 1].GetChild(0).position);
		}

		lineRenderer.SetPosition(cnt - 2, subArms[cnt - 2].GetChild(0).position );
		lineRenderer.SetPosition(cnt - 1, hand.position);
		
	}
}
