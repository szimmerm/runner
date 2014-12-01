using UnityEngine;
using System.Collections;

public abstract class AbstractActivationScript : MonoBehaviour {

	// fonction qui va reveiller le script

	public abstract void Activate();

	public abstract void Desactivate();
}
