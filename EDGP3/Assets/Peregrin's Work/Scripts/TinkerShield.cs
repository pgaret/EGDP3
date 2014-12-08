using UnityEngine;
using System.Collections;

public class TinkerShield : MonoBehaviour {

	public float cd;
	float timer;

	GameObject[] bullets;
	GameObject parent;
	
	GameObject sound;

	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player1");
		if (player.GetComponent<PlayerStats>().role == "Defender") parent = player;
		else parent = GameObject.FindGameObjectWithTag("Player2");
		
		timer = cd + Time.time;
		
		sound = GameObject.Find("Sound");
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
				parent.GetComponent<PlayerStats>().ammo += 2;
				sound.GetComponent<SoundManager>().PlaySound("absorb");
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				Destroy (bullet);
				parent.GetComponent<PlayerStats>().ammo += 2;
				sound.GetComponent<SoundManager>().PlaySound("absorb");
			}
		}
		
		if (Time.time > timer)
		{
			Debug.Log("destroy tinker shield");
			Destroy (gameObject);
		}
	}
}
