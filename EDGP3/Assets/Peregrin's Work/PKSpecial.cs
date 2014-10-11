using UnityEngine;
using System.Collections;

public class PKSpecial : MonoBehaviour {

	public float speed = 10.0f;

	// Use this for initialization
	void Start () {
		float random = Random.Range (-25, 25);
		transform.Rotate (Vector3.forward*random);
	}
	
	// Update is called once per frame
	void Update () {
		
		rigidbody.AddForce(transform.up * speed);
		rigidbody.velocity = Vector3.zero;
	}
}
