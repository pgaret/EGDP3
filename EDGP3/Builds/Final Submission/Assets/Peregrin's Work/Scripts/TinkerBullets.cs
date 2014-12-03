using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TinkerBullets : MonoBehaviour {

	public int type;
	public float speed;
	public Sprite bombSprite;
	public float damage;
	
	private bool explosion = false;
	private bool hasHit = false;
	private int index = 0;
	private List<GameObject> enemies = new List<GameObject>();
	private GameObject boss;
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
			GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("EnemyShipA");
			for (int i = 0; i < enemies1.Length; i++) enemies.Add (enemies1[i]);
			if (GameObject.FindGameObjectWithTag("Boss1")) enemies.Add (GameObject.FindGameObjectWithTag("Boss1"));
			if (GameObject.FindGameObjectWithTag("Boss2")) enemies.Add(GameObject.FindGameObjectWithTag("Boss2"));
			if (GameObject.FindGameObjectWithTag("Boss3")) enemies.Add (boss = GameObject.FindGameObjectWithTag("Boss3"));
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
						if (enemy.renderer.bounds.Intersects(collider2D.bounds) && enemy.tag == "EnemyShipA")
						{
							enemy.GetComponent<Enemy>().health -= damage;
						}
						else if (enemy.renderer.bounds.Intersects(collider2D.bounds) && enemy.tag == "Boss1")
						{
							enemy.GetComponent<Boss1>().subhealth(damage);
						}
						else if (enemy.renderer.bounds.Intersects(collider2D.bounds) && enemy.tag == "Boss2")
						{
							enemy.GetComponent<Boss2>().subhealth(damage);
						}
						else if (enemy.renderer.bounds.Intersects(collider2D.bounds) && enemy.tag == "Boss3")
						{
							enemy.GetComponent<Boss3>().subhealth(damage);
						}
					}
					type = 4;
				}
			}
			
			if (type == 1) //Homing
			{
				if (enemies.Count == 0) transform.Translate(Vector3.up*Time.deltaTime*speed);
				else
				{
					float dist = 0;
					if (enemies.Count == 0) 
					if (hasHit) Destroy(gameObject);
					for (int i = 0; i < enemies.Count; i++)
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
						if (enemies[index].tag == "EnemyShipA")
						{
							enemies[index].GetComponent<Enemy>().health -= damage;
						}
						else if (enemies[index].tag == "Boss1")
						{
							enemies[index].GetComponent<Boss1>().subhealth(damage);
						}
						else if (enemies[index].tag == "Boss2")
						{
							enemies[index].GetComponent<Boss2>().subhealth(damage);
						}
						else if (enemies[index].tag == "Boss3")
						{
							enemies[index].GetComponent<Boss3>().subhealth(damage);
						}
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
						if (enemies[index].tag == "EnemyShipA")
						{
							enemies[index].GetComponent<Enemy>().health -= damage;
						}
						else if (enemies[index].tag == "Boss1")
						{
							enemies[index].GetComponent<Boss1>().subhealth(damage);
						}
						else if (enemies[index].tag == "Boss2")
						{
							enemies[index].GetComponent<Boss2>().subhealth(damage);
						}
						else if (enemies[index].tag == "Boss3")
						{
							enemies[index].GetComponent<Boss3>().subhealth(damage);
						}
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
