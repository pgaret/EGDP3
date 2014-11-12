using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
	
	public float speed;
	public Vector2 clickedPoint;
	public GameObject origin;
	
	private float slope;
	private float upSlope;
	private float downSlope;
	private string mode;
	private Vector2 target;
	
	
	// Use this for initialization
	void Start ()
	{
		if (GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStats>().role == "Attacker") origin = GameObject.FindGameObjectWithTag("Player1");
		else origin = GameObject.FindGameObjectWithTag("Player2");
	}

	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (Vector3.up*Time.deltaTime*10);
		if (!transform.renderer.isVisible) Destroy (gameObject);
		
		if (GameObject.FindGameObjectWithTag("Boss1") != null)
		{
			GameObject.FindGameObjectWithTag("Boss1").GetComponent<Boss1>().subhealth();
			Destroy (gameObject);
		}
		
	}
}
