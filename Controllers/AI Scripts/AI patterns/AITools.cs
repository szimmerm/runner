using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SeRetourner))]
public class AITools : GenericMove {
	public bool moving = true;
	public float jumpForce;
//	public float direction;
	public Vector2 acceleration;
	public Vector2 maxSpeed;

	private FireScript weapon;
	public Vector2 currentGoal;
	public AIPath aiScript;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		enabled = true;
		weapon = GetComponentInChildren<FireScript>();
		aiScript = new AIPath();
		currentGoal = aiScript.nextStep ();
		SetMoveTowardPoint (currentGoal);
	}
	
	// Update is called once per frame
	void Update () {
		if(aiScript != null && GetComponent<Collider2D>().OverlapPoint (currentGoal)) {
			currentGoal = aiScript.nextStep();
			SetMoveTowardPoint (currentGoal);
		}
	}
	
	void FixedUpdate(){
//		if (moving) {
			AccelerateWithCap(acceleration.x, acceleration.y, maxSpeed.x, maxSpeed.y);
//		}
	}

	void LateUpdate(){
		if (values.animator != null) {
			values.animator.SetFloat ("xVelocity", values.body.velocity.x);
		}
	}

	/// <summary>
	/// Fires the equiped weapon
	/// </summary>
	public void Fire() {
		if (weapon != null) {
			weapon.Fire ();
		}
	}

	/// <summary>
	/// Tells the character to jump
	/// </summary>
	public void Jump() {
		JumpImpulse(jumpForce);
	}

	public void MoveForward() {
		values.direction = new Vector2((transform.localScale.x > 0) ? 1 : -1, values.direction.y);
	}

	/// <summary>
	/// Makes the enemy move in the direction of a point
	/// </summary>
	/// <param name="point">The point where to go.</param>
	public void SetMoveTowardPoint(Vector2 point) {
		moving = true;
		if (point.x > transform.position.x) {
			values.direction = new Vector2(1, values.direction.y);
		} else {
			values.direction = new Vector2(-1, values.direction.y);
		}
	}
}
