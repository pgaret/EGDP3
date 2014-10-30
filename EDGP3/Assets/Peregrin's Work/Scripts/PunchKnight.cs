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
	
	float attackTimer = .1f;
	float attackCD = 0;
	
	float swapCountdown = 2f;
	float swapCountdownTimer;
	
	float swapCDTimer;
	float swapCD = 1f;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	Animator anim;
	
	// Use this for initialization
	void Start () {
		//Determines whether player is 1 or 2
		if (GameObject.FindGameObjectWithTag("Player1") == null) transform.tag = "Player1";
		if (transform.tag == null) transform.tag = "Player2";
		
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
	
	void Special()
	{
		transform.GetComponent<PlayerStats>().ammo -= 100;
		specialTimer = Time.time + 2.1f;
		specialCounter = 2f;
	}
	
	public void Shield()
	{
		Instantiate(shield);
	}
	
	// Update is called once per frame
	void Update () {
		shipType = transform.GetComponent<PlayerStats>().role;

		if (shipType == "Attacker")
		{	
			if (transform.tag == "Player1")
			{
				if (Input.GetKey(KeyCode.Space) && attackTimer < Time.time - attackCD && transform.GetComponent<PlayerStats>().ammo > 0)
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
						}
						attackCD = Time.time;
						transform.GetComponent<PlayerStats>().ammo -= 1;
					}
				}
			}
			else
			{
				if (Input.GetButton("XboxFire1") || Input.GetKey(KeyCode.Backspace) && attackTimer < Time.time - attackCD && transform.GetComponent<PlayerStats>().ammo > 0)
				{
					Debug.Log("SHOOTING");
					foreach (Transform child in transform) 
					{
						Transform thePunch;
						Debug.Log (child.name);
						if (child.name == "Left")
						{
							thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
							thePunch.GetComponent<SpriteRenderer>().sprite = punchLeft;
						}
						else if (child.name == "Right")
						{
							thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
							thePunch.GetComponent<SpriteRenderer>().sprite = punchRight;
						}
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

		if (Time.time - swapCountdownTimer > .49 && Time.time - swapCountdownTimer < .51 && swapCountdownTimer != 0)
		{
			swapCountdownTimer = 0;
		}
		
		if (specialTimer - Time.time <= specialCounter  && specialTimer - Time.time > 0)
		{
			Transform instance;
			for (float i = 0; i < specialCounter; i += .5f)
			{
//				Debug.Log (i+" "+specialTimer);
				instance = (Transform)Instantiate(special, transform.position, Quaternion.identity);
				if (i == 0) instance.GetComponent<PKSpecial>().rotation = 13;
				if (i == .5) instance.GetComponent<PKSpecial>().rotation = -13;
				if (i == 1) instance.GetComponent<PKSpecial>().rotation = 25;
				if (i == 1.5) instance.GetComponent<PKSpecial>().rotation = -25;
			}
			instance = (Transform)Instantiate(special, transform.position, Quaternion.identity);
			instance.GetComponent<PKSpecial>().rotation = 0;
			
			specialCounter -= .5f;
		}
		
		if (transform.tag == "Player1")
		{
			if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !Input.GetKey (KeyCode.Space)) anim.SetBool("move!shoot", true);
			else anim.SetBool("move!shoot", false);
			
			if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKey (KeyCode.Space)) anim.SetBool("moveshoot", true);
			else anim.SetBool("moveshoot", false);
			
			if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) && Input.GetKey (KeyCode.Space)) anim.SetBool("shoot!move", true);
			else anim.SetBool("shoot!move", false);
			
			if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetKey (KeyCode.X) && transform.GetComponent<PlayerStats>().ammo >= 100) Special();
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
				else swapCountdownTimer = Time.time + swapCountdown;
			}
		}
		if (transform.tag == "Player2")
		{
			if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) && !Input.GetKey (KeyCode.Space)) anim.SetBool("move!shoot", true);
			else anim.SetBool("move!shoot", false);
			
			if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)) && Input.GetKey (KeyCode.Space)) anim.SetBool("moveshoot", true);
			else anim.SetBool("moveshoot", false);
			
			if ((!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow)) && Input.GetKey (KeyCode.Space)) anim.SetBool("shoot!move", true);
			else anim.SetBool("shoot!move", false);
		
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
				else swapCountdownTimer = Time.time + swapCountdown;
			}
			transform.position += new Vector3(Input.GetAxis("XboxHorizontal")*Time.deltaTime*10, Input.GetAxis("XboxVertical")*Time.deltaTime*10, 0);
			if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right*Time.deltaTime*speed);
		}

	}
}
