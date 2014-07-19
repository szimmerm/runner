using UnityEngine;
using System.Collections;

public class HorizontalMove : GenericHorizontalMove {

	public float maxSpeed = 2f;
	public float acceleration = 20f;
	public float horizontalFriction = 20f;

	void FixedUpdate(){
		if (direction != 0){
			AccelerateWithCap(acceleration, maxSpeed);
		}
		else{
			DecreaseSpeed(horizontalFriction);
		}
		UpdateAnimator();
	}
}
