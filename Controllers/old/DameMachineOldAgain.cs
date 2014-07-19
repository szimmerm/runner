//using UnityEngine;
//using System.Collections;
//
//[RequireComponent (typeof(HorizontalMove))]
//[RequireComponent (typeof(CanJump))]
//[RequireComponent (typeof(AirHeightControl))]
//[RequireComponent (typeof(AirHorizontalControl))]
//[RequireComponent (typeof(DisableHorizontalMove))]
//[RequireComponent (typeof(WhipState))]
//
//public class DameMachine : MonoBehaviour {
//
//	public string activeStateName = "";
//
//	// valeurs des differents parametres du perso
////	public float jumpImpulse = 200;
////	public float lowSpeed = 1.2F;
//
////	private Animator animator; // animator du personnage
//	private StateManager<State, string> controller; // character controller
//	private TestContext context; // valeurs utilisees dans les tests par les transitions
//
//	void Awake(){
//
//		// recuperation de l'animateur
//		//animator = GetComponent<Animator>();
//
//		// machine creation
//		controller = new StateManager<State, string>();
//
//		// creation du context
//		context = new TestContext();
//
//		// creation des etats
//		State ground = StateBuild("ground", new ControlledComponent[]{
//			GetComponent<CanJump>(), 
//		    GetComponent<HorizontalMove>(),
//		    GetComponent<WhipState>()
//			});
//
//		State airDirected = StateBuild("airDirected", new ControlledComponent[]{
//			GetComponent<AirHeightControl>(),
//			GetComponent<AirHorizontalControl>(),
//			GetComponent<WhipState>()
//			});
//	
//		State groundWhip = StateBuild("groundWhip", new ControlledComponent[]{
//			GetComponent<DisableHorizontalMove>()
//			});
//
//		State airNeutral = StateBuild("airNeutral", new ControlledComponent[]{
//			GetComponent<HorizontalMove>(),
//			GetComponent<AirHeightControl>(),
//			GetComponent<WhipState>()
//			});
//
//		// creation des transitions labelisees
//		controller.AddTransition (airDirected, "land", ground);
//		controller.AddTransition (airNeutral, "land", ground);
//		controller.AddTransition (ground, "directedfall", airDirected);
//		controller.AddTransition (ground, "whip", groundWhip);
//		controller.AddTransition (groundWhip, "finished", ground);
//		controller.AddTransition (ground, "neutralfall", airNeutral);
//
//		// creation des tests qui declenchent les transitions
//		System.Predicate<TestContext> test;
//
//		// creation du test pour passer du sol en l'air
//		test = (ctxt) => {return ctxt.GetTrigger ("landTrigger");};
//		airDirected.AddTest (test, "land");
//		airNeutral.AddTest (test, "land");
//
////		test = (ctxt) => {return (!Interval.InInterval (ctxt.GetFloat("yVelocity"), 0.01F));};
////		ground.AddTest(test, "neutralfall");
//
//		test = (ctxt) => {
//			bool testx = ctxt.GetTrigger ("jump") && (!Interval.InInterval (ctxt.GetFloat ("xVelocity"), 0.01F));
///*			Debug.Log ("resultat du test de saut orienté : "+testx);
//			if (testx){
//				Debug.LogError ("saut orienté reussi ; velocity : " + ctxt.GetFloat ("xVelocity"));
//			}
//*/			return testx;};
//		ground.AddTest(test, "directedfall");
//
//		test = (ctxt) => {
//			bool res = ctxt.GetTrigger ("jump") && (Interval.InInterval (ctxt.GetFloat ("xVelocity"), 0.01F));
///*			Debug.Log ("resultat du saut droit : "+res);
//			if (res){
//				Debug.LogError ("saut droit reussi ; velocity : "+ctxt.GetFloat ("xVelocity"));
//			}
//*/			return res;};
//		ground.AddTest(test, "neutralfall");
//		
//		test = (ctxt) => {return ctxt.GetTrigger ("whip");};
//		ground.AddTest (test, "whip");
//
//		// creation des tests pour groundWhip
//		groundWhip.AddTimer (0.3F, "finished");
//
//
//
//		// mise en place de l'etat de base
//		controller.SetDefaultState (airNeutral);
//	}
//
//	// Use this for initialization
//	void Start () {
//		controller.currentState.EnableComponents ();
//	}
//	
//	// Update is called once per frame
//	void LateUpdate () {
////		Debug.Log ("dame update");
////		Debug.Log ("xVelocity : "+context.GetFloat ("xVelocity"));
//		controller.currentState.StateUpdate();
//		activeStateName = controller.currentState.name;
//	}
//
//	
//	public State StateBuild(string name, ControlledComponent[] components){
//		State state = new State(name, context, controller);
//		foreach(ControlledComponent component in components){
//			state.AddComponent (component);
//		}
//		controller.AddState (state);
//		return state;		
//	}
//}
