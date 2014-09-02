﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Gestion de l'arme a feu
/// </summary>
public class FireScript : MonoBehaviour {
	/// <summary>
	/// Projectile tire
	/// </summary>
	public Transform bullet;

	/// <summary>
	/// Vitesse de tir
	/// </summary>
	public float rateOfFire = 10;

	/// <summary>
	/// Refroidissement courant de l'arme
	/// </summary>
	private float currentCooldown = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown > 0) {
			currentCooldown -= Time.deltaTime;
		}
	}

	/// <summary>
	/// Effectue les tests de validite de creation du tir puis le cree
	/// </summary>
	public void Fire() {
		if (currentCooldown <= 0) {
			SpawnShoot();
			currentCooldown = 1/rateOfFire;
		}
	}

	/// <summary>
	/// Instancie le tir
	/// </summary>
	private void SpawnShoot() {
		Transform bul = (Transform) Instantiate(bullet);

		// les tirs ne partent pas pile du canon pour faire un effet de dispersion
		float modif = Random.value * 0.3f - 0.15f;
		Vector3 position = transform.position;
		position.y += modif;
		bul.position = position;

		// duree de vie du tir
		Destroy(bul.gameObject, 1.0f);
	}
}
