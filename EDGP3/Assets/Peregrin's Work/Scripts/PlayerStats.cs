	using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	
	//Basic stats
	public int ammo;
	public int lives;
	public float score;
	public float coinScore;
	public int points;
	public char affinity;
	public string role;
	public float damage;
	
	public Transform swapTransition;
	public Transform powerUp;
	
	
	//Speed and movement testing for animation purposes
	public float speed;
	bool isMoving;
	
	//Boundaries of the screen
	GameObject top;
	GameObject left;
	GameObject right;
	GameObject down;
	
	//Can we shoot?  How long has it been since we last shot?  Time to shoot!
	public float shootCD;
	public bool shootBool;
	float shootTimer;
	
	//Special stuff (are we in tutorial, when did we last use the special, etc)
	public float specialCD;
	public bool specialBool;
	public bool tutSpecial = false;
	public bool hasSwapped = false;
	float specialTimer;
	
	//Role Swapping stuff (attacker vs defender)
	public string swapRole;
	public float swapCD = .5f;
	public bool tutSwap = false;
	public bool swap = false;
	float swapTimer = 2f;
	
	// Affinity timer
	public float affinityCD;
	float affinityTimer;
	
	public float deathCD;
	float deathTimer;
	
	public float flinchCD;
	bool flinchBool = false;
	
	public float pointCD;
	float pointTimer;
	
	public float tinkerCD;
	float tinkerTimer;
	
	private Animator anim;
	private GameObject bar;
	private GameObject sound;
	
	GameObject otherPlayer;
	
	public float coin1 = 10;
	public float coin2 = 100;
	public float coin3 = 150;
	
	bool coin1d = false;
	bool coin2d = false;
	bool coin3d = false;
	
	//Check whether its time to give additional lives based on points;
	int pointChecker = 1000;
	
	// Use this for initialization
	void Start () {
		//Starting stats
		swapRole = "no";
		affinity = 'A';
		points = 0;
		score = 0;
		
		pointTimer = Time.time;
		tinkerTimer = Time.time;
		
		top = GameObject.Find ("Up");
		down = GameObject.Find ("Down");
		left = GameObject.Find("Left");
		right = GameObject.Find ("Right");
		sound = GameObject.Find ("Sound");
		
		
		
		//Determines whether player is 1 or 2
		if (GameObject.FindGameObjectWithTag("Player1") == null) transform.tag = "Player1";
		if (transform.tag == "Untagged") transform.tag = "Player2";
		
		//Determines position based on player, not really active atm
		if (transform.tag == "Player2")
		{
			ammo = 50;
			role = "Defender";
			bar = GameObject.FindGameObjectWithTag("Bar2");
		}
		if (transform.tag == "Player1")
		{
			ammo = 50;
			role = "Attacker";
			bar = GameObject.FindGameObjectWithTag("Bar1");
		}
		//Access the animator so I can animate the character
		anim = GetComponent<Animator>();
		
	}
	//Swapping roles
	public void Swap()
	{	
		sound.GetComponent<SoundManager>().PlaySound("playerSwap");
		//Actual role swapping
		sound.GetComponent<SoundManager>().StopSound("Pending");
		swapRole = "no";
		if (role == "Attacker") role = "Defender";
		else role = "Attacker";
		//Things that happen specific to chars as a result of role swapping
		if (transform.name == "PunchKnight(Clone)")
		{
			if (role != "Defender")Destroy(transform.FindChild("PKShield(Clone)").gameObject);
			if (role == "Defender")transform.GetComponent<PunchKnight>().Shield();
		}
		
		hasSwapped = true;
		
	}
	
	public void GotCoin()
	{
		bar.transform.localScale += new Vector3(0, .1f, 0);
		sound.GetComponent<SoundManager>().PlaySound("Powerup");
		coinScore += 10;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (score > pointChecker)
		{
			lives += 1;
			pointChecker += 1000;
			score += 20;
		}
		
		if (transform.tag == "Player1") otherPlayer = GameObject.FindGameObjectWithTag("Player2");
		else otherPlayer = GameObject.FindGameObjectWithTag("Player1");
		
		if (coinScore >= coin1 && coin1d == false)
		{
			coin1d = true;
			Vector3 pos = new Vector3(transform.position.x, transform.position.y - renderer.bounds.extents.y);
			Transform power = Instantiate(powerUp, pos, Quaternion.identity) as Transform;
			power.parent = transform;
			power.renderer.sortingOrder = 1;
		}
		
		if (Time.time > pointTimer)
		{
			pointTimer = Time.time + pointCD;
			score += (transform.position.y + 5);
		}
		//If hit with bullets, lose life
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletA");
		int currentLives = lives;
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
				Destroy (bullet.gameObject);
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("Boss2");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
				Destroy (bullet.gameObject);
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("FireBall");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
				Destroy (bullet.gameObject);
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("Fire");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("blaze");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.GetComponent<Wings>().on == true && GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("lightn");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<SphereCollider>().bounds) && Time.time > deathTimer)
			{
				lives -= 1;
			}
		}
		if (currentLives == lives + 1)
		{
			GetComponent<Animator>().SetBool("dead", true);
			deathTimer = Time.time + deathCD;
			if (transform.name == "DragonTamer(Clone)") GetComponent<DragonTamer>().DestroyDragon();
		}
		
		//If find coin, gain points
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		foreach (GameObject coin in coins)
		{
			if (transform.renderer.bounds.Intersects(coin.renderer.bounds))
			{
				points += 1;
				coin.GetComponent<Coin>().MoveTowards(bar.transform.position, transform);
				sound.GetComponent<SoundManager>().PlaySound("coin");
				//Change animation when picked up
				coin.GetComponent<Animator>().SetBool("Obtain", true);
			}
		}
		
		if (Time.time > deathTimer && GetComponent<Animator>().GetBool("dead") == true)
		{
			GetComponent<Animator>().SetBool("dead", false);
			GetComponent<Animator>().SetBool("flinch", true);
			deathTimer += flinchCD;
			flinchBool = true;
		}
		if (Time.time > deathTimer && flinchBool)
		{
			GetComponent<Animator>().SetBool("flinch", false);
			flinchBool = false;
		}
		//Input shenanigans
		if (transform.tag == "Player1")
		{
			//Keyboard
			if (Input.GetAxis("Vertical") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("Horizontal") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("Vertical") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("Horizontal") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			
			//Movement
			if (Input.GetAxis("XboxVertical1") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal1") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxVertical1") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal1") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetAxis("XboxHorizontal1") != 0 || Input.GetAxis("XboxVertical1") != 0) isMoving = true;
			else isMoving = false;
			//Swap
			if ((Input.GetButtonUp("XboxFire1Y") || Input.GetKeyUp(KeyCode.O)) && Time.time > swapTimer)
			{
				swapTimer = Time.time + swapCD;
				Transform theSwap = Instantiate(swapTransition, transform.position, Quaternion.identity) as Transform;
				theSwap.parent = transform;
				theSwap.GetComponent<Animator>().speed -= .1f;
				if (role == "Attacker") theSwap.GetComponent<Animator>().SetBool("TowardsOffense", false);
				else theSwap.GetComponent<Animator>().SetBool("TowardsOffense", true);
				theSwap = Instantiate(swapTransition, otherPlayer.transform.position, Quaternion.identity) as Transform;
				theSwap.GetComponent<Animator>().speed -= .1f;
				if (otherPlayer.GetComponent<PlayerStats>().role == "Attacker") theSwap.GetComponent<Animator>().SetBool("TowardsOffense", false);
				else theSwap.GetComponent<Animator>().SetBool("TowardsOffense", true);
				theSwap.parent = otherPlayer.transform;
				swap = true;
			}
			
			//Attacker inputs
			if (role == "Attacker")
			{
				//Shooting
				if ((Input.GetButton("XboxFire1X") || Input.GetButton("XboxFire1A") ||Input.GetKey(KeyCode.I)) && Time.time > shootTimer && shootBool == false && ammo > 0)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if ((Input.GetButtonUp("XboxFire1B") || Input.GetKeyUp(KeyCode.P)) && Time.time > specialTimer && specialBool == false && tutSpecial == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
				if (transform.name == "PunchKnight(Clone)")
				{
					if(GetComponent<PunchKnight>().specialCounter != 0) shootBool = true;
				}
			}
			//Defender inputs
			else if ((Input.GetButtonUp("XboxFire1X") || Input.GetButton("XboxFire1A") || Input.GetKeyUp(KeyCode.I)) && Time.time > affinityTimer)
			{
				if (affinity == 'A') affinity = 'B';
				else affinity = 'A';
				// affinity timer
				affinityTimer = affinityCD + Time.time;
			}
			else if (transform.name == "Tinker(Clone)" && Time.time > tinkerTimer)
			{
				if (Input.GetButtonUp("XboxFire1B") || Input.GetKey(KeyCode.P))
				{
					GetComponent<Tinker>().Shield();
					tinkerTimer = Time.time + tinkerCD;
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
			//Keyboard
			if (Input.GetAxis("Vertical2") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("Horizontal2") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("Vertical2") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("Horizontal2") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			
		
			//Movement
			if (Input.GetAxis("XboxVertical2") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal2") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxVertical2") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal2") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetAxis("XboxVertical2") != 0 ||  Input.GetAxis ("XboxHorizontal2") != 0) isMoving = true;
			else isMoving = false;
			
			if ((Input.GetButtonUp("XboxFire2Y") || Input.GetKeyUp(KeyCode.V)) && Time.time > swapTimer)
			{
				swapTimer = Time.time + swapCD;
				Transform theSwap = Instantiate(swapTransition, transform.position, Quaternion.identity) as Transform;
				theSwap.parent = transform;
				theSwap.GetComponent<Animator>().speed -= .1f;
				theSwap = Instantiate(swapTransition, otherPlayer.transform.position, Quaternion.identity) as Transform;
				theSwap.parent = otherPlayer.transform;
				theSwap.GetComponent<Animator>().speed -= .1f;
				swap = true;
			}
			if (role == "Attacker")
			{
				//Shooting
				if ((Input.GetButton("XboxFire2X") || Input.GetButton("XboxFire2A") ||Input.GetKey(KeyCode.C)) && Time.time > shootTimer && shootBool == false && ammo > 0)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if ((Input.GetButton("XboxFire2B") || Input.GetKey(KeyCode.B)) && Time.time > specialTimer && specialBool == false && tutSpecial == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
				if (transform.name == "PunchKnight(Clone)")
				{
					if(GetComponent<PunchKnight>().specialCounter != 0) shootBool = true;
				}
			}
			//Defender inputs
			else if ((Input.GetButtonUp ("XboxFire2X") || Input.GetButtonUp ("XboxFire2A") || Input.GetKey(KeyCode.C)) && Time.time > affinityTimer)
			{
				if (affinity == 'A') affinity = 'B';
				else affinity = 'A';
				affinityTimer = affinityCD + Time.time;
			}
			else if (transform.name == "Tinker(Clone)" && Time.time > tinkerTimer)
			{
				if (Input.GetButtonUp("XboxFire2B") || Input.GetKey(KeyCode.B))
				{
					GetComponent<Tinker>().Shield();
					tinkerTimer = Time.time + tinkerCD;
				}
				if (transform.name == "PunchKnight(Clone)")
				{
					if (GetComponent<PunchKnight>().specialCounter != 0) shootBool = true;
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
		
		if (swap == true && Time.time > swapTimer)
		{
			Swap ();
			otherPlayer.transform.GetComponent<PlayerStats>().Swap();
			swap = false;
			GameObject[] swaps = GameObject.FindGameObjectsWithTag("swap");
			foreach (GameObject aSwap in swaps) Destroy (aSwap);
		}
		
	}
}
