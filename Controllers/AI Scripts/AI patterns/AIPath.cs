using UnityEngine;
using System.Collections;

public class AIPath {
	// nazi test : 5 --> 10
	public enum Actions {FIRE, JUMP, WAIT}
	
	private Vector2[] steps;
	private Actions[][] actions;
	private int pos = -1;
	
	public AIPath() {
		steps = new Vector2[2];
		steps[0] = new Vector2(5, 0);
		steps[1] = new Vector2(10, 0);
	}
	
	public Vector2 nextStep() {
		pos = (pos + 1) % steps.Length; // looping
		return steps[pos];
	}
}