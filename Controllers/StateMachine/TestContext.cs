using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Contexte contenant toutes les valeurs des variables internes d'un automate
/// </summary>
[System.Serializable]
public class TestContext{

	/// <summary>
	/// Valeurs des variables de type flottant
	/// </summary>
	public Dictionary<string, float> floatContext;

	/// <summary>
	/// Valeurs des variables de type booleen
	/// </summary>
	public Dictionary<string, bool> boolContext;

	/// <summary>
	/// Valeurs des declencheurs
	/// </summary>
	public Dictionary<string, bool> triggerContext;
	
	public TestContext(){
		floatContext = new Dictionary<string, float>();
		boolContext = new Dictionary<string, bool>();
		triggerContext = new Dictionary<string, bool>();
	}

	/// <summary>
	/// Donne la valeur <paramref name="val"/> a la variable de type 
	/// booleen <paramref name="name"/> .
	/// </summary>
	/// <param name="name">Nom de la variable.</param>
	/// <param name="val">Valeur de la variable.</param>
	public void SetBool(string name, bool val){
		boolContext[name] = val;
	}

	/// <summary>
	/// Retourne la valeur de la variable <paramref name="name"/>. Par convention
	/// une variable qui n'existe pas vaut toujours <c>false</c>
	/// </summary>
	/// <returns>La valeur de la variable.</returns>
	/// <param name="name">Le nom de la variable.</param>
	public bool GetBool(string name){
		try{
			return boolContext[name];
		} catch(KeyNotFoundException) {
//			Debug.LogError ("bool variable not found : "+name);
			return false;
		}
	}

	/// <summary>
	/// Donne la valeur <paramref name="val"/> a la variable de type 
	/// flottant <paramref name="name"/> .
	/// </summary>
	/// <param name="name">Nom de la variable.</param>
	/// <param name="val">Valeur de la variable.</param>
	public void SetFloat(string name, float val){
		floatContext[name] = val;
	}

	/// <summary>
	/// Retourne la valeur de la variable <paramref name="name"/>. Par convention
	/// une variable qui n'existe pas vaut toujours <c>0f</c>
	/// </summary>
	/// <returns>La valeur de la variable.</returns>
	/// <param name="name">Le nom de la variable.</param>
	public float GetFloat(string name){
		try{
			return floatContext[name];
		} catch(KeyNotFoundException){
//			Debug.LogError ("float variable not found : "+name);
			return 0f;
		}
	}

	/// <summary>
	/// Active le declencheur <paramref name="val"/>.
	/// </summary>
	/// <param name="name">Nom du declencheur.</param>
	public void SetTrigger(string name){
		triggerContext[name] = true;
	}

	/// <summary>
	/// Indique si le declencheur <paramref name="name"/> a ete active.
	/// </summary>
	/// <returns>La valeur de la variable.</returns>
	/// <param name="name">Le nom du declencheur.</param>
	public bool GetTrigger(string name){
		try{
			return triggerContext[name];
		} catch(KeyNotFoundException) {
			return false;
		}
	}

	public void ResetTriggers(){
		triggerContext.Clear ();
	}

}
