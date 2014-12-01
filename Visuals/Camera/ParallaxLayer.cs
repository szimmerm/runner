using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {
	public float distance;
	private float parallaxFactor;

	public void Start(){
		parallaxFactor = 1/(distance+1);
	}
	
	// Update is called once per frame
	public void Move(float delta) {
		Vector3 newPos = transform.localPosition;
		newPos.x -= delta * parallaxFactor;

		transform.localPosition = newPos;
	}

	public void setPhysicsLayer(int layer){
		gameObject.layer = layer;
		foreach(Transform child in transform){
			child.gameObject.layer = layer;
		}
	}

	public void Update(){
		// virer ca a la fin, calcul inutile, mais pratique pour bidouiller
		parallaxFactor = 1/(distance+1);
	}
}
