using UnityEngine;
using System.Collections;

public class OldDameMachine : MonoBehaviour {

	// valeurs des differents parametres du perso
	public float jumpImpulse = 200;
	public float lowSpeed = 1.2F;

	private Animator animator; // animator du personnage
	private StateManager<FSMState, int> controller; // character controller

	void Awake(){
		animator = GetComponent<Animator>();
		controller = new StateManager<FSMState, int>();

		// default state creation
		OldOnGroundState s = gameObject.AddComponent<OldOnGroundState>() as OldOnGroundState;
		s.animator = animator;
		s.jumpImpulse = jumpImpulse;
		s.machine = controller;
		controller.SetDefaultState(s);

		OldAirState air = gameObject.AddComponent<OldAirState>() as OldAirState;
		air.animator = animator;
		air.machine = controller;
		air.lowSpeed = lowSpeed;
		controller.AddState (air);
		controller.AddTransition (s, 1, air);
		controller.AddTransition (air, 1, s);
	}

	// Use this for initialization
	void Start () {
		controller.currentState.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
