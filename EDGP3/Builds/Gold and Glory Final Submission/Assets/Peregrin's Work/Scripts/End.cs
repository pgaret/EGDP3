using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	public GUIStyle style;
	public float time;
	float timer;
	
	float score;

	// Use this for initialization
	void Start ()
	{
		timer = time + Time.time;
		score = Mathf.RoundToInt(GameObject.Find ("Score").GetComponent<Score>().score);
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 3), "Score: \n\n"+score.ToString(), style);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > timer) Application.LoadLevel("Peregrin's Scene");
	}
}
