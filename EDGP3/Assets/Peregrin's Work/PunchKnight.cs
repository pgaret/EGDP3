﻿using UnityEngine;
using System.Collections;

public class PunchKnight : MonoBehaviour {
	
	public float speed;
	public Transform projectile;
	public bool shipType;
	public int ammo;
	public Sprite Attack;
	public Sprite TypeB;
	public Sprite TypeA;
	
	float attackTimer = .1f;
	float attackCD = 0;

	bool affinity = false;

	bool start = false;
	
	// Use this for initialization
	void Start () {
		//Determines starting position, attacking/defending, and sprite depending on which player is controlling the ship.
		if (GameObject.FindGameObjectWithTag("Player1") == null) transform.tag = "Player1";
		else transform.tag = "Player2";
		if (transform.tag == "Player2")
		{
			shipType = false;
			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width * 7 / 8, Screen.height / 8, 0));
			pos.z = 0;
			transform.position = pos;
			gameObject.GetComponent<SpriteRenderer> ().sprite = TypeA;
		}
		if (transform.tag == "Player1")
		{
			ammo = 10;
			shipType = true;
			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width / 8, Screen.height / 8, 0));
			pos.z = 0;
			transform.position = pos;
		}
		
		
	}
	
	void GetStarted()
	{

	}
	
	// Update is called once per frame
	void Update () {
		GetStarted();
		transform.GetComponent<PlayerStats>().ammo = ammo;
		if (shipType == false)
		{
			if (affinity == true) gameObject.GetComponent<SpriteRenderer>().sprite = TypeA;
			else gameObject.GetComponent<SpriteRenderer>().sprite = TypeB;
		}
		if (shipType == true) gameObject.GetComponent<SpriteRenderer>().sprite = Attack;
		if (shipType == false)
		{
			GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
			GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");

			for (int i = 0; i < bulletA.Length; i++)
			{
				if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == false)
				{
					Destroy (bulletA[i].gameObject);
				}
				else if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == true)
				{
					ammo += 1;
					Destroy (bulletA[i].gameObject);
				}
			}
			for (int i = 0; i < bulletB.Length; i++)
			{
				if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == false)
				{
					ammo += 1;
					Destroy(bulletB[i].gameObject);
				}
				else if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == true)
				{
					Destroy(bulletB[i].gameObject);
				}
			}
			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
			{
				affinity = !affinity;
			}
		}
		else
		{	
			if (Input.GetKey(KeyCode.Space) && attackTimer < Time.time - attackCD && ammo > 0)
			{
				foreach (Transform child in transform) Instantiate (projectile, child.position, Quaternion.identity);
				attackCD = Time.time;
				ammo -= 1;
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

		if (transform.tag == "Player1")
		{
			if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right*Time.deltaTime*speed);
		}
		else if (transform.tag == "Player2")
		{
			if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right*Time.deltaTime*speed);
		}
	}
}