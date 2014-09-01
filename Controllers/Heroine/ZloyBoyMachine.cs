using UnityEngine;
using System.Collections;

[RequireComponent (typeof(HorizontalMove))]
[RequireComponent (typeof(AirHeightControl))]
[RequireComponent (typeof(CanJump))]

/// <summary>
/// Description de l'automate de controle du personnage joueur
/// </summary>
public class ZloyBoyMachine : MonoBehaviour {
	
	public string activeStateName = "";
	
	// valeurs des differents parametres du perso
	//	public float jumpImpulse = 200;
	//	public float lowSpeed = 1.2F;
	
	private StateManager<BaseState, Transition> controller; // character controller
	public TestContext context; // valeurs utilisees dans les tests par les transitions
	private BaseState activeState;
	//	private Animator animator; // animator du personnage
	
	void Awake(){
		
		// recuperation de l'animateur
		//		animator = GetComponent<Animator>();
		
		// machine creation
		controller = new StateManager<BaseState, Transition>();
		
		// creation du context
		context = new TestContext();
		
		// creation des etats
		BaseState ground = EnablingStateBuild("ground", new ControlledComponent[]{
//			GetComponent<HorizontalMove>(),
			GetComponent<AirHeightControl>(),
			GetComponent<CanJump>()
		});

		
		// mise en place de l'etat de base
		activeState = ground;
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
	
	/// <summary>
	/// Construction d'un etat lie a plusieurs composants
	/// </summary>
	/// <returns>L'etat fabrique.</returns>
	/// <param name="name">Nom de l'etat.</param>
	/// <param name="components">Composants utilises par l'etat.</param>
	public BaseState EnablingStateBuild(string name, ControlledComponent[] components){
		EnablingState state = new EnablingState(name, context, controller);
		//		EnablingState state = new EnablingState();
		//		state.context = context;
		foreach(ControlledComponent component in components){
			state.AddComponent (component);
		}
		
		//		State finalState = new State(name, context, controller, state);
		//		controller.AddState (finalState);
		//		return finalState;		
		return state;
	}
}