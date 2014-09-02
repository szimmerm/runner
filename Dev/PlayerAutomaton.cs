using UnityEngine;
using System.Collections;

public class PlayerAutomaton : AutomatonBuilder {
	public override void BuildStates (){
		AddState("ground", GetComponent<ZloyGround>());
		SetBaseState("ground");
	}
	
	public override void BuildTransitions() {
		
	}
}