﻿using UnityEngine;
using System.Collections;

public class Tinker : MonoBehaviour {

	public Transform projectile;
	public Transform shield;
	public string shipType;
	
	public Sprite YellowL;
	public Sprite YellowR;
	public Sprite RedL;
	public Sprite RedR;
	public Sprite BlueL;
	public Sprite BlueR;
	
	Sprite attackRight;
	Sprite attackLeft;
	
	public int attackType = 0;
	
	public float spawnCD;
	float spawnTimer;
	
	GameObject sound;
	
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Sound");
		if (transform.tag == "Player2") Shield ();	
	}
	
	public void Shield()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (attackType == 0)
		{
			attackLeft = BlueL; attackRight = BlueR;
		}
		else if (attackType == 1)
		{
			attackLeft = RedL; attackRight = RedR;
		}
		else 
		{
			attackLeft = YellowL; attackRight = YellowR;
		}
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
					thePunch.GetComponent<SpriteRenderer>().sprite = attackLeft;
					thePunch.GetComponent<TinkerBullets>().type = attackType;
					transform.GetComponent<PlayerStats>().ammo -= 1;
				}
				else if (child.name == "Right")
				{
					thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
					thePunch.GetComponent<SpriteRenderer>().sprite = attackRight;
					thePunch.GetComponent<TinkerBullets>().type = attackType;
					transform.GetComponent<PlayerStats>().ammo -= 1;
				}
				GetComponent<PlayerStats>().shootBool = false;
			}
		}
		
		if (GetComponent<PlayerStats>().specialBool == true && GetComponent<PlayerStats>().role == "Attacker")
		{
			attackType += 1;
			if (attackType > 2) attackType = 0;
			GetComponent<Animator>().SetInteger("type", attackType);
			GetComponent<PlayerStats>().specialBool = false;
		}
		
		if (GetComponent<PlayerStats>().role == "Defender" && Time.time > spawnTimer && Input.GetKeyUp(KeyCode.I))
		{
			Instantiate(shield, transform.position, Quaternion.identity);
			spawnTimer = Time.time + spawnCD;
		}

		
	}
}
