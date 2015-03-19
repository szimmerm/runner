using UnityEngine;
using System.Collections;

/// <summary>
/// Script de lancer de grenades
/// </summary>
public class Grenade : MonoBehaviour {

	/// <summary>
	/// Vitesse de la grenade
	/// </summary>
	public Vector2 speed; 

	/// <summary>
	/// Createur de grenade
	/// </summary>
	public Transform grenadePrefab;


	// Use this for initialization
	void Start () {
	
	}
	
	/// <summary>
	/// Lance une grenade a un point precis
	/// </summary>
	/// <param name="goal">Le point vise par la grenade.</param>
	void AimAtPoint(Vector2 goal){
		Transform grenade = (Transform) Instantiate(grenadePrefab);
		grenade.position = transform.position;

		// calcul de l'impulsion
		Vector2 diff = (Vector2)goal - (Vector2)transform.position;
		float ySpeed = (diff.y / diff.x) * speed.x - Physics.gravity.y * diff.x / (speed.x * 2);
		grenade.GetComponent<Rigidbody2D>().velocity = new Vector2(speed.x, ySpeed);
	}

	/// <summary>
	/// Lance une grenade sur un personnage
	/// </summary>
	/// <param name="goal">La cible visee par la grenade.</param>
	void AimAtTransform(Transform goal){
		AimAtPoint(goal.transform.position);
	}

	/// <summary>
	/// Lance une grenade a distance fixe
	/// </summary>
	void ThrowGrenade() {
		Transform grenade = (Transform) Instantiate(grenadePrefab);
		grenade.position = transform.position;
		grenade.GetComponent<Rigidbody2D>().velocity = new Vector2(speed.x, speed.y);
	}
}
