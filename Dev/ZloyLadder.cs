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

	void OnEnable(){
		// quand il grimpe, le personnage n'est plus sujet a la gravite et ne se cogne plus aux murs
		gravity = rigidbody2D.gravityScale;
		rigidbody2D.gravityScale = 0;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player character"), LayerMask.NameToLayer("ground"), true);
	}

	void OnDisable(){
		rigidbody2D.gravityScale = gravity;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("player character"), LayerMask.NameToLayer("ground"), false);
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
	}

	void FixedUpdate(){
		float trueDirection = direction;

		// on triche un peu pour simuler le mouvement vertical ; on utilise un mouvement horizontal renverse
		direction = Input.GetAxis ("Vertical");	
		if (direction !=0) {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.y, 0);
			ApplyHorizontalMove();
			rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.x);
		}
		else rigidbody2D.velocity = new Vector2(0, 0);

		direction = trueDirection;
	}
}
