using UnityEngine;
using System.Collections;

/// <summary>
/// Module qui bloque la vitesse horizontale d'un objet a son activation
/// trouver d'où vient un truc pareil et le detruire, un module comme ca devrait
/// pas exister
/// </summary>
public class DisableHorizontalMove : ControlledComponent {

	// Use this for initialization
	void Start () {
	}
	
	void OnEnable(){
		float ySpeed = rigidbody2D.velocity.y;
		rigidbody2D.velocity = new Vector2(0, ySpeed);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
