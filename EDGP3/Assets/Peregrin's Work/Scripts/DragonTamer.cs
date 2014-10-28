using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonTamer : MonoBehaviour {
	
	public float speed;
	public float dragDist;
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
	
	float attackTimer = .1f;
	float attackCD = 0;
	
	float swapCDTimer;
	float swapCD = 1f;
	
	float specialTimer;
	float specialCD = 1;
	bool specialActivate = false;
	
	Animator anim;	
	
	// Use this for initialization
	void Start () {
		//Determines whether player is 1 or 2
		if (GameObject.FindGameObjectWithTag("Player1") == null) transform.tag = "Player1";
		else transform.tag = "Player2";
		
		//Determines starting position, attacking/defending, and sprite depending on which player is controlling the ship.
		if (transform.tag == "Player2")
		{
			transform.GetComponent<PlayerStats>().role = "Defender";
			Shield ();
			Vector3 pos = new Vector3(0, 0);
			pos.z = 0;
			transform.position = pos;
		}
		if (transform.tag == "Player1")
		{
			transform.GetComponent<PlayerStats>().ammo = 100;
			transform.GetComponent<PlayerStats>().role = "Attacker";
			Vector3 pos = new Vector3 (0, 0);
			pos.z = 0;
			transform.position = pos;
		}
		
		anim = GetComponent<Animator>();
		
	}
	
	void Dragon()
	{
		Vector3 position = transform.position;
		position.x -= special.transform.renderer.bounds.extents.x*dragons.Count / 4;
		prevLoc1 = position;
		dragons.Add((Transform)(Instantiate (special, prevLoc1, Quaternion.identity)));
		
	}
	
	void Special()
	{
		transform.GetComponent<PlayerStats>().ammo -= 10;
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
			if((Vector3.Distance (dragons[i].position, transform.position) > i + dragDist)) dragons[i].position = Vector3.MoveTowards(dragons[i].position, transform.position, .5f);
			if (dragons[i].GetComponent<Dragon>().dead == true)
			{
				Destroy (dragons[i].gameObject);
				dragons.RemoveAt(i);
			}
		}
		
		if (shipType == "Attacker")
		{	
			if (transform.tag == "Player1")
			{
				if (Input.GetKey(KeyCode.Space) && attackTimer < Time.time - attackCD && transform.GetComponent<PlayerStats>().ammo > 0 && dragons.Count > 0)
				{
					foreach (Transform dragon in dragons) 
					{
						Transform bullet = (Transform)Instantiate(projectile, dragon.position, Quaternion.identity);
						bullet.GetComponent<SpriteRenderer>().sprite = Attack;
					}
					attackCD = Time.time;
					transform.GetComponent<PlayerStats>().ammo -= 1;
				}
			}
			else
			{
				if (Input.GetButton("XboxFire1") || Input.GetKey(KeyCode.Keypad0) && attackTimer < Time.time - attackCD && transform.GetComponent<PlayerStats>().ammo > 0 && dragons.Count > 0)
				{
					foreach (Transform dragon in dragons) 
					{
						GameObject bullet = (GameObject)Instantiate(projectile, dragon.position, Quaternion.identity);
						bullet.GetComponent<SpriteRenderer>().sprite = Attack;
					}
					attackCD = Time.time;
					transform.GetComponent<PlayerStats>().ammo -= 1;
				}
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
			if (Input.GetKeyUp(KeyCode.X)) specialActivate = false;
			else if (Time.time > specialTimer)
			{
				Dragon();
				specialActivate = false;
			}
		}
		
		if (transform.tag == "Player1")
		{
			if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetKey (KeyCode.X) && transform.GetComponent<PlayerStats>().ammo >= 10 && specialActivate == false) Special();
			if (Input.GetKey (KeyCode.Z) && transform.GetComponent<PlayerStats>().swapRole == "no" && Time.time - swapCDTimer >= 0)
			{
				GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
				transform.GetComponent<PlayerStats>().swapRole = "pending";
				if (player2.GetComponent<PlayerStats>().swapRole == "pending")
				{
					transform.GetComponent<PlayerStats>().Swap();
					player2.transform.GetComponent<PlayerStats>().Swap();
					swapCDTimer = Time.time + swapCD;	
				}
			}
		}
		if (transform.tag == "Player2")
		{
			GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
			if (Input.GetButton("XboxFire3") || Input.GetKey(KeyCode.Tab) && transform.GetComponent<PlayerStats>().swapRole == "no" && Time.time - swapCDTimer >= 0)
			{
				transform.GetComponent<PlayerStats>().swapRole = "pending";
				if (player1.GetComponent<PlayerStats>().swapRole == "pending")
				{
					transform.GetComponent<PlayerStats>().Swap();
					player1.transform.GetComponent<PlayerStats>().Swap();
					swapCDTimer = Time.time + swapCD;		
				}
			}
			transform.position += new Vector3(Input.GetAxis("XboxHorizontal")*Time.deltaTime*10, Input.GetAxis("XboxVertical")*Time.deltaTime*10, 0);
			if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.up*Time.deltaTime*5);
			if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left*Time.deltaTime*5);
			if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.down*Time.deltaTime*5);
			if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right*Time.deltaTime*5);
		}
		
	}
}
