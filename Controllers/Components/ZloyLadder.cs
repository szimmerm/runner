using UnityEngine;
using System.Collections;

public class ZloyLadder : PlayerController {

	public float verticalAcceleration = 1;
	private float gravity = 1;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		SetDirection();
	}

	protected void OnEnable(){
		// quand il grimpe, le personnage n'est plus sujet a la gravite et ne se cogne plus aux murs
		gravity = GetComponent<Rigidbody2D>().gravityScale;
		GetComponent<Rigidbody2D>().gravityScale = 0;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Players"), LayerMask.NameToLayer("Walls"), true);
	}

	protected void OnDisable(){
		GetComponent<Rigidbody2D>().gravityScale = gravity;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Players"), LayerMask.NameToLayer("Walls"), false);
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
	}

	void FixedUpdate(){
/*
		float trueDirection = values.direction;

		// on triche un peu pour simuler le mouvement vertical ; on utilise un mouvement horizontal renverse
		values.direction = Input.GetAxis ("Vertical");	
		if (values.direction !=0) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, 0);
			ApplyHorizontalMove();
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.x);
		}
		else GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		values.direction = trueDirection;
*/
	}
}
