using UnityEngine;
using System.Collections;

public class Utils {

	
	public static Vector3 PutNoiseOnVector(Vector3 origin, Vector3 noiseRange) {
		Vector3 noise = new Vector3(Random.value * noiseRange.x * 2, Random.value * noiseRange.y * 2,  Random.value * noiseRange.z * 2);
		Vector3 offset = new Vector3(-noiseRange.x, -noiseRange.y, -noiseRange.z);
		return origin + noise + offset;
	}
}
