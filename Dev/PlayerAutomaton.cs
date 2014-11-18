using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ZloyGround))]
[RequireComponent (typeof(AirZloy))]

public class PlayerAutomaton : AutomatonBuilder {
	public override void BuildStates (){
		AddState("ground", GetComponent<ZloyGround>());
		AddState("air", GetComponent<AirZloy>());
		AddState("ladder", GetComponent<ZloyLadder>());


		SetBaseState("ground");
	}
	
	public override void BuildTransitions() {
		System.Predicate<TestContext> test;
		
		test = (ctxt) => {return !(ctxt.GetBool ("onGround"));};
		AddSimpleTransition("ground", "groundToAir", "air", test);
		
		test = (ctxt) => {return ctxt.GetBool ("onGround");};
		AddSimpleTransition("air", "airToGround", "ground", test);

		test = (ctxt) => {return ctxt.GetBool ("onLadder") && ctxt.GetBool ("climbing");};
		AddSimpleTransition("ground", "groundToLadder", "ladder", test);
	}
}