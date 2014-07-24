using UnityEngine;
using System.Collections;

/// <summary>
/// Classe de base pour les composants geres par des automates de controle
/// </summary>
public class ControlledComponent : MonoBehaviour {

	/// <summary>
	/// Contexte comprennant les variables internes de l'automate de controle
	/// </summary>
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
