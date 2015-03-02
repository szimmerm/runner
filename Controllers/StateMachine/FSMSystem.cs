using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Un automate a etat fini generique. Il est represente sous la forme d'une paire (E, E x R x E) ou
/// E est un ensemble d'etat
/// R un ensemble de transitions
/// Un triplet (e1, r, e2) decrit une transition de e1 vers e2 par l'action r
/// </summary>
public class StateManager<State, Transition>{
	private HashSet<State> states;
	protected Dictionary<State, Dictionary<Transition, State>> transitions;

	public State currentState{ get; set;}
	public string name;

	public StateManager(){
		states = new HashSet<State>();
		transitions = new Dictionary<State, Dictionary<Transition, State>>();
	}

/// <summary>
/// Ajoute l'etat <paramref name="s" /> a l'automate et le designe comme etat courant
/// </summary>
/// <param name="s">Etat a ajouter</param>
	public void SetDefaultState(State s){
		AddState(s);
		currentState = s;
	}

	/// <summary>
	/// Ajoute l'etat <paramref name="s" /> a l'automate
	/// </summary>
	/// <param name="s">Etat a ajouter</param>
	public void AddState(State s){	
		if (!states.Contains(s)){
			states.Add (s);
			transitions[s] = new Dictionary<Transition, State>();
		}
	}

	/// <summary>
	/// Ajoute la liste d'etats <paramref name="statesList" /> a l'automate
	/// </summary>
	/// <param name="statesList">Etats a ajouter</param>
	public void AddStates(List<State> statesList){
		foreach (State s in statesList){
			AddState(s);
		}
	}

	/// <summary>
	/// Rajoute une transition a l'automate
	/// </summary>
	/// <param name="origin">Etat d'origine</param>
	/// <param name="t">transition a effectuer</param>
	/// <param name="final">Etat d'arrivee</param>
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

	/// <summary>
	/// Execute la transition <paramref name"t" /> a partir de l'etat <paramref name="s" /> et retourne l'etat obtenu
	/// </summary>
	/// <returns>L'etat obtenu apres la transition</returns>
	/// <param name="s">L'etat de depart</param>
	/// <param name="t">La transition a effectuer</param>
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

	/// <summary>
	/// Execute la transition <paramref name="t"/> à partir de l'etat courant
	/// </summary>
	/// <param name="t">transition a effectuer</param>
	public void DoTransition(Transition t){
		State newState = DoStateTransition(currentState, t);
		if(newState == null){
			Debug.LogError ("erreur dans la transition, etat courant inchange");
			return;
		} else {
			currentState = newState;
		}
	}

	/// <summary>
	/// Verifie si l'etat <paramref name="s"/> peut effectuer la transition <paramref name="t"/> 
	/// </summary>
	/// <returns><c>true</c> si la transition est possible <c>false</c> sinon.</returns>
	/// <param name="s">etat de depart.</param>
	/// <param name="t">transition a effectuer.</param>
	public bool DoesTransitionExists(State s, Transition t){
		try{
			return transitions[s].ContainsKey(t);
		} catch(KeyNotFoundException){
			return false;
		}
	}

}