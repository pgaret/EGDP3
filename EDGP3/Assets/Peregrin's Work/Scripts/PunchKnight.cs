using UnityEngine;
using System.Collections;

public class PunchKnight : MonoBehaviour {
	
	public float speed;
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	public Sprite punchLeft;
	public Sprite punchRight;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	// Use this for initialization
	void Start () {
		if (transform.tag == "Player2") Shield ();
		
	}
	
	void Special()
	{
		if (GetComponent<PlayerStats>().ammo >= 100)
		{
			transform.GetComponent<PlayerStats>().ammo -= 100;
			specialTimer = Time.time + 2.1f;
			specialCounter = 2f;
		}
	}
	
	public void Shield()
	{
		Instantiate(shield);
	}
	
	// Update is called once per frame
	void Update ()
	{
		shipType = transform.GetComponent<PlayerStats>().role;

		if (GetComponent<PlayerStats>().shootBool == true)
		{
			foreach (Transform child in transform)
			{
				Transform thePunch;
				if (child.name == "Left")
				{
					thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
					thePunch.GetComponent<SpriteRenderer>().sprite = punchLeft;
				}
				else if (child.name == "Right")
				{
					thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
					thePunch.GetComponent<SpriteRenderer>().sprite = punchRight;
				transform.GetComponent<PlayerStats>().ammo -= 1;
			}
			GetComponent<PlayerStats>().shootBool = false;
			}
		}
		
		if (GetComponent<PlayerStats>().specialBool == true)
		{
			Special ();
			GetComponent<PlayerStats>().specialBool = false;
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
		
		if (specialTimer - Time.time <= specialCounter  && specialTimer - Time.time > 0)
		{
			Transform instance;
			for (float i = 0; i < specialCounter; i += .5f)
			{
				instance = (Transform)Instantiate(special, transform.position, Quaternion.identity);
				if (i == 0) instance.GetComponent<PKSpecial>().rotation = 13;
				if (i == .5) instance.GetComponent<PKSpecial>().rotation = -13;
				if (i == 1) instance.GetComponent<PKSpecial>().rotation = 25;
				if (i == 1.5) instance.GetComponent<PKSpecial>().rotation = -25;
				if (instance.GetComponent<PKSpecial>().rotation > 0) instance.GetComponent<SpriteRenderer>().sprite = punchLeft;
				else instance.GetComponent<SpriteRenderer>().sprite = punchRight;
			}
			instance = (Transform)Instantiate(special, transform.position, Quaternion.identity);
			instance.GetComponent<PKSpecial>().rotation = 0;
			instance.GetComponent<SpriteRenderer>().sprite = punchLeft;
			
			specialCounter -= .5f;
		}

	}
}
