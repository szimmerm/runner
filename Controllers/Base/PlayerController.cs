using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PlayerValues))]
[RequireComponent (typeof(SeRetourner))]
/// <summary>
/// Classe generique regissant les mouvements du personnage controlle par le joueur
/// </summary>
public class PlayerController : GenericMove {

	protected PlayerValues pvalues; // valeurs internes du personnage, etend object values

	protected override void Awake() {
		base.Awake ();
		pvalues = GetComponent<PlayerValues>();
		values = pvalues;
	}

	/// <summary>
	/// Resolution du mouvement horizontal
	/// </summary>
	public void ApplyHorizontalMove(){
		if (values.direction.x != 0){
			AccelerateWithCap(pvalues.acceleration, 0, pvalues.maxSpeed, 100000000000000000);
		}
		else{
			DecreaseSpeed(pvalues.horizontalFriction);
		}
	}

	/// <summary>
	/// Met a jour la valeur de la direction du mouvement
	/// </summary>
	public void SetDirection() {
		values.direction = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));	
//		values.context.SetFloat ("xDirection", values.direction.x);
//		values.context.SetFloat ("yDirection", values.direction.y);
	}

	/// <summary>
	/// Verifie si le personnage est au sol
	/// </summary>
	/// <returns><c>true</c>, si le personnage est au sol, <c>false</c> sinon.</returns>
	public bool IsOnGround() {
		RaycastHit2D [] hits = Physics2D.LinecastAll (
			transform.position 	
			,pvalues.GetGroundTrigger().transform.position
			, 1 << LayerMask.NameToLayer("Walls") 
		);
		foreach(RaycastHit2D hit in hits){
			if (hit.collider != null) {
				if (!Physics2D.GetIgnoreCollision(GetComponent<Collider2D>(), hit.collider)) {
					pvalues.groundCollider = hit.collider;
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>
	/// Met a jour la valeur du sol
	/// </summary>
	protected void UpdateGround(){
		// on ne met la vraie valeur que si l'objet n'est pas entrain de monter, cause de la gestion des plateformes
		if (GetComponent<Rigidbody2D>().velocity.y <= 0) {
			pvalues.onGround = IsOnGround();
		} else {
			pvalues.onGround = false;
		}
		/*
		pvalues.animator.SetBool ("grounded", pvalues.onGround);
		pvalues.context.SetBool ("onGround", pvalues.onGround);
		Debug.Log ("updating ground : "+pvalues.onGround);
		*/
	}

	protected void SetVertical(){
		values.context.SetFloat ("vertical", Input.GetAxis ("Vertical"));
	}
}
