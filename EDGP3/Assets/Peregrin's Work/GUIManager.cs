using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (768, 1024, true);
	}

	void OnGUI()
	{
		GameObject player1 = GameObject.Find ("Ship1");
		GameObject player2 = GameObject.Find ("Ship2");
		GUI.Box (new Rect (Screen.width * 7 / 8, 0, Screen.width / 8, Screen.height), "Ammo1: \n" + player1.GetComponent<Ship1>().ammo.ToString() + "  \nAmmo2: \n" + player2.GetComponent<Ship1>().ammo.ToString());
		
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
	}
}
