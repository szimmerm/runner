using UnityEngine;
using System.Collections;

public class DirectionChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("coucou !");
		WalkingEnemy enemyScript = other.gameObject.GetComponent<WalkingEnemy>();
		if(enemyScript != null){
			enemyScript.ChangeDirection();
			Debug.Log ("voir ma bite ?");
		}
	}
}
