#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Script used for normalizing item with respect to the tiles
/// </summary>
public class NormalizeShortcut : MonoBehaviour {

	[MenuItem ("Zim Tools/Normalize Transform %g")]
	public static void NormalizeCall(){
		GameObject obj = Selection.activeGameObject;
		if (obj != null)
			NormalizeObject(obj.transform);
	}

/*
	[MenuItem ("Zim Tools/MyAnus %g")]
	public static void PrintAnus(){
		Debug.Log ("Welcome to my anus");
	}
*/

	/// <summary>
	/// Aligne un objet par rapport a la grille
	/// </summary>
	/// <param name="obj">Objet a normaliser</param>
	private static void NormalizeObject(Transform obj){
		// l'arrondi est calcule a partir du coin inferieur gauche, on utilise un decalage
		Vector3 position = obj.transform.position;
		Vector3 scale = obj.transform.localScale;
		Vector3 corner = position - (scale / 2.0F);

		int xScale = Mathf.FloorToInt (scale.x);
		int yScale = Mathf.FloorToInt (scale.y);
		float xPos = Mathf.Round (corner.x) + xScale / 2.0F;
		float yPos = Mathf.Round (corner.y) + yScale / 2.0F;
/*
		float xPos = Mathf.RoundToInt (obj.transform.position.x) + ((xScale % 2 == 0) ? 0F : 0.5F);
		float yPos = Mathf.RoundToInt (obj.transform.position.y) + ((yScale % 2 == 0) ? 0F : 0.5F);
*/
		obj.transform.position = new Vector3(xPos, yPos, obj.transform.position.z);
		obj.transform.localScale = new Vector3(xScale, yScale, obj.transform.localScale.z);
	}
}
#endif
