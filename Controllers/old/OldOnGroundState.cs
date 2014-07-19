using UnityEngine;
using System.Collections;

public class OldOnGroundState : FSMState {
	public float deadZone = 0.001F;
	public float jumpImpulse;

	private bool doJump = false;
	
	// animation
	public Animator animator;
	
	// Use this for initialization
	void Awake () {
		//Physics2D.IgnoreLayerCollision(8, 9, true);	
		this.enabled = false;
	}

	void MachineInit(Animator anim){
		animator = anim;
	}
	
	void OnEnable(){
		animator.SetBool("grounded", true);
		doJump = false;
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
			DoTransition(1);
		} else if(!Interval.InInterval(rigidbody2D.velocity.y, deadZone)){
			// sinon si on tombe, c'est aussi l'etat de saut qui prend le relais mais sans impulsion
			DoTransition(1);
		}
	}
}
