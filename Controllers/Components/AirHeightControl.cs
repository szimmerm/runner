using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]

public class AirHeightControl : ControlledComponent {
	
	public float deadZone = 0.001F;

	public float lowSpeed;

//	private bool doJump = false;
//	private bool isGrounded = false;
	private float lastYSpeed = 1;
	
	public Vector2 velocity;
	
	// animation
	public Animator animator;

	// touche de saut enfoncee ?
	public bool jumpPushed;

	
	public override void Awake(){
		base.Awake ();
		animator = GetComponent<Animator>();
	}

	void OnEnable(){
		animator.SetBool("grounded", false);
		animator.SetFloat ("attack height", 0);
//		controller.context.SetTrigger ("landTrigger");
		lastYSpeed = 1;
		jumpPushed = true;
	}
	
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		jumpPushed = Input.GetButton ("Jump");
	}
	
	void FixedUpdate(){
		if (lastYSpeed < 0 && rigidbody2D.velocity.y >= -deadZone){
			// on vient d'atterir au sol ; on change donc d'etat
//			DoTransition("fall");
			animator.SetBool("grounded", true);
			animator.SetFloat ("attack height", 1);
//			Debug.LogError ("landTrigger !");
			context.SetTrigger ("landTrigger");
		} else{
			// si le bouton de saut est releve, on bloque la vitesse max du saut
			if ((!jumpPushed) && rigidbody2D.velocity.y > lowSpeed){
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, lowSpeed);
			}
			lastYSpeed = rigidbody2D.velocity.y;
		}
	}
}