using UnityEngine;
using System.Collections;

public class DestroyableObject : MonoBehaviour {

	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void Die(){
		if (deathSound) {
			AudioSource.PlayClipAtPoint(deathSound, transform.position);
		}
		Destroy(gameObject);
	}
}
