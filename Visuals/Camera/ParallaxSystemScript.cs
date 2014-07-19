using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxSystemScript : MonoBehaviour {

	List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

	// Use this for initialization
	void Start () {
		createLayerList();
	}

	private void createLayerList(){
		parallaxLayers.Clear ();
		ParallaxLayer[] layers = GetComponentsInChildren<ParallaxLayer>();

		foreach(ParallaxLayer layer in layers){
			parallaxLayers.Add(layer);
			// on fixe le layer des objets fils comme etant le meme que l'objet courant,
			// pour pouvoir mettre des objets utilises ailleurs
			layer.setPhysicsLayer(gameObject.layer);
		}
	}

	// delta = valeur du deplacement
	public void Move(float delta){
		foreach(ParallaxLayer layer in parallaxLayers){
			layer.Move(delta);
		}
	}
}
