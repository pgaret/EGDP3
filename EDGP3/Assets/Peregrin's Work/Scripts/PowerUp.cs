using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public float CD;
	float timer;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("test");
		timer = Time.time + CD;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > timer) Destroy(gameObject);
	}
}
