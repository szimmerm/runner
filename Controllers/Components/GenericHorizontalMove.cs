using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]

public class GenericHorizontalMove : ControlledComponent {

	protected float direction = 0;
	private Animator animator;

	public override void Awake(){
		base.Awake ();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		direction = Input.GetAxis ("Horizontal");	
	}

	public void AccelerateWithCap(float acceleration, float cap){
		float previousSpeed = rigidbody2D.velocity.x;
		float newSpeed = Interval.PutInInterval (previousSpeed + (direction*acceleration), cap);
		rigidbody2D.velocity = new Vector2(newSpeed, rigidbody2D.velocity.y);
	}

	public void DecreaseSpeed(float decreaseValue){
		float previousSpeed = rigidbody2D.velocity.x;
		float newSpeed;
		if(Mathf.Sign (previousSpeed) > 0){	
			newSpeed = Mathf.Max(0, previousSpeed - decreaseValue);
		}
		else{
			newSpeed = Mathf.Min (0, previousSpeed + decreaseValue);
		}
		rigidbody2D.velocity = new Vector2(newSpeed, rigidbody2D.velocity.y);
	}

	public void UpdateAnimator(){
//		animator.SetFloat ("horizontal speed", rigidbody2D.velocity.x);
	}
}
