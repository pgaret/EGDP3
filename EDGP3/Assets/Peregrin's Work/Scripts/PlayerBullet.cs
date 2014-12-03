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
			if (GameObject.FindGameObjectWithTag("Boss1").renderer.bounds.Intersects(renderer.bounds))
			{
				GameObject.FindGameObjectWithTag("Boss1").GetComponent<Boss1>().subhealth(1);
				Destroy (gameObject);
			}
		}
		else if (GameObject.FindGameObjectWithTag("Boss2") != null)
		{	
			if (GameObject.FindGameObjectWithTag("Boss2").renderer.bounds.Intersects(renderer.bounds))
			{
				GameObject.FindGameObjectWithTag("Boss2").GetComponent<Boss2>().subhealth(1);
				Destroy (gameObject);
			}
		}
		else if (GameObject.FindGameObjectWithTag("Boss3") != null)
		{
			Debug.Log (GameObject.FindGameObjectWithTag("Boss3").collider.bounds+ "  " + renderer.bounds);
			if (GameObject.FindGameObjectWithTag("Boss3").collider.bounds.Intersects(renderer.bounds))
			{
				GameObject.FindGameObjectWithTag("Boss3").GetComponent<Boss3>().subhealth(1);
				Destroy (gameObject);
			}
		}
		
	}
}
