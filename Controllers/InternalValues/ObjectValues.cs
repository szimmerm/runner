using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Valeurs internes des objets. On utilise une classe separee pour qu'elles soient communes entre les differents modules
/// </summary>
public class ObjectValues : MonoBehaviour {
	public TestContext context; // valeurs internes de l'automate de controle
	public Vector2 direction; // direction dans laquelle regarde l'objet

	public Animator animator; // animator de l'element
	public Rigidbody2D body; // rigidbody de l'element

	public GameManager controller; // gestionnaire general

	protected virtual void Awake(){
		animator = (Animator) GetComponent<Animator>();
		body = (Rigidbody2D) GetComponent<Rigidbody2D>();
		controller = (GameManager) GameObject.Find("Game Manager").GetComponent<GameManager>();
	}

	
}
