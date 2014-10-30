using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	
	public float speed;
	public Vector2 clickedPoint;
	
	private float slope;
	private float upSlope;
	private float downSlope;
	private string mode;
	private Vector2 target;
	
	
	// Use this for initialization
	void Start () {
	
	}

	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (Vector3.up*Time.deltaTime*10);
		if (!transform.renderer.isVisible) Destroy (gameObject);
		
	}
}
