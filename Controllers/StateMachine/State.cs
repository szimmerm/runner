using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Surcouche des etats des machines a etat fini permettant des transitions sur des conditions au lieu de simplement des signaux
/// </summary>
public class BaseState{

	/// <summary>
	/// L'automate controlant l'etat
	/// </summary>
	public StateManager<BaseState, Transition> controller;

	/// <summary>
	/// Le contexte de test, contenant les valeurs des variables internes de l'automate
	/// </summary>
	public TestContext context;

	/// <summary>
	/// Les tests booleens declenchant les transitions
	/// </summary>
	public List<KeyValuePair<System.Predicate<TestContext>, Transition>> tests;

	/// <summary>
	/// Les timers declenchant les transitions minutees
	/// </summary>
	public List<KeyValuePair<float, Transition>> timerTests;

	/// <summary>
	/// Valeur de depart des timers. Ils se declenchent quand une des valeurs arrive a 0
	/// </summary>
	private List<KeyValuePair<float, Transition>> timerInits;

	/// <summary>
	/// Nom de l'etat, pour des besoins de lisibilite au debug
	/// </summary>
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

	/// <summary>
	/// Actions a effectuer quand on arrive dans l'etat
	/// </summary>
	virtual public void OnStateEnter(){

	}

	/// <summary>
	/// Actions a effectuer quand on sort de l'etat
	/// </summary>
	virtual public void OnStateExit(){
		ResetTimers();
	}

	/// <summary>
	/// Execution de la transition <paramref name="s"/>.
	/// </summary>
	/// <returns>L'etat obtenu apres transition.</returns>
	/// <param name="s">la transition a effectuer.</param>
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

	/// <summary>
	/// Rajoute un timer avec un delai de <paramref name="value"/> qui 
	/// execute la transition <paramref name="transition"/> quand il arrive a 0
	/// </summary>
	/// <param name="value">duree du timer.</param>
	/// <param name="transition">transition a effectuer.</param>
	public void AddTimer(float value, Transition transition){
		timerTests.Add(new KeyValuePair<float, Transition>(value, transition));
		timerInits.Add(new KeyValuePair<float, Transition>(value, transition));
	}

	/// <summary>
	/// Rajoute un test <paramref name="test"/> executant la 
	/// transition <paramref name="transition"/> quand il est reussi
	/// </summary>
	/// <param name="test">Test a effectuer.</param>
	/// <param name="transition">Transition a executer quand le test est reussi.</param>
	public void AddTest(System.Predicate<TestContext> test, Transition transition){
		if (controller.DoesTransitionExists (this, transition)){
			tests.Add (new KeyValuePair<System.Predicate<TestContext>, Transition>(test, transition));
		} else{
			//Debug.LogError ("transition "+transition+" inexistante pour l'etat "+this);
		}
	}

	/// <summary>
	/// Effectue la serie de tests relatifs a l'etat courant et renvoit un nouvel 
	// etat si un des test est reussi
	/// </summary>
	/// <returns>Le nouvel etat apres verification des tests.</returns>
	public BaseState StateUpdate(){
		// on regarde si un tests a ete reussi
		for(int i=0 ; i < tests.Count ; i++){
			if(tests[i].Key(context)){
				//Debug.Log ("test "+i+" reussi");
				return DoTransition(tests[i].Value);
			}
		}

		// si aucun test n'est reussi, on met a jour les timers et on les verifie
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
		return this; // pourquoi this et pas null ?
	}

	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="BaseState"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="BaseState"/>.</returns>
	public override string ToString(){
		return name;
	}

	/// <summary>
	/// Remise a zero des timers
	/// </summary>
	public void ResetTimers(){
		timerTests = new List<KeyValuePair<float, Transition>>();
		foreach(KeyValuePair<float, Transition> pair in timerInits){
			timerTests.Add (pair);		
		}
	}

}

/// <summary>
/// Surcouche des transitions, pouvant appliquer des modifications sur les etats de
/// depart et d'arrivee quand elles sont executees
/// </summary>
public class Transition{
	/// <summary>
	/// Type abstrait pour les fonctions de transition
	///</summary>
	/// <param name="orig">Etat d'origine de la transition.</param>
	/// <param name="dest">Etat d'arrivee de la transition.</param>
	public delegate void transType(BaseState orig, BaseState dest);
	
	/// <summary>
	/// nom de la transition, pour des besoins de debug
	/// </summary>
	public string name;

	/// <summary>
	/// fonction a executer lors de la transition
	/// </summary>
	public transType transitionFunction;
	
	public Transition(string nam){
		name = nam;
		transitionFunction = null;
	}
	
	/// <summary>
	/// Applique l'effet de la fonction de transition sur les etats de depart
	/// et d'arrivee
	/// </summary>
	/// <param name="orig">Etat d'origine.</param>
	/// <param name="dest">Etat d'arrivee.</param>
	public void Execute(BaseState orig, BaseState dest){
		if (transitionFunction != null){
			transitionFunction(orig, dest);
		}
	}
}

/// <summary>
/// Surcouche de BaseState, activant et desactivant automatiquement des composants
/// </summary>
public class EnablingState : BaseState{
	/// <summary>
	/// Les composants controles par l'etat.
	/// </summary>
	public List<MonoBehaviour> components = new List<MonoBehaviour>();
	
	public EnablingState(string nam, TestContext ctxt, StateManager<BaseState, Transition> control) : 
		base(nam, ctxt, control){
		components = new List<MonoBehaviour>();
	}	

	/// <summary>
	/// rajoute le composant <paramref name="component"/>  sous le controle de l'etat
	///</summary>	
	/// <param name="component">Composant a mettre sous le controle de l'etat.</param>
	public void AddComponent(MonoBehaviour component){
		//		Debug.Log ("component added : "+component.ToString ());
		components.Add (component);
	}
	
	/// <summary>
	/// Actions a effectuer quand on sort de l'etat
	/// </summary>
	public override void OnStateExit(){
		base.OnStateExit ();
		foreach(MonoBehaviour component in components){
			component.enabled = false;
			
		}
	}
	
	/// <summary>
	/// Actions a effectuer quand on arrive dans l'etat
	/// </summary>
	public override void OnStateEnter(){
		context.ResetTriggers();
		foreach(ControlledComponent component in components){
//			component.context = context;
			component.enabled = true;
		}	
	}
}

