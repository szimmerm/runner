using UnityEngine;
using System.Collections;

public class OnViewActivator : MonoBehaviour {
	// liste des scripts a activer quand on arrive a l'ecran
	public AbstractActivationScript[] scripts;
	
	// permet de ne faire qu'une seule activation
	private bool wasVisible = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!wasVisible && GetComponent<Renderer>().isVisible){
			wasVisible = true;
			for(int i=0 ; i<scripts.Length ; i++){
				scripts[i].Activate();
			}
		}
	}
}
