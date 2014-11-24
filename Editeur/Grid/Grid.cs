using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public float width = 1.0f;
	public float height = 1.0f;

	void OnDrawGizmos(){
		if (height == 0f || width == 0f){
			return;
		}

		Vector3 pos = Camera.current.transform.position;

		// draw the grid
		for(float y = pos.y - 800.0f; y < pos.y+800.0f; y+=height){
			Gizmos.DrawLine (new Vector3(-10000000.0f, Mathf.Floor (y/height)*height, 0.0f),
							new Vector3(100000000.0f, Mathf.Floor (y/height)*height, 0.0f));
		}

		for(float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x+=width){
			Gizmos.DrawLine (new Vector3(Mathf.Floor (x/width)*width, -10000000.0f, 0.0f),
				new Vector3(Mathf.Floor (x/width)*width, 100000000.0f, 0.0f));
		}
	}
}
