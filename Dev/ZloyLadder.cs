using UnityEngine;
using System.Collections;

public class ZloyLadder : PlayerController {

	public float verticalAcceleration = 1;
	public float caca = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		direction = Input.GetAxis ("Vertical");	
		caca = direction;
	}

	void OnEnable(){
		rigidbody2D.isKinematic = true;
	}

	void OnDisable(){
		rigidbody2D.isKinematic = false;
	}

	void FixedUpdate(){
		if (direction !=0) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.y, 0);
			ApplyHorizontalMove();
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.x);
		}
		else rigidbody2D.velocity = new Vector2(0, 0);
	}
}
