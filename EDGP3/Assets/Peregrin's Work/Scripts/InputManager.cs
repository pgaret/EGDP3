using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonUp("XboxFire1A")) Debug.Log ("XboxFire1A");
		if (Input.GetButtonUp("XboxFire1B")) Debug.Log ("XboxFire1B");
		if (Input.GetButtonUp("XboxFire1X")) Debug.Log ("XboxFire1X");
		if (Input.GetButtonUp("XboxFire1Y")) Debug.Log ("XboxFire1Y");
		if (Input.GetButtonUp("XboxFire2A")) Debug.Log ("XboxFire2A");
		if (Input.GetButtonUp("XboxFire2B")) Debug.Log ("XboxFire2B");
		if (Input.GetButtonUp("XboxFire2X")) Debug.Log ("XboxFire2X");
		if (Input.GetButtonUp("XboxFire2Y")) Debug.Log ("XboxFire2Y");
//		if (Input.GetAxis("XboxHorizontal1") != 0) Debug.Log (Input.GetAxis("XboxHorizontal1"));
//		if (Input.GetAxis("XboxVertical1") != 0) Debug.Log (Input.GetAxis("XboxVertical1"));
//		if (Input.GetAxis("XboxHorizontal2") != 0) Debug.Log (Input.GetAxis("XboxHorizontal2"));
//		if (Input.GetAxis("XboxVertical2") != 0) Debug.Log (Input.GetAxis("XboxVertical2"));
	}
}
