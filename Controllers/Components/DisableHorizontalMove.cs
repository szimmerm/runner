using UnityEngine;
using System.Collections;

public class DisableHorizontalMove : ControlledComponent {

	// Use this for initialization
	void Start () {
	}
	
	void OnEnable(){
		float ySpeed = rigidbody2D.velocity.y;
		rigidbody2D.velocity = new Vector2(0, ySpeed);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
