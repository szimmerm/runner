using UnityEngine;
using System.Collections;

public class JumpAppears : MonoBehaviour {

	public WalkingEnemy moveScript;
	public float height;

	// booleen qui sert a determiner dans quelle phase du saut on est
	private bool descending = false;
	private float deadZone = 0.001F;

	// Use this for initialization
	void Start () {
		moveScript.enabled = false;
		GetComponent<Rigidbody2D>().AddForce (new Vector2(0, height));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		float velocity = GetComponent<Rigidbody2D>().velocity.y;
		if (!descending && velocity < 0){
			descending = true;
			gameObject.layer = 20;
		}
		else if (descending){
			// si le personnage est au sol (sa vitesse devient nulle apres une phase descendante)
			if (velocity < deadZone && velocity > -deadZone){
				moveScript.enabled = true;
				this.enabled = false;
			}
		}
	}
}
