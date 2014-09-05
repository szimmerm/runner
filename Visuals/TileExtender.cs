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
	private delegate void DrawFunc(Vector3 pos);

	// Use this for initialization
	void Start () {
		SpriteRenderer parentRenderer = gameObject.GetComponent<SpriteRenderer>();
		parentRenderer.enabled = false;
		FillWithChildren(parentRenderer.bounds.min, parentRenderer.bounds, InstantiateTileAt);
	}

	/// <summary>
	/// Rempli l'objet avec les tiles
	/// </summary>
	/// <param name="destination">Position de depart.</param>
	/// <param name="box">Taille du rectangle a remplir.</param>
	/// <param name="drawer">Fonction d'affichage.</param>
	private void FillWithChildren(Vector3 destination, Bounds box, DrawFunc drawer){
		Bounds bounds = tile.gameObject.GetComponent<SpriteRenderer>().bounds;

		// calcul du nombre d'iterations pour remplir le bloc
		// on fait la conversion en utilisant un arrondi ; a l'utilisateur de n'utiliser que des valeurs entieres
		float xFloatIteration = box.extents.x / bounds.extents.x;
		float yFloatIteration = box.extents.y / bounds.extents.y;
		int xMaxIteration = Mathf.RoundToInt (xFloatIteration);
		int yMaxIteration = Mathf.RoundToInt (yFloatIteration);

		
		// on recupere la moitie de la taille du rectangle
		Vector3 halfsize = bounds.extents;

		Vector3 horizontalOffset = new Vector3(halfsize.x*2, 0, 0);
		Vector3 verticalOffset = new Vector3(0, halfsize.y*2, 0);
		for(int i=0 ; i<xMaxIteration ; i++){
			for(int j=0 ; j<yMaxIteration ; j++){
//				Transform newTile = Instantiate(tile) as Transform;
				Vector3 position = destination + halfsize + (i*horizontalOffset) + (j*verticalOffset);
				drawer(position);
				// nb : on soustrait halfsize pour faire en sorte que les coins inferieurs gauche soient coordonnes
				//newTile.transform.parent = this.transform;
			}
		}
	}
	
	// affichage des tiles dans l'editeur
	private void OnDrawGizmos(){
		if(!drawn){
			rendereur = gameObject.GetComponent<SpriteRenderer>();
			drawn = true;
		}
		FillWithChildren(rendereur.bounds.min, rendereur.bounds, DrawTextureAt);

	}

	/// <summary>
	/// Fonction d'affichage instanciant les tiles
	/// </summary>
	/// <param name="pos">Position ou creer la tile.</param>
	private void InstantiateTileAt(Vector3 pos) {
		Transform newTile = Instantiate(tile) as Transform;
		newTile.position = pos;
		newTile.transform.parent = this.transform;
	}

	/// <summary>
	/// Fonction d'affichage pour l'editeur
	/// </summary>
	/// <param name="pos">Position de la tile.</param>
	private void DrawTextureAt(Vector3 pos){
		Sprite sprite = tile.gameObject.GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		Rect rect = new Rect(pos.x, pos.y, 1, 1);
		Gizmos.DrawGUITexture (rect, texture);
	}
}
