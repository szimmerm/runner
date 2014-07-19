using UnityEngine;
using System.Collections;

public class SimpleActivator : AbstractActivationScript {
	public MonoBehaviour aiScript;

	public override void Activate(){
		aiScript.enabled = true;
		gameObject.rigidbody2D.isKinematic = false; 
	}
	
	public override void Desactivate(){

	}
}
