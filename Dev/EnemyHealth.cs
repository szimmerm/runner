using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {
	public bool hitImpact = false;
	public float blinkTime = 0.001f;

	public ParticleSystem blood;		
	public Transform frontBloodPosition;
	public Transform rearBloodPosition;

	protected override void OnHit(Collider2D coll) {
		KnockBack(0.5f, coll.gameObject.GetComponent<ObjectValues>());
//			StartCoroutine(FreezeOnHit());
		StartCoroutine(makeCharacterBlink());
		BuildBlood(coll);
		values.animator.SetTrigger("hurt");
	}

	protected override void OnDeath(Collider2D coll){
		if (hasCadavre) {
			values.animator.SetTrigger ("dead");
			DisableCharacter();
		} else {
			Destroy(gameObject);
		}
	}

/*		
	IEnumerator FreezeOnHit() {
		if(!freezing) {
			freezing = true;
			float oldTimeScale = Time.timeScale;
			Time.timeScale = 0.000001f;
			yield return new WaitForSeconds(0.03f * Time.timeScale);
			Time.timeScale = oldTimeScale;
			freezing = false;
		}
	}
*/
	
	protected IEnumerator makeCharacterBlink() {
		foreach(BlinkOnCommand blink in GetComponentsInChildren<BlinkOnCommand>()) {
			blink.swapMaterial();
		}
		yield return new WaitForSeconds(blinkTime);
		foreach(BlinkOnCommand blink in GetComponentsInChildren<BlinkOnCommand>()) {
			blink.swapMaterial();
		}
	}
	
	protected void KnockBack(float knockValue, ObjectValues othervalues) {
		Vector3 knockBack = knockValue * othervalues.direction;
		transform.position += knockBack;
	}
	
	protected void BuildBlood(Collider2D bulletCollider) {
		bool val = (values.direction.x * bulletCollider.gameObject.GetComponent<ObjectValues>().direction.x == 1);
		Transform particlesPosition = val ? frontBloodPosition : rearBloodPosition;
		GameObject giclette = (GameObject) Instantiate(blood, particlesPosition.position, particlesPosition.rotation);
		giclette.GetComponent<ParticleSystem>().Play ();
		Destroy(giclette, 1.0f);	
	}
		
	protected void DisableCharacter(){
		values.body.velocity = new Vector3(0, 0, 0);
		values.body.isKinematic = true;
		foreach(MonoBehaviour comp in GetComponentsInChildren<MonoBehaviour>()) { // pas MonoBehaviour parce qu'on veut bloquer les colliders
			if(comp.GetType() != typeof(SpriteRenderer) && comp.GetType () != typeof(Animator)) {
				comp.enabled = false;
			}
		}
		
		foreach (Collider2D collider in GetComponentsInChildren<Collider2D>()) {
			collider.enabled = false;
		}
	}
}
