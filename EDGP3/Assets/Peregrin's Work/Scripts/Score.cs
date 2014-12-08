using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public float score;

	// Use this for initialization
	void Start ()
	{
		score = 0;
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Application.loadedLevelName == "Alex's Scene")
		{
			Application.LoadLevel("Peregrin's Scene");
		}
		if (Application.loadedLevelName == "Peregrin's Scene")
		{
			if (GameObject.Find ("Main Camera").GetComponent<GUIManager>().mode == 2)
			{
				float potentialScore = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStats>().score + GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerStats>().score;
				if (potentialScore > score) score = potentialScore;
			}
		}
	}
}
