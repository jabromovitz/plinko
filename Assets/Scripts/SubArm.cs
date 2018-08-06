using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 public class SubArm : MonoBehaviour
 {
 
     private Transform myTransform;
	 private Vector3 origScale;
     private Vector2 origPos;
	 private Vector3 stretchScale;
	 public float targetY;
	 private Renderer renderer;
     private Vector3 velocity;
 
     public bool useLerp;
     public Vector3 target;
	private Vector3 newTarget;

     public float smoothTime;
     public float lerpSpeed;
	 private bool grow = false;
     // Use this for initialization
     void Start()
     {
		renderer = GetComponent<Renderer>();
        
        origPos = transform.position;
        origScale = transform.localScale;
        float area = origScale.x * origScale.y;
        float targetX = area / targetY;
        stretchScale = new Vector3(targetX, targetY, 0f);
     }
 
     // Update is called once per frame
     void Update()
     {
		// newTarget = grow ? stretchScale : origScale;
		// float preScaleY = renderer.bounds.max.y;

        //  if (useLerp)
        //  {
        //      transform.localScale = Vector3.Lerp(transform.localScale, newTarget, Time.deltaTime * lerpSpeed);
        //  }
        //  else
        //  {
        //      transform.localScale = Vector3.SmoothDamp(transform.localScale, newTarget, ref velocity, smoothTime);
        //  }

		//  float postScaleY = renderer.bounds.max.y;
		//   //calculate difference
 		//  float diff = postScaleY - preScaleY;
 
 		//  //move the bar to the left
 		//  transform.Translate(new Vector2(0f, -diff));
 
     }

     /// <summary>
     /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
     /// </summary>
     void FixedUpdate()
     {
         Vector2 pos = GetComponent<Rigidbody2D>().position;
         Vector2 diff = pos - origPos;
         GetComponent<Rigidbody2D>().MovePosition(new Vector2( pos.x, origPos.y + diff.y));

     }

     IEnumerator ChangeSize(Transform hand = null) {
        
        newTarget = grow ? stretchScale : origScale;

        while( Mathf.Abs(newTarget.y - transform.localScale.y) > 0) {
		float preScaleY = renderer.bounds.max.y;

         if (useLerp)
         {
             transform.localScale = Vector3.Lerp(transform.localScale, newTarget, Time.deltaTime * lerpSpeed);
         }
         else
         {
             transform.localScale = Vector3.SmoothDamp(transform.localScale, newTarget, ref velocity, smoothTime);
         }

		 float postScaleY = renderer.bounds.max.y;
		  //calculate difference
 		 float diff = postScaleY - preScaleY;
 
 		 //move the bar to the left
 		 transform.Translate(new Vector2(0f, diff));

        if(hand != null) {
            hand.position = new Vector2(transform.position.x, transform.position.y - 0.5f * transform.localScale.y);
        }

         yield return null;
        }

     }

     public void Grow(Transform hand = null) {
         grow = true;
         StartCoroutine(ChangeSize(hand));
     }

     public void Shrink(Transform hand = null) {
         grow = false;
        StartCoroutine(ChangeSize(hand));
     }
 }
