
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
				Flip();

				// on retourne le pistolage si y a lieu

//				FireScript weapon = GetComponentInChildren<FireScript>();	
//				if (weapon != null) {
//					ReverseDirection(weapon.gameObject);
//				}
				previousDirection = direction;
			}
		}
	}

	public void Flip(){
		Debug.Log ("flip !");
		SeRetourner.ReverseDirectionOfObject(this.gameObject);
	}

	public static void ReverseDirectionOfObject(GameObject obj){
//		obj.transform.Rotate (new Vector3(0, 180, 0));
		Vector3 scale = obj.transform.localScale;
		obj.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
	}

	void OnReturn(){
		
	}
}
