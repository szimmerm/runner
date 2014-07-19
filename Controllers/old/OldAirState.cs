using UnityEngine;
using System.Collections;

public class OldAirState : FSMState {
	
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

	void OnEnable(){
		animator.SetBool("grounded", false);
		lastYSpeed = 1;
		jumpPushed = true;
	}
	
	// Use this for initialization
	void Awake () {
		//Physics2D.IgnoreLayerCollision(8, 9, true);	
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Jump")){
			Debug.Log ("pipi");
			jumpPushed = true;
		}
		else{
			Debug.Log ("popo");
			jumpPushed = false;
		}
	}
	
	void FixedUpdate(){
		if (lastYSpeed < 0 && rigidbody2D.velocity.y >= -deadZone){
			// on vient d'atterir au sol
			Debug.Log ("doing air transition");
			DoTransition(1);
		} else{
			// si le bouton de saut est releve, on bloque la vitesse max du saut
			if ((!jumpPushed) && rigidbody2D.velocity.y > lowSpeed){
				Debug.Log ("uncap !!");
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, lowSpeed);
			}
			lastYSpeed = rigidbody2D.velocity.y;
		}
	}
}