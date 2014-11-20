using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Valeurs internes des objets. On utilise une classe separee pour qu'elles soient communes entre les differents modules
/// </summary>
public class ObjectValues : MonoBehaviour {
	public TestContext context; // valeurs internes de l'automate de controle
	public float direction; // direction dans laquelle regarde l'objet

	void Awake(){
		context = new TestContext ();
	}
}
