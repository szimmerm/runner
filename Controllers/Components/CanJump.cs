using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]

public class CanJump : ControlledComponent {
	public float deadZone = 0.001F;
	public float jumpImpulse = 165;

	private bool doJump = false;
	
	// animation
//	private Animator animator;
	
	// Use this for initialization
	public override void Awake () {
		base.Awake ();
//		animator = GetComponent<Animator>();
	}

/*
	void MachineInit(Animator anim){
		animator = anim;
	}
*/
	
	void OnEnable(){
		doJump = false;
//		Vector2 velocity = rigidbody2D.velocity;
		context.SetFloat("yVelocity", 0);
		context.SetFloat("xVelocity", rigidbody2D.velocity.x);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			doJump = true;
		}
	}
	
	void FixedUpdate(){
		if(doJump){
			// si on saute, c'est l'etat de saut qui prend le relais
			rigidbody2D.AddForce (new Vector2(0, jumpImpulse));
			context.SetTrigger ("jump");
//			Debug.Log ("jump activated !! xVelocity : "+controller.context.GetFloat("xVelocity"));
			doJump = false;
//			DoTransition(1);
		} else if(!Interval.InInterval(rigidbody2D.velocity.y, deadZone)){
			// sinon si on tombe, c'est aussi l'etat de saut qui prend le relais mais sans impulsion
//			DoTransition(1);
		}
		context.SetFloat("xVelocity", rigidbody2D.velocity.x);
		context.SetFloat("yVelocity", rigidbody2D.velocity.y);
	}
}
