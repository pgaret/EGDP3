using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TinkerBullets : MonoBehaviour {

	public int type;
	public float speed;
	public Sprite bombSprite;
	
	private bool explosion = false;
	private bool hasHit = false;
	private int index = 0;
	private GameObject[] enemies;
	private float bombCD;
	private float bombTimer = -1;

	// Use this for initialization
	void Start ()
	{
		bombCD = .3f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!renderer.isVisible) Destroy (gameObject);
		if (type == 0 || type == 1 || type == 2)
		{
			enemies = GameObject.FindGameObjectsWithTag("EnemyShipA");
			if (type != 1) transform.Translate(Vector3.up*Time.deltaTime*speed);
			
			if (type == 0) //Bomb
			{
				foreach (GameObject enemy in enemies)
				{
					if (enemy.renderer.bounds.Intersects(renderer.bounds))
					{
						explosion = true;
					}
				}
				if (explosion == true)
				{
					foreach (GameObject enemy in enemies)
					{
						if (enemy.renderer.bounds.Intersects(collider2D.bounds))
						{
							enemy.GetComponent<Enemy>().health -= 1;
						}
					}
					type = 4;
				}
			}
			
			if (type == 1) //Homing
			{
				if (enemies.Length == 0) transform.Translate(Vector3.up*Time.deltaTime*speed);
				else
				{
					float dist = 0;
					if (enemies.Length == 0) 
					if (hasHit) Destroy(gameObject);
					for (int i = 0; i < enemies.Length; i++)
					{
						float test = Vector3.Distance(enemies[i].transform.position, transform.position);
						if (test < dist)
						{
							dist = test;
							index = i;
						}
					}
					transform.position = Vector3.MoveTowards(transform.position, enemies[index].transform.position, Time.deltaTime*speed);
					Quaternion rotation = Quaternion.LookRotation(enemies[index].transform.position + transform.position);
					transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
					if (transform.position == enemies[index].transform.position)
					{
						Destroy(gameObject);
						Debug.Log ("test");
						enemies[index].GetComponent<Enemy>().health -= 1;
						hasHit = true;
					}
				}
			}
			
			if (type == 2) //Piercing
			{
				List<GameObject> haveHit = new List<GameObject>();
				foreach (GameObject enemy in enemies)
				{
					if (enemy.renderer.bounds.Intersects(renderer.bounds) && !haveHit.Contains(enemy))
					{
						enemy.GetComponent<Enemy>().health -= 1	;
						haveHit.Add (enemy);
					}
				}
			}
		}
		
		if (type == 4 && bombTimer == -1) //Bomb explosion
		{
			transform.position = enemies[index].transform.position;
			GetComponent<SpriteRenderer>().sprite = bombSprite;
			bombTimer = Time.time + bombCD;
		}
		if (type == 4 && bombTimer != -1 && bombTimer < Time.time)
		{
			Destroy(gameObject);
		}
	}
	
}
