using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour {

	private Vector3 oldPosition;
	private List<ParallaxItem> backgroundItems;

	// Use this for initialization
	void Awake () {
		oldPosition = transform.position;
		backgroundItems = new List<ParallaxItem>();
	}

	public void RegisterObject(ParallaxItem element) {
		backgroundItems.Add (element);
	}

	public void RemoveObject(ParallaxItem element) {
		backgroundItems.Remove (element);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = oldPosition - transform.position;
		foreach(ParallaxItem item in backgroundItems) {
			item.UpdatePosition (movement);
		}
		oldPosition = transform.position;
	}
}
