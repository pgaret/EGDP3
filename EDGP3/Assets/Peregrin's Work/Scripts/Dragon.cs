using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	public bool dead = false;
	public bool specialDragon = false;

	public GameObject parent;
	float lifeSpan = 15.0f;
	float startTime;
	
	public Sprite A;
	public Sprite B;

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
		
		if (parent.GetComponent<PlayerStats>().role == "Defender") BulletCheck();
		
		if (parent.GetComponent<PlayerStats>().affinity == 'A') transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = A;
		else transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = B;
	}
}
