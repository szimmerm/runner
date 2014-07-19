using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int hitPoints = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log ("coucoullision !");
		if(coll.gameObject.tag == "shoots"){
			if (--hitPoints <= 0) {
				Die();
			}
		}
	}

	void Die(){
		Debug.Log ("je meurs, aaarg");
	}
}
