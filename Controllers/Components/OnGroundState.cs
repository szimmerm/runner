using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]

/// <summary>
/// Etat de controle d'un personnage au sol (utile ou ?)
/// </summary>
public class OnGroundState : ControlledComponent {
	public float deadZone = 0.001F;
	public float jumpImpulse;

	private bool doJump = false;
	
	// animation
	public Animator animator;
	
	// Use this for initialization
	public override void Awake () {
		base.Awake ();
		animator = GetComponent<Animator>();
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
//			DoTransition("fall");
		} else if(!Interval.InInterval(rigidbody2D.velocity.y, deadZone)){
			// sinon si on tombe, c'est aussi l'etat de saut qui prend le relais mais sans impulsion
//			DoTransition("fall");
		}
	}
}
