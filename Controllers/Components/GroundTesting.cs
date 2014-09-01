using UnityEngine;
using System.Collections;

/// <summary>
/// Classe pour tester si un objet est au sol ou non
/// </summary>
public class GroundTesting : MonoBehaviour {

	public Transform goal;
	public int[] layers;
	public bool testValue;


	// bug probleme a retrouver
	public bool isOnGround() {
		RaycastHit2D result = Physics2D.Linecast (
				transform.position, 
				goal.transform.position, 
				buildRaycastLayer(layers));
		return result;
		
	}

	/// <summary>
	/// Construit l'entier correspondant aux differents layers
	/// </summary>
	/// <returns>L'entier de la serie de layers.</returns>
	/// <param name="layerList">les indices de layers consideres.</param>
	private int buildRaycastLayer(int[] layerList) {
		// trouver foldr
		int mask = 0;
		foreach (int layer in layerList) {
			mask = mask | (1 << layer);
		}
		return mask;
	}
	

	void FixedUpdate() {
		testValue = isOnGround();
	}

}
