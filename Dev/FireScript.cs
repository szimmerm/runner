using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
	public Transform bullet;

	private float rateOfFire = 10;
	private bool button;
	private float currentCooldown = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown > 0) {
			currentCooldown -= Time.deltaTime;
		}

		button = Input.GetButton ("Fire");
		if (button && currentCooldown <= 0) {
			Shoot();
			currentCooldown = 1/rateOfFire;
		}
	}

	void Shoot() {
		Transform bul = (Transform) Instantiate(bullet);
		float modif = Random.value * 0.3f - 0.15f;
		Vector3 position = transform.position;
		position.y += modif;
		bul.position = position;
		Destroy(bul.gameObject, 1.0f);
	}
}
