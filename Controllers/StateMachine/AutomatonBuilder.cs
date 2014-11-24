using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(ObjectValues))]
/// <summary>
/// Classe de base fabriquant automatiquement l'automate de controle d'un element a partir de sa description
/// </summary>
public class AutomatonBuilder : MonoBehaviour {
		
	public string activeStateName = "";

	private StateManager<BaseState, Transition> controller; // character controller
	public TestContext context; // valeurs utilisees dans les tests par les transitions
	private BaseState activeState;

	private Dictionary<string, BaseState> stateNames = new Dictionary<string, BaseState>(); // dictionnaire faisant le lien entre les noms d'etats et les etats

	// Methodes a remplir par la classe fille
	
	/// <summary>
	/// Fonction abstraite appelee lors de la creation des etats de l'automate
	/// A remplir dans la classe fille
	/// </summary>
	public virtual void BuildStates (){
	}
	
	/// <summary>
	/// Fonction abstraite appelee lors de la creation des transitions de l'automate
	/// A remplir dans la classe fille
	/// </summary>
	public virtual void BuildTransitions() {
		
	}

	public virtual void BuildContext(TestContext ctxt){

	}
	
	/// <summary>
	/// Defini l'etat de depart de l'automate.
	/// </summary>
	/// <param name="s">Etat de depart.</param>
	protected void SetBaseState(string name){
		activeState = stateNames[name];
	}
	
	// primitives de remplissage des etats et des transitions

	/// <summary>
	/// Fonction creant un etat contenant un MonoBehaviour
	/// </summary>
	/// <param name="name">Le nom de l'etat.</param>
	/// <param name="stateContent">Le MonoBehaviour contenu dans l'etat.</param>
	protected void AddState(string name, MonoBehaviour stateContent){
		EnablingState state = new EnablingState(name, context, controller);
		stateNames.Add (name, state);
		state.AddComponent (stateContent);
	}

	/// <summary>
	/// Renvoit un etat a partir de son nom ; utile pour les transitions
	/// </summary>
	/// <returns>L'etat lie au nom.</returns>
	/// <param name="name">Nom de l'etat.</param>
	protected BaseState GetStateFromName(string name) {
		return stateNames[name];
	}

	/// <summary>
	/// Ajoute une transition a l'automate
	/// </summary>
	/// <param name="orig">Etat de depart</param>
	/// <param name="name">Nom de la transition</param>
	/// <param name="dest">Etat d'arrivee</param>
	/// <param name="test">Test declenchant la transition</param>
	protected void AddSimpleTransition(string orig, string name, string dest, System.Predicate<TestContext> test){
		Transition transition = new Transition(name);
		BaseState originState = GetStateFromName(orig);
		BaseState destState = GetStateFromName(dest);
		controller.AddTransition (originState, transition, destState);
		originState.AddTest (test, transition);
	}

	/// <summary>
	/// Ajoute une transition effectuant des modifications sur ces etats de depart et d'arrivee
	/// </summary>
	/// <param name="orig">Etat de depart.</param>
	/// <param name="name">Nom de la transition.</param>
	/// <param name="dest">Etat d'arrivee.</param>
	/// <param name="test">Test declenchant la transition.</param>
	/// <param name="func">Modification des etats de depart et d'arrivee.</param>
	protected void AddActionTransition(string orig, string name, string dest, System.Predicate<TestContext> test, Transition.transType func){
		Transition transition = new Transition(name);
		BaseState originState = GetStateFromName(orig);
		BaseState destState = GetStateFromName(dest);
		controller.AddTransition (originState, transition, destState);
		originState.AddTest (test, transition);
		transition.transitionFunction = func;
	}

	// Infrastructure de l'automate en tant que MonoBehaviour
	void Awake(){

		// machine creation
		controller = new StateManager<BaseState, Transition>();
		
		// creation du context
		context = GetComponent<ObjectValues>().context;
		
		// creation des etats
		BuildStates();

		// creation des transitions
		BuildTransitions();

		// creation des valeurs par defaut du contexte
		BuildContext (context);
	}
	
	// Use this for initialization
	void Start () {
		activeState.OnStateEnter();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//		Debug.Log ("dame update");
		//		Debug.Log ("xVelocity : "+context.GetFloat ("xVelocity"));
		activeState = activeState.StateUpdate();
		activeStateName = activeState.name;
	}
}
	