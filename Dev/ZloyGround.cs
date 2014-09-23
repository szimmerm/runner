﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Logique du personnage au sol
/// </summary>
public class ZloyGround : PlayerController {

	private float deadZone = 0.001F;

	private FireScript CurrentWeapon;
	private bool firing;
	public float jumpImpulse;

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
		
		// gestion de la hauteur du saut
		float jump = Input.GetAxis ("Jump");
		if(onGround && jump != 0){
			JumpImpulse(jumpImpulse);
		}
		context.SetBool ("onGround", onGround);
	}

}
