using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	public float time;
	float timer;

	// Use this for initialization
	void Start ()
	{
		timer = time + Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > timer) Application.LoadLevel("Peregrin's Scene");
	}
}
