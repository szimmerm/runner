using UnityEngine;
using System.Collections;

/// <summary>
/// Logique du personnage au sol
/// </summary>
public class ZloyGround : PlayerController {

	private FireScript CurrentWeapon;
	private bool firing;

	// Use this for initialization
	void Start () {
		// on recupere la premiere arme activee 
		CurrentWeapon = GetComponentInChildren<FireScript>();
	}
	
	// Update is called once per frame
	void Update () {
		SetDirection();
		if(Input.GetAxis ("Fire") != 0)
			CurrentWeapon.Fire ();
	}

	void FixedUpdate(){
		UpdateGround();
		ApplyHorizontalMove();
	}
}
