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
		float velocity = GetComponent<Rigidbody2D>().velocity.x;
		if(velocity > deadZone || velocity < -deadZone){
			float direction = Mathf.Sign (velocity);
	
			/* si le sprite a change de direction */
			if (direction != previousDirection){
				/* scale update */
				ReverseDirection(gameObject);

				// on retourne le pistolage si y a lieu

//				FireScript weapon = GetComponentInChildren<FireScript>();	
//				if (weapon != null) {
//					ReverseDirection(weapon.gameObject);
//				}
				previousDirection = direction;
			}
		}
	}

	static void ReverseDirection(GameObject obj){
		obj.transform.Rotate (new Vector3(0, 180, 0));
	}

	void OnReturn(){
		
	}
}
