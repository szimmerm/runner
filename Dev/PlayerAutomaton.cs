using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ZloyGround))]
[RequireComponent (typeof(AirZloy))]

public class PlayerAutomaton : AutomatonBuilder {
	public override void BuildStates (){
		System.Predicate<TestContext> test;

		AddState("ground", GetComponent<ZloyGround>());
		AddState("air", GetComponent<AirZloy>());

		test = (ctxt) => {return !(ctxt.GetBool ("onGround"));};
		AddSimpleTransition("ground", "groundToAir", "air", test);

		test = (ctxt) => {return ctxt.GetBool ("onGround");};
		AddSimpleTransition("air", "airToGround", "ground", test);

		SetBaseState("ground");
	}
	
	public override void BuildTransitions() {
		
	}
}