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
	
	//Affinity swapping stuff (A vs B)
	public Texture AffinityA;
	public Texture AffinityB;
	
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
	float swapTimer = 2f;
	
	// Affinity timer
	public float affinityCD;
	float affinityTimer;
	
	public float pointCD;
	float pointTimer;
	
	private Animator anim;
	private GameObject bar;
	private GameObject sound;

	// Use this for initialization
	void Start () {
		//Starting stats
		lives = 10000000;
		swapRole = "no";
		affinity = 'A';
		points = 0;
		score = 0;
		
		pointTimer = Time.time;
		
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
		bar.transform.localScale += new Vector3(0, .01f, 0);
		sound.GetComponent<SoundManager>().PlaySound("Powerup");
		coinScore += 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		Debug.Log (transform.name+": "+coinScore);
	
		if (Time.time > pointTimer)
		{
			pointTimer = Time.time + pointCD;
			score += (transform.position.y + 5);
		}
		//If hit with bullets, lose life
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletA");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds))
			{
				lives -= 1;
				Destroy (bullet.gameObject);
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.GetComponent<BoxCollider>().bounds))
			{
				lives -= 1;
				Destroy (bullet.gameObject);
			}
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
				
			}
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
			if ((Input.GetButtonUp("XboxFire1Y") || Input.GetKeyUp(KeyCode.P)) && Time.time > swapTimer)
			{
				GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
				Swap();
				player2.transform.GetComponent<PlayerStats>().Swap();
				swapTimer = Time.time + swapCD;	
			}

			//Attacker inputs
			if (role == "Attacker")
			{
				//Shooting
				if ((Input.GetButton("XboxFire1A") || Input.GetKey(KeyCode.I)) && Time.time > shootTimer && shootBool == false && ammo > 0)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if ((Input.GetButtonUp("XboxFire1B") || Input.GetKeyUp(KeyCode.T)) && Time.time > specialTimer && specialBool == false && tutSpecial == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
			}
			//Defender inputs
			else if (Input.GetButtonUp("XboxFire1X") || Input.GetKeyUp(KeyCode.T) && Time.time > affinityTimer)
			{
				if (affinity == 'A') affinity = 'B';
				else affinity = 'A';
				// affinity timer
				affinityTimer = affinityCD + Time.time;
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
			if (Input.GetAxis("XboxVertical2") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal2") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxVertical2") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal2") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetAxis("XboxVertical2") != 0 ||  Input.GetAxis ("XboxHorizontal2") != 0) isMoving = true;
			else isMoving = false;
			
			if ((Input.GetButtonUp("XboxFire2Y") || Input.GetKeyUp(KeyCode.O)) && Time.time > swapTimer)
			{
				GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
				Swap();
				player1.transform.GetComponent<PlayerStats>().Swap();	
				swapTimer = Time.time + swapCD;	
			}
			if (role == "Attacker")
			{
				//Shooting
				if (Input.GetButton("XboxFire2A") && Time.time > shootTimer && shootBool == false && ammo > 0)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if (Input.GetButton("XboxFire2B") && Time.time > specialTimer && specialBool == false && tutSpecial == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
			}
			//Defender inputs
			else if (Input.GetButtonUp ("XboxFire2X") && Time.time > affinityTimer)
			{
				if (affinity == 'A') affinity = 'B';
				else affinity = 'A';
				affinityTimer = affinityCD + Time.time;
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
