using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

public class WhipState : ControlledComponent {

	// animation
	private Animator animator;
	
	// Use this for initialization
	public override void Awake() {
		base.Awake ();
		animator = GetComponent<Animator>();
	}

	void OnEnable(){
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Whip")){
			Debug.Log ("whipit !");
			animator.SetTrigger ("whip");
			context.SetTrigger("whip");
		}
	}
}
