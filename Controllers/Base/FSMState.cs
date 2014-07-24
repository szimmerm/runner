using UnityEngine;
using System.Collections;

/// <summary>
/// Etat generique pour un automate fini
/// </summary>
public class FSMState : MonoBehaviour {

	public StateManager<FSMState, int> machine;

	// Use this for initialization
	void Start () {
//		machine.AddState (this);
	}

	/// <summary>
	/// Execution de la transition <paramref name="i">
	/// </summary>
	/// <param name="i">Numero de la transition a effectuer.</param>
	public void DoTransition(int i){
		if (machine != null){
			machine.DoTransition (i);
			FSMState newState = machine.currentState;
			newState.enabled = true;
			this.ResetStateLogic (); // utile ou ?
			this.enabled = false; // utile ou ?
		}
	}

	/// <summary>
	/// Remise a zero de l'etat interne de l'etat si necessaire
	/// </summary>
	public virtual void ResetStateLogic(){
		return;
	}
}
