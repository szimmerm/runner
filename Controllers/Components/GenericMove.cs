using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(ObjectValues))]

/// <summary>
/// Module contenant des operations generiques sur la manipulation du rigidbody.
/// </summary>
public class GenericMove : ControlledComponent {

	/// <summary>
	/// Fait accelerer un rigidbody en fixant une vitesse maximale.
	/// </summary>
	/// <param name="acceleration">Acceleration.</param>
	/// <param name="cap">valeur absolue de la vitesse maximale.</param>
	public void AccelerateWithCap(float acceleration, float cap){
		float previousSpeed = rigidbody2D.velocity.x;
		float newSpeed = Interval.PutInInterval (previousSpeed + (values.direction*acceleration), cap);
		rigidbody2D.velocity = new Vector2(newSpeed, rigidbody2D.velocity.y);
	}

	/// <summary>
	/// Ralenti un rigidbody en garantissant que la vitesse ne change pas de signe
	/// </summary>
	/// <param name="decreaseValue">Ralentissement applique.</param>
	public void DecreaseSpeed(float decreaseValue){
		float previousSpeed = rigidbody2D.velocity.x;
		float newSpeed;
		if(Mathf.Sign (previousSpeed) > 0){	
			newSpeed = Mathf.Max(0, previousSpeed - decreaseValue);
		}
		else{
			newSpeed = Mathf.Min (0, previousSpeed + decreaseValue);
		}
		rigidbody2D.velocity = new Vector2(newSpeed, rigidbody2D.velocity.y);
	}

	public void UpdateAnimator(){
//		animator.SetFloat ("horizontal speed", rigidbody2D.velocity.x);
	}

	/// <summary>
	/// Donne une impulsion de saut (vers le haut)
	/// </summary>
	/// <param name="force">Force.</param>
	public void JumpImpulse(float force){
		rigidbody2D.AddForce (new Vector3(0F, force, 0F));
	}

	/// <summary>
	/// Limitateur de la vitesse verticale vers le haut
	/// </summary>
	/// <param name="ySpeedCap">Vitesse fixee maximale.</param>
	public void CapJumpSpeed(float ySpeedCap){
		float ySpeed = Mathf.Min (ySpeedCap, rigidbody2D.velocity.y);
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, ySpeed);
	}
}
