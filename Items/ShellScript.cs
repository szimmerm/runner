using UnityEngine;
using System.Collections;

public class ShellScript : MonoBehaviour {

	private Rigidbody2D body;
	private bool testing = false;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}

	IEnumerator FreezeIfStopped(){
		testing = true;
		yield return new WaitForSeconds(0.02f);
		if (body.velocity.y < 0.01 && body.velocity.y > -0.01) {
			body.isKinematic = true;
			Debug.Log ("zzzz");
			this.enabled = false;
		}
		testing = false;
	}

	void FixedUpdate() {
		if (!testing && body.velocity.y < 0.01 && body.velocity.y > -0.01) {
			StartCoroutine(FreezeIfStopped());
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
