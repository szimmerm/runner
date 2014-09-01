// libs
using UnityEngine;
using System.Collections;
	

[RequireComponent (typeof(HorizontalMove))]
[RequireComponent (typeof(CanJump))]
[RequireComponent (typeof(AirHeightControl))]
[RequireComponent (typeof(AirHorizontalControl))]
[RequireComponent (typeof(DisableHorizontalMove))]
[RequireComponent (typeof(WhipState))]

public class DameMachine : MonoBehaviour {

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
			GetComponent<CanJump>(), 
//		    GetComponent<HorizontalMove>(),
		    GetComponent<WhipState>()
			});

		BaseState airDirected = EnablingStateBuild("airDirected", new ControlledComponent[]{
			GetComponent<AirHeightControl>(),
//			GetComponent<AirHorizontalControl>(),
			GetComponent<WhipState>()
			});
	
		BaseState groundWhip = EnablingStateBuild("groundWhip", new ControlledComponent[]{
			GetComponent<DisableHorizontalMove>()
			});

		BaseState airNeutral = EnablingStateBuild("airNeutral", new ControlledComponent[]{
//			GetComponent<HorizontalMove>(),
			GetComponent<AirHeightControl>(),
			GetComponent<WhipState>()
			});

		BaseState swimming = EnablingStateBuild ("swimming", new ControlledComponent[]{
//			GetComponent<HorizontalMove>(),
			GetComponent<CanJump>()
			});

		// creation des transitions
		Transition land = new Transition("land");
		Transition directedJump = new Transition("directed jump");
		Transition neutralJump = new Transition("neutral jump");
		Transition finished = new Transition("finished");
		Transition whip = new Transition("whip");

		// la transition dans l'eau modifie la physique
		Transition swim = new Transition ("swim");
		swim.transitionFunction = (orig, dest) => {
				rigidbody2D.drag = 8;
				CanJump jumpScript = GetComponent<CanJump> ();
				jumpScript.jumpImpulse = 300;
			Debug.Log ("swim transition");
			};

		// la transition hors de l'eau modifie la physique
		Transition outOfWater = new Transition ("out of water");
		outOfWater.transitionFunction = (orig, dest) => {
				rigidbody2D.drag = 0;
				CanJump jumpScript = GetComponent<CanJump>();
				jumpScript.jumpImpulse = 165;
			};


		//public delegate void transType(BaseState orig, BaseState dest);

/*
		Transition.transType func = (orig, dest) => {
			Debug.LogError ("finiwhip !");
			return;
		};
*/


		// creation des transitions labelisees
		controller.AddTransition (airDirected, land, ground);
		controller.AddTransition (airNeutral, land, ground);
		controller.AddTransition (ground, directedJump, airDirected);
		controller.AddTransition (ground, whip, groundWhip);
		controller.AddTransition (groundWhip, finished, ground);
		controller.AddTransition (ground, neutralJump, airNeutral);
		controller.AddTransition (airNeutral, swim, swimming);
		controller.AddTransition (airDirected, swim, swimming);
		controller.AddTransition (swimming, outOfWater, airNeutral);

		// creation des tests qui declenchent les transitions
		System.Predicate<TestContext> test;

		// creation du test pour passer du sol en l'air
		test = (ctxt) => {return ctxt.GetTrigger ("landTrigger");};
		airDirected.AddTest (test, land);
		airNeutral.AddTest (test, land);

//		test = (ctxt) => {return (!Interval.InInterval (ctxt.GetFloat("yVelocity"), 0.01F));};
//		ground.AddTest(test, "neutralfall");

		test = (ctxt) => {
			bool testx = ctxt.GetTrigger ("jump") && (!Interval.InInterval (ctxt.GetFloat ("xVelocity"), 0.01F));
//			Debug.Log ("resultat du test de saut orienté : "+testx);
			if (testx){
//				Debug.LogError ("saut orienté reussi ; velocity : " + ctxt.GetFloat ("xVelocity"));
			}
			return testx;};
		ground.AddTest(test, directedJump);

		test = (ctxt) => {
			bool res = ctxt.GetTrigger ("jump") && (Interval.InInterval (ctxt.GetFloat ("xVelocity"), 0.01F));
//			Debug.Log ("resultat du saut droit : "+res);
			if (res){
//				Debug.LogError ("saut droit reussi ; velocity : "+ctxt.GetFloat ("xVelocity"));
			}
			return res;};
		ground.AddTest(test, neutralJump);

		test = (ctxt) => {
			return (ctxt.GetBool("grounded"));
		};
		ground.AddTest (test, neutralJump);
		
		test = (ctxt) => {
				bool res =  ctxt.GetTrigger ("whip");
				if (res){
					//Debug.LogError ("whipping");
				} 
				return res;
			};
		ground.AddTest (test, whip);

		test = (ctxt) => {
				return ctxt.GetTrigger ("swim");
			};
		airNeutral.AddTest (test, swim);
		airDirected.AddTest (test, swim);

		test = (ctxt) => {
				return ctxt.GetTrigger ("out");
			};
		swimming.AddTest (test, outOfWater);

		// creation des tests pour groundWhip
		groundWhip.AddTimer (0.3F, finished);

		// mise en place de l'etat de base
		activeState = airNeutral;
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

