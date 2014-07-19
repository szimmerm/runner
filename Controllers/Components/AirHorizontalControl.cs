using UnityEngine;
using System.Collections;

public class AirHorizontalControl : GenericHorizontalMove {

	
	public float airSlowing = 0.2f;
	public float maxSpeed = 2f;
	private float jumpDirection;
	
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		// jumpDirection = Mathf.Sign (rigidbody2D.velocity.x);
	}
	
	// Update is called once per frame
	void Update () {
		direction = Input.GetAxis ("Horizontal");	
	}
	
	void FixedUpdate(){
		//if (Mathf.Sign (direction) != jumpDirection){
			AccelerateWithCap(airSlowing, maxSpeed);
		//}
	}

}
