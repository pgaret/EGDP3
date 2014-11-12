using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	//Basic stats
	public int ammo;
	public int lives;
	public float score;
	public int points;
	public char affinity;
	public string role;
	
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
	float specialTimer;
	
	//Role Swapping stuff (attacker vs defender)
	public string swapRole;
	public float swapCD = .5f;
	public bool tutSwap = false;
	float swapTimer = 2f;
	
	public float pointCD;
	float pointTimer;
	
	private Animator anim;
	private GameObject bar;
	private GameObject sound;

	// Use this for initialization
	void Start () {
		//Starting stats
		lives = 5;
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
			role = "Defender";
			bar = GameObject.FindGameObjectWithTag("Bar2");
		}
		if (transform.tag == "Player1")
		{
			ammo = 100;
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
			if (role != "Defender")Destroy(transform.FindChild("Shield(Clone)").gameObject);
			if (role == "Defender")transform.GetComponent<PunchKnight>().Shield();

		}
		if (transform.name == "DragonTamer(Clone)")
		{

			
		}
		
	}
	
	public void GotCoin()
	{
		bar.transform.localScale += new Vector3(0, .01f, 0);
		sound.GetComponent<SoundManager>().PlaySound("Powerup");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > pointTimer)
		{
			pointTimer = Time.time + pointCD;
			score += (transform.position.y + 5);
		}
		//If hit with bullets, lose life
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletA");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds))
			{
				if (points >= 50) points -= 50;
				else points = 0;
				Destroy (bullet.gameObject);
			}
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (GetComponent<BoxCollider2D>().bounds.Intersects(bullet.renderer.bounds))
			{
				if (points >= 50) points -= 50;
				else points = 0;
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
		if (swapRole == "pending")
		{
			if (Time.time > swapTimer)
			{
				swapRole = "no";
				sound.GetComponent<SoundManager>().StopSound("Pending");
			}
		}
		//Input shenanigans
		if (transform.tag == "Player1")
		{
			//Movement
			if (Input.GetKey(KeyCode.W) && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.A) && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.S) && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.D) && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S)) isMoving = true;
			else isMoving = false;
			//Swap
			if (Input.GetKeyUp (KeyCode.Z) && swapRole == "no" && tutSwap == false)
			{
				GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
				swapRole = "pending";
				if (player2.GetComponent<PlayerStats>().swapRole == "pending")
				{
					Swap();
					player2.transform.GetComponent<PlayerStats>().Swap();
				}
				else
				{
					sound.GetComponent<SoundManager>().PlaySound("Pending");
					sound.GetComponent<SoundManager>().LoopSound("Pending");
				}
				swapTimer = Time.time + swapCD;	
			}

			//Attacker inputs
			if (role == "Attacker")
			{
				//Shooting
				if (Input.GetKey(KeyCode.Space) && Time.time > shootTimer && shootBool == false && ammo > 0)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if (Input.GetKey(KeyCode.X) && Time.time > specialTimer && specialBool == false && tutSpecial == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
			}
			//Defender inputs
			else if (Input.GetKeyUp(KeyCode.Space))
			{
				if (affinity == 'A') affinity = 'B';
				else affinity = 'A';
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
			if (Input.GetAxis("XboxVertical") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxVertical") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("XboxVertical") > 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis ("XboxHorizontal") < 0 || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis ("XboxVertical") < 0 || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis ("XboxHorizontal") > 0) isMoving = true;
			else isMoving = false;
			//Swap
//			if (Input.GetButton("XboxFire1")) Debug.Log ("Fire1");
//		    if (Input.GetButton("XboxFire2")) Debug.Log ("Fire2");
//		    if (Input.GetButton("XboxFire3")) Debug.Log ("Fire3");
			if ((Input.GetButtonUp("XboxFire2") || Input.GetKeyUp(KeyCode.P)) && swapRole == "no" && tutSwap == false)
			{
				GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
				swapRole = "pending";
				if (player1.GetComponent<PlayerStats>().swapRole == "pending")
				{
					Swap();
					player1.transform.GetComponent<PlayerStats>().Swap();	
				}
				else
				{
					sound.GetComponent<SoundManager>().PlaySound("Pending");
					sound.GetComponent<SoundManager>().LoopSound("Pending");
				}
				swapTimer = Time.time + swapCD;	
			}
			if (role == "Attacker")
			{
				//Shooting
				if (Input.GetButton("XboxFire1") && Time.time > shootTimer && shootBool == false && ammo > 0)
				{
					shootBool = true;
					shootTimer = Time.time + shootCD;
				}
				//Special
				if (Input.GetButton("XboxFire3") && Time.time > specialTimer && specialBool == false && tutSpecial == false)
				{
					specialBool = true;
					specialTimer = Time.time + specialCD;
				}
			}
			//Defender inputs
			else if (Input.GetButtonUp ("XboxFire1"))
			{
				if (affinity == 'A') affinity = 'B';
				else affinity = 'A';
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
