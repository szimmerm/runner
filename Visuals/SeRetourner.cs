using UnityEngine;
using System.Collections;

public class SeRetourner : MonoBehaviour {

	// a l'origine tout les persos regardent a droite
	public float previousDirection = 1;
	public float deadZone = 0.000001f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float velocity = rigidbody2D.velocity.x;
		if(velocity > deadZone || velocity < -deadZone){
			float direction = Mathf.Sign (velocity);
	
			/* si le sprite a change de direction */
			if (direction != previousDirection){
				/* scale update */
				Vector3 oldScale = transform.localScale;
				Vector3 newScale = new Vector3(-1*oldScale.x, oldScale.y, oldScale.z);
				transform.localScale = newScale;
	
				previousDirection = direction;
			}
		}
	}

	void OnReturn(){
		
	}
}
