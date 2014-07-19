using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager<State, Transition>{
	private HashSet<State> states;
	protected Dictionary<State, Dictionary<Transition, State>> transitions;

	public State currentState{ get; set;}

	public StateManager(){
		states = new HashSet<State>();
		transitions = new Dictionary<State, Dictionary<Transition, State>>();
	}

	public void SetDefaultState(State s){
		AddState(s);
		currentState = s;
	}

	public void AddState(State s){	
		if (!states.Contains(s)){
			states.Add (s);
			transitions[s] = new Dictionary<Transition, State>();
		}
	}

	public void AddStates(List<State> statesList){
		foreach (State s in statesList){
			AddState(s);
		}
	}

	public void AddTransition(State origin, Transition t, State final){
		// on force l'ajout pour etre sur qu'ils sont là ; dans l'ideal faudrait faire le test
		AddState(origin);
		AddState(final);

		Dictionary<Transition, State> localTransitions;

		try{
			localTransitions = transitions[origin];
		} catch (KeyNotFoundException e){
			Debug.Log ("transition non-cree ; ne devrait jamais arriver : "+e.Data.ToString());
			localTransitions = new Dictionary<Transition, State>();
			transitions[origin] = localTransitions;
		}
		transitions[origin][t] = final;

		//Debug.Log ("ajout de la transition "+t+" entre les etats "+origin.ToString ()+" et "+final.ToString());
	}

	public State DoStateTransition(State s, Transition t){
		try {
			return transitions[s][t];
		}
		catch(KeyNotFoundException e){
			//Debug.LogError ("etat pas trouve : "+currentState.ToString ());
			foreach(Transition key in transitions[currentState].Keys){
				Debug.Log ("key : "+key.ToString ());
			}
			Debug.LogError("FSM State Error, transition on "+e.Data.ToString ()+" not found");
			Debug.LogError(e.Message);
			return s;
		}
	}

	public void DoTransition(Transition t){
		State newState = DoStateTransition(currentState, t);
		if(newState == null){
			Debug.LogError ("erreur dans la transition, etat courant inchange");
			return;
		} else {
			currentState = newState;
		}
	}

	public bool DoesTransitionExists(State s, Transition t){
		try{
			return transitions[s].ContainsKey(t);
		} catch(KeyNotFoundException){
			return false;
		}
	}

}