using UnityEngine;
using System.Collections;

/// <summary>
/// Classe generique regissant les mouvements du personnage controlle par le joueur
/// </summary>
public class PlayerController : GenericMove {

	// valeurs visibles pour l'utilisateur
	public float maxSpeed = 2f;
	public float acceleration = 20f;
	public float horizontalFriction = 20f;
	public bool onGround = false;
	public bool climbing = false;

	public Transform groundTrigger;

	/// <summary>
	/// Resolution du mouvement horizontal
	/// </summary>
	public void ApplyHorizontalMove(){
		if (direction != 0){
			AccelerateWithCap(acceleration, maxSpeed);
		}
		else{
			DecreaseSpeed(horizontalFriction);
		}
		UpdateAnimator();
	}

	/// <summary>
	/// Met a jour la valeur de la direction du mouvement
	/// </summary>
	public void SetDirection() {
		direction = Input.GetAxis ("Horizontal");	
		context.SetFloat ("direction", direction);
	}

	/// <summary>
	/// Verifie si le personnage est au sol
	/// </summary>
	/// <returns><c>true</c>, si le personnage est au sol, <c>false</c> sinon.</returns>
	public bool IsOnGround() {
		RaycastHit2D hit = Physics2D.Linecast (
			transform.position 	
			,groundTrigger.transform.position
			, 1 << LayerMask.NameToLayer("ground") 
		);
		if(hit.collider != null) return true;
		else return false;
	}

	/// <summary>
	/// Met a jour la valeur du sol
	/// </summary>
	protected void UpdateGround(){
		onGround = IsOnGround();
	}

	protected void SetVertical(){
		context.SetFloat ("vertical", Input.GetAxis ("Vertical"));
	}
	
}
