using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("entering pouet pouet pouet");
		DameMachine machine = other.GetComponent<DameMachine> ();
		if (machine != null && other.rigidbody2D.velocity.y < 0) {
			machine.context.SetTrigger ("swim");
			Debug.Log ("in");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("exiting pouet pouet pouet");
		DameMachine machine = other.GetComponent<DameMachine> ();
		if (machine != null && other.rigidbody2D.velocity.y > 0) {
			machine.context.SetTrigger ("out");
			Debug.Log ("out");
		}
	}

}
