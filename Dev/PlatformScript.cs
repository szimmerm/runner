
using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		Vector2 normal = collision.contacts[0].normal;
		Debug.Log("collision normal : "+normal);
		if (collision.collider.tag == "Player" && collision.contacts[0].normal.y >= 0) {
			Debug.Log ("relative velocity : "+collision.relativeVelocity);
			Physics2D.IgnoreCollision(collision.collider, collider2D, true);
			Debug.Log ("enter");
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"){
			Physics2D.IgnoreCollision (other, collider2D, false);
			Debug.Log ("exit");
		}
	}
}
