using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public bool dead = false;
	public bool specialDragon = false;

	Transform parent;
	float lifeSpan = 15.0f;
	float startTime;

	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
	}
	
	void BulletCheck()
	{
		GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
		GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
		
		for (int i = 0; i < bulletA.Length; i++)
		{
			if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				Destroy (bulletA[i].gameObject);
			}
			else if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				parent.GetComponent<PlayerStats>().ammo += 1;
				Destroy (bulletA[i].gameObject);
			}
		}
		for (int i = 0; i < bulletB.Length; i++)
		{
			if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				parent.GetComponent<PlayerStats>().ammo += 1;
				Destroy(bulletB[i].gameObject);
			}
			else if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				Destroy(bulletB[i].gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
//		Debug.Log (Time.time+"  "+startTime+" "+lifeSpan);
		if (Time.time - lifeSpan >= startTime && specialDragon == false) dead = true;
	}
}
