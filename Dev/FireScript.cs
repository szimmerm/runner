using UnityEngine;
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
	public float cooldown = 0.5f;

	/// <summary>
	/// Refroidissement courant de l'arme
	/// </summary>
	private bool canFire = true;

	/// <summary>
	/// The life time of a bullet.
	/// </summary>
	public float lifeTime = 5f;

	public Vector2 shellEjectionBase;
	public Vector2 shellEjectionNoise;

	public float dispersionHeight = 0.3f;
	public float dispersionAngle = 5f;

	private SeRetourner parentDirection;
	private ObjectValues parentValues;

	public AudioClip shootSound;

	public Transform shell;

	// Use this for initialization
	void Start () {
		parentDirection = (SeRetourner) GetComponentInParent<SeRetourner>();
		parentValues = (ObjectValues) GetComponentInParent<ObjectValues>();
		if(parentDirection == null) {
			Debug.LogError ("ERROR I AM ERROR");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	/// <summary>
	/// Effectue les tests de validite de creation du tir puis le cree
	/// </summary>
	public void Fire() {
		GetComponentInParent<PlayerValues>().isFiring = true;
		if (canFire) {
			StartCoroutine (SpawnShoot());
		}
	}

	private Transform BuildBullet() {
		Transform bul = (Transform) Instantiate(bullet, Utils.PutNoiseOnVector(transform.position, new Vector3(0, dispersionHeight, 0)), Quaternion.identity);

		//		bul.transform.Rotate (getDispersionAngle());
		AITools bulletController = (AITools) bul.GetComponent<AITools>();
		
		bulletController.aiScript = null;

		if (parentValues.transform.localScale.x < 0) {
			SeRetourner.ReverseDirectionOfObject (bul.gameObject);
		}
		bulletController.MoveForward ();
//		bulletController.values.direction = (parentValues.direction.x > 0) ? -transform.right : transform.right;
		AudioSource.PlayClipAtPoint(shootSound, transform.position);
		return bul;
	}

	private void BuildShell() {
		if (shell == null) {
			return;
		}
		Transform shellInstance = (Transform) Instantiate(shell, transform.position, transform.rotation);
		Vector3 shellSize = shellInstance.GetComponent<Collider2D>().bounds.extents;
		shellInstance.GetComponent<Rigidbody2D>().AddForceAtPosition (
			Utils.PutNoiseOnVector(transform.rotation * shellEjectionBase, transform.rotation * shellEjectionNoise), 
			Utils.PutNoiseOnVector(transform.position, shellSize/2));
	}

	/// <summary>
	/// Instancie le tir
	/// </summary>
	private IEnumerator SpawnShoot() {
		GetComponentInParent<ObjectValues>().controller.AddRumble(new Vector2(1f, 1f));
		canFire = false;
		// duree de vie du tir
		Transform shoot = BuildBullet();
		Destroy(shoot.gameObject, lifeTime);
		BuildShell();
		yield return (new WaitForSeconds(cooldown));
		canFire = true;
	}
}
