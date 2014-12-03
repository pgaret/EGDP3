﻿using UnityEngine;
using System.Collections;

public class MirrorMage : MonoBehaviour {
	
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	
	public float width = .1f;
	public Color color = Color.white;
	
	public Material lazor;
	
	public string mode;
	
	GameObject sound;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	LineRenderer leftSide;
	LineRenderer rightSide;
	
	LineRenderer topLeft;
	LineRenderer topRight;
	
	GameObject left;
	GameObject right;
	
	// Use this for initialization
	void Start () {
	
		left = GameObject.Find ("Left");
		right = GameObject.Find ("Right");
		
		sound = GameObject.Find ("Sound");
		if (tag == "Player1")
		{
			mode = "Attack";
			LazerTime();
		}
		else
		{
			Shield ();
		}


	}
	
	void Special()
	{

	}
	
	public void Shield()
	{
		Instantiate(shield, transform.GetChild(0).position, Quaternion.identity);
		Instantiate(shield, transform.GetChild(0).position, Quaternion.identity);
	}
	
	void LazerTime()
	{
		//From player to mirrors lenses
		leftSide = left.AddComponent<LineRenderer>();
		rightSide = right.AddComponent<LineRenderer>();
		//From mirrors to top lenses
		topLeft = left.transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
		topRight = right.transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
		//Narrow the beams
		topLeft.SetWidth(width, width); topLeft.SetColors(color, color);
		topRight.SetWidth(width, width); topRight.SetColors(color, color);
		leftSide.SetWidth(width, width); leftSide.SetColors(color, color);
		rightSide.SetWidth(width, width); rightSide.SetColors(color, color);
	}
	
	void NotLazerTime()
	{
		Destroy (leftSide);
		Destroy (rightSide);
		Destroy (topRight);
		Destroy (topLeft);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GetComponent<PlayerStats>().role == "Defender" && GameObject.FindGameObjectsWithTag("MMShield").Length == 0)
		{
			Shield ();
			NotLazerTime();
		}
		if (GetComponent<PlayerStats>().role  == "Attacker" && GameObject.FindGameObjectsWithTag("MMShield").Length > 0)
		{
			LazerTime();
			GameObject[] shields = GameObject.FindGameObjectsWithTag("MMShield");
			foreach (GameObject shield in shields) Destroy (shield);
		}	
		if (GetComponent<PlayerStats>().role == "Attacker")
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShipA");
			RaycastHit hit;
	
			if (Physics.Raycast(transform.position, leftSide.transform.position - transform.position, out hit,  Vector3.Distance(transform.position, leftSide.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
			}
			if (Physics.Raycast(transform.position, rightSide.transform.position - transform.position, out hit,  Vector3.Distance(transform.position, rightSide.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
			}
			if (Physics.Raycast(leftSide.transform.position, topLeft.transform.position - leftSide.transform.position, out hit, Vector3.Distance(leftSide.transform.position, topLeft.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
			}
			if (Physics.Raycast(rightSide.transform.position, topRight.transform.position - rightSide.transform.position, out hit, Vector3.Distance(rightSide.transform.position, topRight.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
			}
			
			Debug.Log (rightSide.transform.position+" "+topRight.transform.position);
			
			//Beam follows the player
			Vector3 pos = transform.position;
			pos.z = -width;
			
			leftSide.SetPosition(0, pos);
			rightSide.SetPosition(0, pos);
			
			//Beam goes to left and right sides
			leftSide.SetPosition(1, left.transform.position);
			rightSide.SetPosition(1, right.transform.position);
			
			//Beam starts at left and right sides
			topLeft.SetPosition(0, left.transform.position);
			topRight.SetPosition(0, right.transform.position);
			
			//Change position of top destination
			pos = left.transform.GetChild(1).position;
			pos.x = transform.position.x;
			pos.y = -transform.position.y;
			left.transform.GetChild(1).position = pos;
			right.transform.GetChild(1).position = pos;
			
			
			//Beam goes to the top
			topLeft.SetPosition(1, left.transform.GetChild(1).transform.position);
			topRight.SetPosition(1, right.transform.GetChild(1).transform.position);
		}
		
		
	}
}
