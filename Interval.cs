using UnityEngine;
using System.Collections;

public static class Interval{

	public static bool InInterval(float value, float cap){
		if (cap < 0){
			cap = -cap;
		}
		return (value < cap && value > -cap);
	}

	public static float PutInInterval(float value, float cap){
		if (cap < 0){
			cap = -cap;
		}

		if (value > cap){
			return cap;
		} else if (value < -cap){
			return -cap;
		} else {
			return value;
		}
	}

}
