using UnityEngine;
using System.Collections;

public class SimpleActivator : AbstractActivationScript {
	public MonoBehaviour aiScript;

	public override void Activate(){
		aiScript.enabled = true;
		gameObject.GetComponent<Rigidbody2D>().isKinematic = false; 
	}
	
	public override void Desactivate(){

	}
}
