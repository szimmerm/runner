using UnityEngine;
using System.Collections;

public class ParallaxItem : MonoBehaviour {

	private Vector3 truePosition;
	public float distance;

	// Use this for initialization
	void Start () {
		truePosition = transform.position;
		ParallaxManager manager = (ParallaxManager)GameObject.Find("Game Manager").GetComponent<ParallaxManager>();
		manager.RegisterObject (this);
	}

	public void UpdatePosition (Vector3 movement) {
		truePosition -= movement / distance ;
		transform.position = truePosition;
	}
}
