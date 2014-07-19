using UnityEngine;
using System.Collections;

public class TriggersLauncher : MonoBehaviour {

	public string[] triggers;

	void OnTriggerEnter2D(Collider2D other){
	//	Debug.Log ("entering pouet pouet pouet");
		DameMachine machine = other.GetComponent<DameMachine> ();
		if (machine != null) {
			foreach(string trigger in triggers){
				//Debug.Log ("setting trigger "+trigger);
				machine.context.SetTrigger (trigger);
			}
		}
	}
}
