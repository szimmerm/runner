using UnityEngine;
using System.Collections;

public class FSMState : MonoBehaviour {

	public StateManager<FSMState, int> machine;

	// Use this for initialization
	void Start () {
//		machine.AddState (this);
	}

	public void DoTransition(int i){
		if (machine != null){
			machine.DoTransition (i);
			FSMState newState = machine.currentState;
			newState.enabled = true;
			this.ResetStateLogic ();
			this.enabled = false;
		}
	}

	public virtual void ResetStateLogic(){
		return;
	}
}
