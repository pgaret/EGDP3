using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int ammo;
	public int lives;
	public int score;
	public int points;
	public char affinity;
	public string role;
	
	public float speed;
	bool isMoving;	

	public float shootCD;
	public bool shootBool;
	float shootTimer;
	
	public float specialCD;
	public bool specialBool;
	float specialTimer;
	
	public string swapRole;
	public float swapCD = .5f;
	float swapTimer = 2f;
	
	private Animator anim;
	private GameObject bar;

	// Use this for initialization
	void Start () {
		//Starting stats
		lives = 5;
		swapRole = "no";
		affinity = 'A';
		points = 0;
		score = 0;
		
		//Determines whether player is 1 or 2
		if (GameObject.FindGameObjectWithTag("Player1") == null) transform.tag = "Player1";
		if (transform.tag == null) transform.tag = "Player2";
		
		//Determines position based on player, not really active atm
		if (transform.tag == "Player2")
		{
			role = "Defender";
			Vector3 pos = new Vector3(0, 0);
			pos.z = 0;
			transform.position = pos;
			bar = GameObject.FindGameObjectWithTag("Bar2");
		}
		if (transform.tag == "Player1")
		{
			ammo = 100;
			role = "Attacker";
			Vector3 pos = new Vector3 (0, 0);
			pos.z = 0;
			transform.position = pos;
			bar = GameObject.FindGameObjectWithTag("Bar1");
		}
		
		//Access the animator so I can animate the character
		anim = GetComponent<Animator>();
	}
	//Swapping roles
	public void Swap()
	{	
		//Actual role swapping
		swapRole = "no";
		if (role == "Attacker") role = "Defender";
		else role = "Attacker";
		//Things that happen specific to chars as a result of role swapping
		if (transform.name == "PunchKnight(Clone)")
		{
			if (role != "Defender")Destroy(transform.FindChild("Shield(Clone)").gameObject);
			if (role == "Defender")transform.GetComponent<PunchKnight>().Shield();

		}
		if (transform.name == "DragonTamer(Clone)")
		{
			if (role != "Defender")
			{
				GameObject[] dragons = GameObject.FindGameObjectsWithTag("Dragon");
				for (int i = 0; i < dragons.Length; i++)
				{
					for (int j = 0; j < dragons[i].transform.childCount; j++)
					{
						if (dragons[i].transform.GetChild(j).name == "DTShield(Clone") Destroy (dragons[i].transform.GetChild(j).gameObject);
					}	
				}
			}
			if (role == "Defender")transform.GetComponent<DragonTamer>().Shield();
			
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		//If hit with bullets, lose life
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletA");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds)) lives -= 1;
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds)) lives -= 1;
		}
		//If find coin, gain points
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		foreach (GameObject coin in coins)
		{
			if (transform.renderer.bounds.Intersects(coin.renderer.bounds))
			{
				points += 1;
				bar.transform.localScale += new Vector3(0, 1, 0);
				Destroy (coin.gameObject);
			}
		}
		//Input shenanigans
		if (transform.tag == "Player1")
		{
			//Movement
			if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)) isMoving = true;
			else isMoving = false;
			//Swap
			if (Input.GetKey (KeyCode.Z) && swapRole == "no" && Time.time > swapTimer)
			{
				GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
				swapRole = "pending";
				if (player2.GetComponent<PlayerStats>().swapRole == "pending")
				{
					Swap();
					player2.transform.GetComponent<PlayerStats>().Swap();
					swapTimer = Time.time + swapCD;	
				}
			}
			//Attacker inputs
			if (role == "Attacker")
			{
				//Shooting
				if (Input.GetKey(KeyCode.Space) && Time.time > shootTimer && shootBool == false)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if (Input.GetKey(KeyCode.X) && Time.time > specialTimer && specialBool == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
			}
			//Defender inputs
			else
			{
				if (Input.GetKey(KeyCode.C))
				{
					if (affinity == 'A') affinity = 'B';
					else affinity = 'A';
				}
			}
			//Animation stuff
			if (isMoving && !shootBool) anim.SetBool("move!shoot", true);
			else anim.SetBool("move!shoot", false);
			if (!isMoving && shootBool) anim.SetBool("shoot!move", true);
			else anim.SetBool("shoot!move", false);
			if (isMoving && shootBool) anim.SetBool("moveshoot", true);
			else anim.SetBool("moveshoot", false);
			
		}
		else
		{
			//Movement
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("XboxVertical") > 0) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis ("XboxHorizontal") < 0) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis ("XboxVertical") < 0) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis ("XboxHorizontal") > 0) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("XboxVertical") > 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis ("XboxHorizontal") < 0 || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis ("XboxVertical") < 0 || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis ("XboxHorizontal") > 0) isMoving = true;
			else isMoving = false;
			//Swap
//			if (Input.GetButton("XboxFire1")) Debug.Log ("Fire1");
//		    if (Input.GetButton("XboxFire2")) Debug.Log ("Fire2");
//		    if (Input.GetButton("XboxFire3")) Debug.Log ("Fire3");
			if (Input.GetButton("XboxFire2") || Input.GetKey(KeyCode.Less) && swapRole == "no" && Time.time > swapTimer)
			{
				GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
				swapRole = "pending";
				if (player1.GetComponent<PlayerStats>().swapRole == "pending")
				{
					Swap();
					player1.transform.GetComponent<PlayerStats>().Swap();
					swapTimer = Time.time + swapCD;		
				}
			}
			if (role == "Attacker")
			{
				//Shooting
				if ((Input.GetKey(KeyCode.Question) || Input.GetButton("XboxFire1")) && Time.time > shootTimer && shootBool == false)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if ((Input.GetKey(KeyCode.Greater) || Input.GetButton("XboxFire3")) && Time.time > specialTimer && specialBool == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
			}
			//Defender inputs
			else
			{
				if (Input.GetKey(KeyCode.RightShift) || Input.GetButton("XboxFire3"))
				{
					if (affinity == 'A') affinity = 'B';
					else affinity = 'A';
				}
			}
			//Animation stuff
			if (isMoving && !shootBool) anim.SetBool("move!shoot", true);
			else anim.SetBool("move!shoot", false);
			if (!isMoving && shootBool) anim.SetBool("shoot!move", true);
			else anim.SetBool("shoot!move", false);
			if (isMoving && shootBool) anim.SetBool("moveshoot", true);
			else anim.SetBool("moveshoot", false);
		}
		
		
	}
}
