using UnityEngine;
using System.Collections;

public class PKSpecial : MonoBehaviour {

	public int rotation = -1000;
	
	public float speed = 10.0f;

	// Use this for initialization
	public void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (rotation != -1000)
		{
			transform.Rotate (Vector3.forward*rotation);
			rotation = -1000;
		}
		rigidbody.AddForce(transform.up * speed);
		rigidbody.velocity = Vector3.zero;
	}
}
