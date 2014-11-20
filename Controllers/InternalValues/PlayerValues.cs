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

	public Transform groundTrigger;
}
