using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public bool isRunning = true;

	// Use this for initialization
	void Start () {
		isRunning = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("space")){
			Debug.Log ("space");
			if(isRunning){
				Debug.Log("pause");
				Time.timeScale = 0;
				isRunning = false;
			} 
			else{
				Debug.Log ("Run");
				Time.timeScale = 1;
				isRunning = true;
			}
		}
	}
}
