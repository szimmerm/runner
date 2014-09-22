using UnityEngine;
using System.Collections;

public class TileExtender : MonoBehaviour {

	/// <summary>
	/// The tile.
	/// </summary>
	public Transform tile;
	private bool drawn = false;
	private SpriteRenderer rendereur;

	// type des fonctions d'affichage, pour gerer le tiling uniformement
	private delegate void DrawFunc(Vector3 pos, Vector3 offset);

	// Use this for initialization
	void Start () {
		SpriteRenderer parentRenderer = gameObject.GetComponent<SpriteRenderer>();
		parentRenderer.enabled = false;
		normalize();
//		FillWithChildren(parentRenderer.bounds.min, parentRenderer.bounds, InstantiateTileAt);
		TileFilling(InstantiateTileAt);
	}

	private void normalize(){
		Vector3 scale = transform.localScale;
		int xScale = Mathf.FloorToInt (scale.x);
		int yScale = Mathf.FloorToInt (scale.y);
		float xPos = Mathf.FloorToInt (transform.position.x) + ((xScale % 2 == 0) ? 0F : 0.5F);
		float yPos = Mathf.FloorToInt (transform.position.y) + ((yScale % 2 == 0) ? 0F : 0.5F);
		transform.position = new Vector3(xPos, yPos, transform.position.z);
		transform.localScale = new Vector3(xScale, yScale, transform.localScale.z);
	}

	/// <summary>
	/// Rempli la zone de tiles en fonction de l'afficheur utilise
	/// </summary>
	/// <param name="drawer">La fonction de creation de tiles.</param>
	private void TileFilling(DrawFunc drawer){
		Vector3 start = transform.position - (transform.localScale /2);
		Vector3 end = transform.position + (transform.localScale / 2);
		Vector3 offset = new Vector3(0.5F, 0.5F, 0F);

		Debug.Log ("start : "+start);
		Debug.Log ("end : "+end);

		for(float xPos = Mathf.Round (start.x); xPos < Mathf.Round(end.x) ; xPos ++)
			for (float yPos = Mathf.Round (start.y) ; yPos < Mathf.Round(end.y) ; yPos++) {
				drawer(new Vector3(xPos, yPos, transform.position.z), offset);
			}

	}

	// affichage des tiles dans l'editeur
	private void OnDrawGizmos(){
		if(!drawn){
			rendereur = gameObject.GetComponent<SpriteRenderer>();
//			drawn = true;
		}
//		FillWithChildren(rendereur.bounds.min, rendereur.bounds, DrawTextureAt);
		TileFilling(DrawTextureAt);
		drawn = true;
	}

	/// <summary>
	/// Fonction d'affichage instanciant les tiles
	/// </summary>
	/// <param name="pos">Position ou creer la tile.</param>
	private void InstantiateTileAt(Vector3 pos, Vector3 offset) {
		// on decale l'instantiation avec sa largeur, pour contrebalancer la difference
		// de comportement entre Instantiate et DrawGUITexture
		pos = pos + offset;
		Debug.Log ("instanciation a "+pos);
		Transform newTile = Instantiate(tile) as Transform;
		newTile.position = pos;
		newTile.transform.parent = this.transform;
	}

	/// <summary>
	/// Fonction d'affichage pour l'editeur
	/// </summary>
	/// <param name="pos">Position de la tile.</param>
	private void DrawTextureAt(Vector3 pos, Vector3 offset){
		// DrawGUITexture instancie a partir du coin alors que Instantiate a partir du centre du transform
		// on oublie donc l'offset
		Sprite sprite = tile.gameObject.GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
//		Rect rect = new Rect(pos.x, pos.y, 1, 1);
		Rect rect = new Rect(pos.x, pos.y, 1.0F, 1.0F);
		Gizmos.DrawGUITexture (rect, texture);
	}
}
