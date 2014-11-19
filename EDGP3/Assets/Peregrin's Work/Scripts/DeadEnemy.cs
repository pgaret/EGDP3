using UnityEngine;
using System.Collections;

public class DeadEnemy : MonoBehaviour {

	public float cd;
	float timer;

	// Use this for initialization
	void Start () 
	{
		timer = Time.time + cd;
		GetComponent<Animator>().speed = .75f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timer) Destroy (gameObject);
	
	}
}
