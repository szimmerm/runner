using UnityEngine;
using System.Collections;

public class TileExtender : MonoBehaviour {

	public Transform tile;

	// Use this for initialization
	void Start () {
		SpriteRenderer parentRenderer = gameObject.GetComponent<SpriteRenderer>();
		parentRenderer.enabled = false;
		FillWithChildren(parentRenderer.bounds.min, parentRenderer.bounds);
	}

	void FillWithChildren(Vector3 destination, Bounds box){
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
				Transform newTile = Instantiate(tile) as Transform;
				newTile.position = destination + halfsize + (i*horizontalOffset) + (j*verticalOffset);
				// nb : on soustrait halfsize pour faire en sorte que les coins inferieurs gauche soient coordonnes
				newTile.transform.parent = this.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
