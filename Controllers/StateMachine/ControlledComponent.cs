using UnityEngine;
using System.Collections;


[RequireComponent (typeof(ObjectValues))]
/// <summary>
/// Classe de base pour les composants geres par des automates de controle
/// </summary>
public class ControlledComponent : MonoBehaviour {
	/// <summary>
	/// valeurs internes de l'objet controle
	/// </summary>
	protected ObjectValues values;
	
	void Awake() {
		values = GetComponent<ObjectValues>();
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
