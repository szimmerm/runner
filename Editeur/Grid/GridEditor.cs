#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Grid))]
public class GridEditor : Editor {

	private Grid grid;

	public void OnEnable(){
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate += GridUpdate;
	}

	public override void OnInspectorGUI(){
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Grid width");
		grid.width = EditorGUILayout.FloatField (grid.width, GUILayout.Width (50));
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ( "Grid height ");
		grid.height = EditorGUILayout.FloatField (grid.height, GUILayout.Width (50));
		GUILayout.EndHorizontal ();

		SceneView.RepaintAll ();
	}

	private void GridUpdate(SceneView sceneview){
		Event e = Event.current;
//		Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
//		Vector3 mousePos = r.origin;

		if(e.isKey && e.character == 'a'){
			GameObject obj;
//			Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);


			if(Selection.activeGameObject){
//			if (prefab){
//				obj = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
				obj = Selection.activeGameObject;
				Vector3 pos = obj.transform.position;
				Vector3 offset = (obj.GetComponent<SpriteRenderer>().sprite.bounds.extents);
				offset.x = offset.x * obj.transform.localScale.x;
				offset.y = offset.y * obj.transform.localScale.y;
				obj.transform.position = normalize_to_grid(pos - offset) + offset;
			}
		}
	}

	public static float normalize_value(float value, float unit){
		return Mathf.Floor(value / unit) * unit;
	}

	public Vector3 normalize_to_grid(Vector3 vect){
		return new Vector3(
				normalize_value(vect.x, grid.width),
				normalize_value(vect.y, grid.height),
			vect.z);
	}



	public void OnDisable(){
		SceneView.onSceneGUIDelegate -= GridUpdate;
	}
}
#endif