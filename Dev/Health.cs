using UnityEngine;
using System.Collections;

public abstract class Health : MonoBehaviour {

	public int hitPoints = 4;
	public bool hasCadavre = false;

	public AudioClip deathSound;
	public AudioClip hitSound;


	protected ObjectValues values;
//	private static bool freezing = false;

	// Use this for initialization
	protected virtual void Start () {
		values = GetComponent<ObjectValues>();
	}

	abstract protected void OnHit(Collider2D coll);
	abstract protected void OnDeath(Collider2D coll);

	protected void PlayHitSound() {
		if (hitSound != null) {
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
		}
	}

	protected void PlayDeathSound(){
		if (deathSound != null) {
			AudioSource.PlayClipAtPoint(deathSound, transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "shoots"){
			Destroy (coll.gameObject); // a modifier pour appeler un Die sur l'objet
			if (--hitPoints <= 0) {
				PlayDeathSound();
				OnDeath(coll);
			}
			if (hitPoints > 0) {
				PlayHitSound();
				OnHit(coll);
			}
		}
	}

}
