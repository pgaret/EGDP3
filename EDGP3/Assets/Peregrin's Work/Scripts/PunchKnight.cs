using UnityEngine;
using System.Collections;

public class PunchKnight : MonoBehaviour {

	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	public Sprite punchLeft;
	public Sprite punchRight;
	
	GameObject sound;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Sound");
		if (transform.tag == "Player2") Shield ();	
	}
	
	void Special()
	{
		specialCounter = transform.GetComponent<PlayerStats>().ammo / 5;
		transform.GetComponent<PlayerStats>().ammo = 0;
		specialTimer = Time.time + .5f;
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
			sound.GetComponent<SoundManager>().PlaySound("punch");
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
		
		if (specialTimer - Time.time <= specialCounter  && Time.time > specialTimer && specialCounter > 0)
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
			specialTimer = Time.time + .5f;
			specialCounter -= 1f;
		}

	}
}
