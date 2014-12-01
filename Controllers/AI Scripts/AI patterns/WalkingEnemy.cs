using UnityEngine;
using System.Collections;

public class WalkingEnemy : MonoBehaviour {

	public float direction;
	public float acceleration;
	public Vector2 maxSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		// si aucun input, le perso freine
		/*	if (direction < deadZone && direction > -deadZone){
			if (rigidbody2D.velocity.x > 0){
				rigidbody2D.velocity = new Vector2(Mathf.Max(0, rigidbody2D.velocity.x - horizontalFriction), rigidbody2D.velocity.y);
			}
			else if (rigidbody2D.velocity.x < 0){
				rigidbody2D.velocity = new Vector2(Mathf.Min(0, rigidbody2D.velocity.x + horizontalFriction), rigidbody2D.velocity.y);
			}
		}
		else {*/
		// si on se cogne à un mur, on change de direction
		if (rigidbody2D.velocity.x == 0) {
			ChangeDirection();
		}
		// on rajoute l'accélération dans la bonne direction
		Vector2 velocity = rigidbody2D.velocity + new Vector2 (direction*acceleration, 0);
	
		if (velocity.x > maxSpeed.x){
			//rigidbody2D.velocity.Set(maxSpeed, rigidbody2D.velocity.y);
			velocity = new Vector2(maxSpeed.x, velocity.y);
		}
		else if (velocity.x < -maxSpeed.x){
			velocity = new Vector2(-maxSpeed.x, velocity.y);
		}

		rigidbody2D.velocity = velocity;

	}

	public void ChangeDirection(){
		direction = direction * (-1);
	}

}
