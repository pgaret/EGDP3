using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonTamer : MonoBehaviour {
	
	public float speed;
	public float dragonSpeed;
	public float dragDist;
	public float dragSpawn;
	public int dragCost;
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	public Sprite Attack;

	public List<Transform> dragons = new List<Transform>();
	
	float specialTimer;
	float specialCD = 1;
	bool specialActivate = false;
	
	// Use this for initialization
	void Start () {
		Dragon();
		
	}
	
	void Dragon()
	{
		transform.GetComponent<PlayerStats>().ammo -= dragCost;
		Vector3 position = transform.position;
		position.x -= (special.transform.renderer.bounds.extents.x*dragons.Count*3 + 1)* dragSpawn;
		dragons.Add((Transform)(Instantiate (special, position, Quaternion.identity)));
		if (dragons.Count == 1) dragons[0].GetComponent<Dragon>().specialDragon = true;
	}
	
	// Update is called once per frame
	void Update () {
		shipType = transform.GetComponent<PlayerStats>().role;
		
		for (int i = 0; i < dragons.Count; i++)
		{
			if((Vector3.Distance (dragons[i].position, transform.position) > (i+1)*dragDist)) dragons[i].position = Vector3.MoveTowards(dragons[i].position, transform.position, Time.deltaTime*dragonSpeed);
			if (dragons[i].GetComponent<Dragon>().dead == true)
			{
				Destroy (dragons[i].gameObject);
				dragons.RemoveAt(i);
			}
		}
		
		if (shipType == "Attacker")
		{
			if (GetComponent<PlayerStats>().specialBool == true && dragons.Count < 4 && transform.GetComponent<PlayerStats>().ammo >= dragCost)
			{
				Dragon ();
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

	}
}
