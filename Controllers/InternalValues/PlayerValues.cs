using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Valeurs internes du personnage. On utilise une classe separee pour qu'elles soient communes a tout les composants du personnage
/// </summary>
public class PlayerValues : ObjectValues {
	
	public float maxSpeed = 2f; // vitesse horizontale maximale de l'objet
	public float acceleration = 20f; // acceleration horizontale de l'objet
	public float horizontalFriction = 20f; // ralentissement de l'objet lorsqu'on ne touche a rien
	public bool onGround = false; // objet au sol ?
	public bool climbing = false; // objet grimpant ? (utile ? devrait etre integre a l'automate d'etat)

	public Transform groundTriggerPrefab; // qui instancier comme detecteur de sol

	// le detecteur de sol, cree au lancement du jeu
	private Transform groundTrigger;
	public Collider2D groundCollider; // element sur lequel marche le personnage

	void Awake() {
		groundTrigger = (Transform)Instantiate (groundTriggerPrefab);	
		groundTrigger.parent = transform;
		groundTrigger.localPosition = new Vector3 (0f, -0.75f, 0f);
	}

	public Transform GetGroundTrigger(){
		return groundTrigger;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "ladderTrigger") {
			float ladders = context.GetFloat ("ladderValue") + 1;
			context.SetFloat ("ladderValue", ladders);
		} else if (other.tag == "ladderTop") {
			float ladders = context.GetFloat ("ladderTopValue") + 1;
			context.SetFloat ("ladderTopValue", ladders);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "ladderTrigger") {
			float ladders = context.GetFloat ("ladderValue") - 1;
			context.SetFloat ("ladderValue", ladders);
		} else if (other.tag == "ladderTop") {
			float ladders = context.GetFloat ("ladderTopValue") - 1;
			context.SetFloat ("ladderTopValue", ladders);
		}
	}
}
