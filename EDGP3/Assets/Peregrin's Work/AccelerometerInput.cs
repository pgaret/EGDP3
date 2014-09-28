using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

	void OnGUI()
	{
		GUI.Box (new Rect (Screen.width / 3, Screen.height / 4, Screen.width / 3, Screen.height / 8), Input.acceleration.ToString());
	}

	void Update() {
		Debug.Log (Input.acceleration);
	}
}
	