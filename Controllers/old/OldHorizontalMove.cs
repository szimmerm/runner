using UnityEngine;
using System.Collections;

public class OldHorizontalMove : MonoBehaviour {

	public float maxSpeed = 1f;
	public float acceleration = 150f;
	private float direction;

	public Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		direction = Input.GetAxis ("Horizontal");	
	}

	void FixedUpdate(){
		rigidbody2D.AddForce(new Vector2(direction * acceleration, 0));
		float xSpeed = Interval.PutInInterval (rigidbody2D.velocity.x, maxSpeed);
		rigidbody2D.velocity = new Vector2(xSpeed, rigidbody2D.velocity.y);
		animator.SetFloat ("horizontal speed", xSpeed);
	}
}
