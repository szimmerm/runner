using UnityEngine;
using System.Collections;

public class OldWhipState : MonoBehaviour {

	public float Cooldown = 20F;

	private bool doWhip = false;
	public float currentCoolDown;

	// animation
	private Animator animator;
	
	// Use this for initialization
	void Start () {
		//Physics2D.IgnoreLayerCollision(8, 9, true);	
		animator = GetComponent<Animator>();
		currentCoolDown = 0F;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCoolDown > 0){
			currentCoolDown -= Time.deltaTime * 100F;
		}
		else if(Input.GetButtonDown("Whip")){
			doWhip = true;
		}
	}

	void FixedUpdate(){
		if(doWhip){
			animator.SetTrigger("whip");
			doWhip = false;
			currentCoolDown = Cooldown;
		}
	}
}
