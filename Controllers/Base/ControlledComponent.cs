using UnityEngine;
using System.Collections;

public class ControlledComponent : MonoBehaviour {

	public TestContext context;

	public virtual void Awake(){
		enabled = false;
	}

	// Use this for initialization
	void Start () {
//		machine.AddState (this);
	}

/*
	public void DoTransition(string s){
		if (controller != null){
			controller.DoTransition (s);
		}
	}
*/
}
