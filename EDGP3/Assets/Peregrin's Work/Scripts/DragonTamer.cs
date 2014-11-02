using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonTamer : MonoBehaviour {
	
	public float speed;
	public float dragonSpeed;
	public float dragDist;
	public float dragSpawn;
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	public Sprite Attack;
	public Vector3 prevLoc1;
	public Vector3 prevLoc2;
	public Vector3 prevLoc3;
	public Vector3 prevLoc4;

	public List<Transform> dragons = new List<Transform>();
	
	float specialTimer;
	float specialCD = 1;
	bool specialActivate = false;
	
	// Use this for initialization
	void Start () {
		//Instantiates the dragon and shield
		if (transform.tag == "Player2")
		{
			Dragon();
			Shield ();
		}
		if (transform.tag == "Player1")
		{
			Dragon ();
		}
		
	}
	
	void Dragon()
	{
		Vector3 position = transform.position;
		position.x -= (special.transform.renderer.bounds.extents.x*dragons.Count*3 + 1)* dragSpawn;
		prevLoc1 = position;
		dragons.Add((Transform)(Instantiate (special, prevLoc1, Quaternion.identity)));
		if (dragons.Count == 1) dragons[0].GetComponent<Dragon>().specialDragon = true;
	}
	
	void Special()
	{
//		transform.GetComponent<PlayerStats>().ammo -= 10;
		specialActivate = true;
		specialTimer = specialCD + Time.time;
	}
	
	public void Shield()
	{
		foreach (Transform dragon in dragons) Instantiate (shield);
	}
	
	// Update is called once per frame
	void Update () {
		shipType = transform.GetComponent<PlayerStats>().role;
		
		for (int i = 0; i < dragons.Count; i++)
		{
			if((Vector3.Distance (dragons[i].position, transform.position) > i + dragDist)) dragons[i].position = Vector3.MoveTowards(dragons[i].position, transform.position, Time.deltaTime*dragonSpeed);
			if (dragons[i].GetComponent<Dragon>().dead == true)
			{
				Destroy (dragons[i].gameObject);
				dragons.RemoveAt(i);
			}
		}
		
		if (shipType == "Attacker")
		{
			if (GetComponent<PlayerStats>().specialBool == true)
			{
				Special();
				GetComponent<PlayerStats>().specialBool = false;
			}

			if (GetComponent<PlayerStats>().shootBool == true)
			{
				foreach (Transform dragon in dragons) 
				{
					Transform bullet = (Transform)Instantiate(projectile, dragon.position, Quaternion.identity);
					bullet.GetComponent<SpriteRenderer>().sprite = Attack;
				}
				transform.GetComponent<PlayerStats>().ammo -= 1;
				GetComponent<PlayerStats>().shootBool = false;
			}

			GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
			GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
			for (int i = 0; i < bulletA.Length; i++)
			{
				if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds))
				{
					Destroy (bulletA[i].gameObject);
				}
			}
			for (int i = 0; i < bulletB.Length; i++)
			{
				if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds))
				{
					Destroy(bulletB[i].gameObject);
				}
			}
		}
		
		if (specialActivate == true)
		{
			Dragon();
			specialActivate = false;
		}

	}
}
