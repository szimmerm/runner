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
	public void AccelerateWithCap(float xAcceleration, float yAcceleration, float xCap, float yCap){
		Vector2 previousSpeed = values.body.velocity;
		float newXSpeed = Interval.PutInInterval (previousSpeed.x + (values.direction.x*xAcceleration), xCap);
		float newYSpeed = Interval.PutInInterval (previousSpeed.y + (values.direction.y*yAcceleration), yCap);
		values.body.velocity = new Vector2(newXSpeed, newYSpeed);
	}

	public void AccelerateWithCap(float xAcceleration, float xCap){
		Vector2 previousSpeed = values.body.velocity;
		float newXSpeed = Interval.PutInInterval (previousSpeed.x + (values.direction.x*xAcceleration), xCap);
		values.body.velocity = new Vector2(newXSpeed, previousSpeed.y);
	}

	/// <summary>
	/// Ralenti un rigidbody en garantissant que la vitesse ne change pas de signe
	/// </summary>
	/// <param name="decreaseValue">Ralentissement applique.</param>
	public void DecreaseSpeed(float decreaseValue){
		float previousSpeed = values.body.velocity.x;
		float newSpeed;
		if(Mathf.Sign (previousSpeed) > 0){	
			newSpeed = Mathf.Max(0, previousSpeed - decreaseValue);
		}
		else{
			newSpeed = Mathf.Min (0, previousSpeed + decreaseValue);
		}
		values.body.velocity = new Vector2(newSpeed, values.body.velocity.y);
	}

	/// <summary>
	/// Donne une impulsion de saut (vers le haut)
	/// </summary>
	/// <param name="force">Force.</param>
	public void JumpImpulse(float force){
		values.body.AddForce (new Vector3(0F, force, 0F));
	}

	/// <summary>
	/// Limitateur de la vitesse verticale vers le haut
	/// </summary>
	/// <param name="ySpeedCap">Vitesse fixee maximale.</param>
	public void CapJumpSpeed(float ySpeedCap){
		float ySpeed = Mathf.Min (ySpeedCap, values.body.velocity.y);
		values.body.velocity = new Vector2(values.body.velocity.x, ySpeed);
	}
}
