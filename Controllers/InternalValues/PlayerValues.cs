﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// Valeurs internes du personnage. On utilise une classe separee pour qu'elles soient communes a tout les composants du personnage
/// </summary>
public class PlayerValues : ObjectValues {
	
	public float maxSpeed = 2f; // vitesse horizontale maximale de l'objet
	public float acceleration = 20f; // acceleration horizontale de l'objet
	public float horizontalFriction = 20f; // ralentissement de l'objet lorsqu'on ne touche a rien
	public bool onGround = false; // objet au sol ?
	public bool climbing = false; // objet grimpant ? (utile ? devrait etre integre a l'automate d'etat)
	public bool isFiring = false; // objet entrain de tirer ?

	public Transform groundTriggerPrefab; // qui instancier comme detecteur de sol

	// le detecteur de sol, cree au lancement du jeu
	public Vector2 groundColliderDistance;
	private Transform groundTrigger;
	public Collider2D groundCollider; // element sur lequel marche le personnage

	private List<Animator> animators;

	protected override void Awake() {
		base.Awake();
		groundTrigger = (Transform)Instantiate (groundTriggerPrefab);	
		groundTrigger.parent = transform;
		groundTrigger.localPosition = groundColliderDistance;
		animators = new List<Animator>(GetComponentsInChildren<Animator>());
	}

	public Transform GetGroundTrigger(){
		return groundTrigger;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "ladderTrigger") {
			float ladders = context.GetFloat ("ladderValue") + 1;
			context.SetFloat ("ladderValue", ladders);
		} else if (other.tag == "ladderTop") {
			float ladders = context.GetFloat ("ladderTopValue") + 1;
			context.SetFloat ("ladderTopValue", ladders);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "ladderTrigger") {
			float ladders = context.GetFloat ("ladderValue") - 1;
			context.SetFloat ("ladderValue", ladders);
		} else if (other.tag == "ladderTop") {
			float ladders = context.GetFloat ("ladderTopValue") - 1;
			context.SetFloat ("ladderTopValue", ladders);
		}
	}

	private void resetTriggers() {
		isFiring = false;
	}

	void Update() {
		resetTriggers();
	}

	void LateUpdate(){
		AnimatorConstantUpdate();
	}

	void AnimatorConstantUpdate(){
		Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
		foreach (Animator anim in animators) {
			anim.SetFloat ("xVelocity", velocity.x);
			anim.SetFloat ("yVelocity", velocity.y);
			// on ne modifie onGround que si on ne monte pas pour eviter
			// les multiples sauts lors de l'ascension
		}
		if (GetComponent<Rigidbody2D>().velocity.y >= 0) {
			context.SetBool ("onGround", onGround);
			foreach(Animator anim in animators) {
				anim.SetBool ("onGround", onGround);
				anim.SetBool ("isFiring", isFiring);
			}
		}
	}
}
