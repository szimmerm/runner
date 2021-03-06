﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ZloyGround))]
[RequireComponent (typeof(AirZloy))]
[RequireComponent (typeof(ZloyLadder))]

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

		test = (ctxt) => {return (ctxt.GetFloat ("ladderValue") == 2) 
									&& (ctxt.GetFloat ("ladderTopValue") == 0) 
									&& ctxt.GetFloat ("vertical") > 0;
						 };
		AddSimpleTransition("ground", "groundToLadder", "ladder", test);

		test = (ctxt) => {return (ctxt.GetFloat ("ladderValue") == 0);};
		AddSimpleTransition("ladder", "ladderToAir", "air", test);

		test = (ctxt) => {
						return ((ctxt.GetFloat ("ladderTopValue") == 2) 
							&& (ctxt.GetFloat ("vertical") < 0));
						};
		AddSimpleTransition ("ground", "ladderDown", "ladder", test);
	}

	public override void BuildContext(TestContext ctxt) {
		context.SetFloat("ladderValue", 0);
		context.SetFloat("ladderTopValue", 0);
	}
}