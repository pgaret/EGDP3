using UnityEngine;
using System.Collections;

public class TinkerShield : MonoBehaviour {

	public float cd;
	float timer;

	GameObject[] bullets;
	GameObject parent;

	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player1");
		if (player.GetComponent<PlayerStats>().role == "Defender") parent = player;
		else parent = GameObject.FindGameObjectWithTag("Player2");
		
		timer = cd + Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (parent.GetComponent<PlayerStats>().affinity == 'A') GetComponent<Animator>().SetBool("typea", true);
		else GetComponent<Animator>().SetBool("typea", false);
		bullets = GameObject.FindGameObjectsWithTag("BulletA");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				Destroy (bullet);
				parent.GetComponent<PlayerStats>().ammo += 1;
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				Destroy (bullet);
				parent.GetComponent<PlayerStats>().ammo += 1;
			}
		}
		
		if (Time.time > timer) Destroy (gameObject);
	}
}
