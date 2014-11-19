using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public bool dead = false;
	public bool specialDragon = false;

	public GameObject parent;
	public GameObject shield;
	float lifeSpan = 15.0f;
	float startTime;

	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
	}
	
	void BulletCheck()
	{
		GameObject sound = GameObject.Find ("Sound");
		
		GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
		GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
		
		for (int i = 0; i < bulletA.Length; i++)
		{
			if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				sound.GetComponent<SoundManager>().PlaySound("absorb");
				parent.GetComponent<PlayerStats>().ammo += 2;
				Destroy (bulletA[i].gameObject);
			}
		}
		for (int i = 0; i < bulletB.Length; i++)
		{
			if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				sound.GetComponent<SoundManager>().PlaySound("absorb");
				parent.GetComponent<PlayerStats>().ammo += 2;
				Destroy(bulletB[i].gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
//		Debug.Log (Time.time+"  "+startTime+" "+lifeSpan);
		if (Time.time - lifeSpan >= startTime && specialDragon == false) dead = true;
		
		if (parent.GetComponent<PlayerStats>().role == "Defender")
		{
			if (transform.childCount == 0)
			{
				
			}
			BulletCheck();
			if (parent.GetComponent<PlayerStats>().affinity == 'B') transform.GetChild(0).GetComponent<Animator>().SetInteger("State", 1);
			else transform.GetChild(0).GetComponent<Animator>().SetInteger("State", 2);
		}
		else transform.GetChild(0).GetComponent<Animator>().SetInteger("State", 0);
	}
}
