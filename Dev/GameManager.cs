using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private Transform linkedCamera;
	public float recenterFactor = 2;
	public float cameraDeadzone = 0.1f;

	// Use this for initialization
	void Start () {
		linkedCamera = GetComponentInChildren<Camera>().transform;
	}

	public void AddRumble(Vector3 force) {
		linkedCamera.localPosition = force;
	}
	
	private void RecenterCamera() {
		if (Vector3.SqrMagnitude(linkedCamera.localPosition) < cameraDeadzone) {
			linkedCamera.localPosition = Vector3.zero;
		} else {
			linkedCamera.localPosition = linkedCamera.localPosition / recenterFactor;
		}
	}

	public void Test(){
		Debug.Log ("prout");
	}

	// Update is called once per frame
	void Update () {
		RecenterCamera();
	}
}
