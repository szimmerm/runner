using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject following;
	public Vector2 min;
	public Vector2 max;
	public Vector2 deadZone;

	private ParallaxSystemScript layerManager;
	public GameObject layers = null;

	public float oldPosX;

	// Use this for initialization
	void Start () {
		oldPosX = transform.position.x;
		if (layers != null){
			layerManager = layers.GetComponent<ParallaxSystemScript>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		Vector3 followPos = following.transform.position;

		// mise a jour abscisse
		if (followPos.x > pos.x + deadZone.x){
			pos.x = followPos.x - deadZone.x;
		}
		else if (followPos.x < pos.x - deadZone.x){
			pos.x = followPos.x + deadZone.x;
		}
		// test sur les extremums
		if (pos.x < min.x){
			pos.x = min.x;
		}
		else if (pos.x > max.x){
			pos.x = max.x;
		}


		// mise a jour ordonnee
		if (followPos.y > pos.y + deadZone.y){
			pos.y = followPos.y - deadZone.y;
		}
		else if (followPos.y < pos.y - deadZone.y){
			pos.y = followPos.y + deadZone.y;
		}
		// test sur les extremums
		if (pos.y < min.y){
			pos.y = min.y;
		}
		else if (pos.y > max.y){
			pos.y = max.y;
		}
		
		transform.position = pos;
		float delta = pos.x - oldPosX;
		oldPosX = pos.x;
		if (layerManager != null){
			layerManager.Move(delta);
		}
	}
}
