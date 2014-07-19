using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
public class BaseState{
	public TestContext context;

	virtual public void OnStateEnter(){
		return;
	}
	
	virtual public void OnStateExit(){
		return;
	}
}
*/

public class EnablingState : BaseState{
	public List<ControlledComponent> components = new List<ControlledComponent>();
	
	public EnablingState(string nam, TestContext ctxt, StateManager<BaseState, Transition> control) : base(nam, ctxt, control){
		components = new List<ControlledComponent>();
	}	
	
	public void AddComponent(ControlledComponent component){
		//		Debug.Log ("component added : "+component.ToString ());
		components.Add (component);
	}
	
	public override void OnStateExit(){
		base.OnStateExit ();
		foreach(ControlledComponent component in components){
			component.enabled = false;
			
		}
	}
	
	public override void OnStateEnter(){
		context.ResetTriggers();
		foreach(ControlledComponent component in components){
			//			Debug.Log ("Enabling : "+component);
			component.context = context;
			component.enabled = true;
		}
		
		// reset des timers
		
	}
	
	// reset des triggers
	//		context.ResetTriggers ();
}

	
	


public class BaseState{
	
	public StateManager<BaseState, Transition> controller;
	public TestContext context;
	public List<KeyValuePair<System.Predicate<TestContext>, Transition>> tests;
	public List<KeyValuePair<float, Transition>> timerTests;
	private List<KeyValuePair<float, Transition>> timerInits;

	public string name;


	public BaseState(string nam, TestContext ctxt, StateManager<BaseState, Transition> control){
		name = nam;
		context = ctxt;
		controller = control;

		// initialisation des composantes
		timerTests = new List<KeyValuePair<float, Transition>>();
		timerInits = new List<KeyValuePair<float, Transition>>();
		tests = new List<KeyValuePair<System.Predicate<TestContext>, Transition>>();
	}

	virtual public void OnStateEnter(){

	}

	virtual public void OnStateExit(){
		ResetTimers();
	}

	public BaseState DoTransition(Transition s){
		if (controller != null){
			BaseState newState = controller.DoStateTransition (this, s);
			//Debug.Log ("exiting "+this.name);
			OnStateExit();
			//Debug.Log ("executing transition "+s.name);
			s.Execute (this, newState);
			//Debug.Log ("entering "+newState.name);
			newState.OnStateEnter();
			//Debug.Log ("update finished");
			return newState;
		}
		else{
			//Debug.LogError ("malheureusement je trouve pas l'automate");
			return this;
		}
	}

	// fonctions concernant les tests
	public void AddTimer(float value, Transition transition){
		timerTests.Add(new KeyValuePair<float, Transition>(value, transition));
		timerInits.Add(new KeyValuePair<float, Transition>(value, transition));
	}

	public void AddTest(System.Predicate<TestContext> test, Transition transition){
		if (controller.DoesTransitionExists (this, transition)){
			tests.Add (new KeyValuePair<System.Predicate<TestContext>, Transition>(test, transition));
		} else{
			//Debug.LogError ("transition "+transition+" inexistante pour l'etat "+this);
		}
	}

	public BaseState StateUpdate(){
		// on regarde si un tests a ete reussi
		for(int i=0 ; i<tests.Count ; i++){
			if(tests[i].Key(context)){
				//Debug.Log ("test "+i+" reussi");
				return DoTransition(tests[i].Value);
			}
		}

		// sinon on regarde si un des timers est termine
		for(int i=0 ; i<timerTests.Count ; i++){
			KeyValuePair<float, Transition> pair = timerTests[i];
			float newKey = pair.Key - Time.deltaTime;
			if(newKey <= 0){
				//Debug.Log ("fini chrono !");
				return DoTransition(pair.Value);
			}
			else{
				timerTests[i] = new KeyValuePair<float, Transition>(newKey, pair.Value);
			}
		}

		// et sinon on remet les triggers a 0 et on ne change pas d'etat
		context.ResetTriggers();
		return this;
	}

	public override string ToString(){
		return name;
	}

	public void ResetTimers(){
		timerTests = new List<KeyValuePair<float, Transition>>();
		foreach(KeyValuePair<float, Transition> pair in timerInits){
			timerTests.Add (pair);		
		}
	}

}


public class Transition{
	public delegate void transType(BaseState orig, BaseState dest);
	
	public string name;
	public transType transitionFunction;
	
	public Transition(string nam){
		name = nam;
		transitionFunction = null;
	}
	
	public void Execute(BaseState orig, BaseState dest){
		if (transitionFunction != null){
			transitionFunction(orig, dest);
		}
	}
}
