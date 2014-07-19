using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestContext{

	public Dictionary<string, float> floatContext;
	public Dictionary<string, bool> boolContext;
	public Dictionary<string, bool> triggerContext;

	public TestContext(){
		floatContext = new Dictionary<string, float>();
		boolContext = new Dictionary<string, bool>();
		triggerContext = new Dictionary<string, bool>();
	}

	public void SetBool(string name, bool val){
		boolContext[name] = val;
	}

	public bool GetBool(string name){
		try{
			return boolContext[name];
		} catch(KeyNotFoundException) {
//			Debug.LogError ("bool variable not found : "+name);
			return false;
		}
	}

	public void SetFloat(string name, float val){
		floatContext[name] = val;
	}

	public float GetFloat(string name){
		try{
			return floatContext[name];
		} catch(KeyNotFoundException){
//			Debug.LogError ("float variable not found : "+name);
			return 0f;
		}
	}

	public void SetTrigger(string name){
		triggerContext[name] = true;
	}

	public bool GetTrigger(string name){
		try{
			return triggerContext[name];
		} catch(KeyNotFoundException) {
			return false;
		}
	}

	public void ResetTriggers(){
		triggerContext.Clear ();
	}

}
