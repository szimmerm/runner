using UnityEngine;
using System.Collections;

/// <summary>
/// Classe generique regissant les mouvements du personnage controlle par le joueur
/// </summary>
public class PlayerController : GenericMove {

	new protected PlayerValues values; // valeurs internes du personnage (verifier syntaxe et comportement ?)
										// on se permet le hiding ici parce que values n'est qu'un cast du values utilise par
										// controlled component

	void Awake() {
		values = GetComponent<PlayerValues>();
	}

	/// <summary>
	/// Resolution du mouvement horizontal
	/// </summary>
	public void ApplyHorizontalMove(){
		if (values.direction != 0){
			AccelerateWithCap(values.acceleration, values.maxSpeed);
		}
		else{
			DecreaseSpeed(values.horizontalFriction);
		}
		UpdateAnimator();
	}

	/// <summary>
	/// Met a jour la valeur de la direction du mouvement
	/// </summary>
	public void SetDirection() {
		values.direction = Input.GetAxis ("Horizontal");	
		values.context.SetFloat ("direction", values.direction);
	}

	/// <summary>
	/// Verifie si le personnage est au sol
	/// </summary>
	/// <returns><c>true</c>, si le personnage est au sol, <c>false</c> sinon.</returns>
	public bool IsOnGround() {
		RaycastHit2D hit = Physics2D.Linecast (
			transform.position 	
			,values.groundTrigger.transform.position
			, 1 << LayerMask.NameToLayer("ground") 
		);
		if(hit.collider != null) return true;
		else return false;
	}

	/// <summary>
	/// Met a jour la valeur du sol
	/// </summary>
	protected void UpdateGround(){
		values.onGround = IsOnGround();
	}

	protected void SetVertical(){
		values.context.SetFloat ("vertical", Input.GetAxis ("Vertical"));
	}
}
