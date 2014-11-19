using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {

	public Transform Trigger;

	// Use this for initialization
	void Start () {
		// on construit les deux trigger du bord
		// il en faut deux pour verifier que le personnage soit bien centre sur l'echelle
		buildTrigger(-0.45f);
		buildTrigger(0.45f);

		// on construit les deux trigger du haut, il en faut deux pour la meme raison
		buildTopTrigger(new Vector2(-0.05f, 0.05f));
		buildTopTrigger(new Vector2(-0.95f, 0.05f));
	}
	
	/// <summary>
	/// Construit un des declencheurs de l'echelle
	/// </summary>
	/// <param name="offset">Decalage par rapport au centre.</param>
	private void buildTrigger(float offset){
		Vector2 boxSize = new Vector2(0.1f, transform.localScale.y);

		Transform trigger = (Transform) Instantiate(Trigger);
		trigger.position = transform.position;
		BoxCollider2D box = trigger.GetComponent<BoxCollider2D>();
		box.center = new Vector2(offset, 0f);
		box.size = boxSize;
		box.tag = "ladderTrigger";
		box.transform.parent = transform;
	}

	private void buildTopTrigger(Vector2 offset){
		Vector2 boxSize = new Vector2(0.1f, 0.1f);
		
		Transform trigger = (Transform) Instantiate(Trigger);
		trigger.position = transform.position;
		BoxCollider2D box = trigger.GetComponent<BoxCollider2D>();
		box.center = ((Vector2)transform.localScale)/2 + offset;
		box.size = boxSize;
		box.tag = "ladderTop";
		box.transform.parent = transform;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
