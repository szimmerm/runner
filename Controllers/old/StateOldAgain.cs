//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class State{
//	
//	public StateManager<State, string> controller;
//	public List<ControlledComponent> components;
//	public TestContext context;
//	public List<KeyValuePair<System.Predicate<TestContext>, string>> tests;
//
//
////	public List<KeyValuePair<Test<float>, string>> floatTests;
//	public List<KeyValuePair<float, string>> timerTests;
//	private List<KeyValuePair<float, string>> timerInits;
//
///*
//	public Dictionary<string, string> triggers;
//*/
//
//	public string name;
//
//	public State(string nam, TestContext ctxt, StateManager<State, string> control){
//		name = nam;
//		context = ctxt;
//		controller = control;
//
//		// initialisation des composantes
//		components = new List<ControlledComponent>();
////		floatTests = new List<KeyValuePair<Test<float>, string>>();
//		timerTests = new List<KeyValuePair<float, string>>();
//		timerInits = new List<KeyValuePair<float, string>>();
////		triggers = new Dictionary<string, string>();
//		tests = new List<KeyValuePair<System.Predicate<TestContext>, string>>();
//	}
//
//	public void DisableComponents(){
//		foreach(ControlledComponent component in components){
//			component.enabled = false;
//		}
//	}
//
//	public void EnableComponents(){
//		foreach(ControlledComponent component in components){
////			Debug.Log ("Enabling : "+component);
//			component.controller = this;
//			component.enabled = true;
//		}
//
//		// reset des timers
//		timerTests = new List<KeyValuePair<float, string>>();
//		foreach(KeyValuePair<float, string> pair in timerInits){
//			timerTests.Add (pair);
//		}
//
//		// reset des triggers
////		context.ResetTriggers ();
//	}
//	
//	public void DoTransition(string s){
//		if (controller != null){
//			Debug.Log ("transition vers : "+s);
//			controller.DoTransition (s);
//			DisableComponents();
//			controller.currentState.EnableComponents ();
//			controller.currentState.StateUpdate();
//		}
//		else{
//			Debug.LogError ("malheureusement je trouve pas la transition : "+s);
//		}
//	}
//
//	public void AddComponent(ControlledComponent component){
////		Debug.Log ("component added : "+component.ToString ());
//		components.Add (component);
//	}
//
//	
//	// fonctions concernant les tests
//	public void AddTimer(float value, string transition){
//		timerTests.Add(new KeyValuePair<float, string>(value, transition));
//		timerInits.Add(new KeyValuePair<float, string>(value, transition));
//	}
//
//	public void AddTest(System.Predicate<TestContext> test, string transition){
//		if (controller.DoesTransitionExists (this, transition)){
//			tests.Add (new KeyValuePair<System.Predicate<TestContext>, string>(test, transition));
//		} else{
//			Debug.LogError ("transition "+transition+" inexistante pour l'etat "+this);
//		}
//	}
//
///*
//	public void AddFloatTest(Test<float> test, string transition){
//		floatTests.Add(new KeyValuePair<Test<float>, string>(test, transition));
//	}
//*/
//
//
///*
//	public void AddTrigger(string name, string transition){
//		triggers[name] = transition;
//	}
//*/
//
//	public void StateUpdate(){
///*
////		Debug.Log ("state update");
//		for(int i=0 ; i<floatTests.Count ; i++){
//			if(floatTests[i].Key.Execute()){
////				Debug.Log ("test reussi ! En route pour "+floatTests[i].Value);
//				DoTransition(floatTests[i].Value);
//				return;
//			}
//		}
//*/
//
//		// on regarde si un tests a ete reussi
//		for(int i=0 ; i<tests.Count ; i++){
//			if(tests[i].Key(context)){
//				DoTransition(tests[i].Value);
//				return;
//			}
//		}
//
//		// sinon on regarde si un des timers est termine
//		for(int i=0 ; i<timerTests.Count ; i++){
//			KeyValuePair<float, string> pair = timerTests[i];
//			float newKey = pair.Key - Time.deltaTime;
//			if(newKey <= 0){
////				Debug.Log ("fini chrono !");
//				DoTransition(pair.Value);
//				return ;
//			}
//			else{
//				timerTests[i] = new KeyValuePair<float, string>(newKey, pair.Value);
//			}
//		}
//
//		// et sinon on remet les triggers a 0
//		context.ResetTriggers();
//
///*
//		string trig = context.GetTrigger ();
//		if(trig != null){
//			DoTransition(triggers[trig]);
//		}
//*/
//	}
//
//	public override string ToString(){
//		return name;
//	}
//
//}