using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class BlinkOnCommand : MonoBehaviour {

	public Material blinkMaterial;

	private Material originalMaterial;
	private SpriteRenderer spriteRenderer;

	private bool isOriginalMaterial = true;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalMaterial = spriteRenderer.material;
	}
	
	public void setOriginalMaterial(){
		spriteRenderer.material = originalMaterial;
	}

	public void setBlinkMaterial(){
		spriteRenderer.material = blinkMaterial;
	}

	public void swapMaterial(){
		spriteRenderer.material = isOriginalMaterial ? blinkMaterial : originalMaterial;
		isOriginalMaterial = !isOriginalMaterial;
	}

}
