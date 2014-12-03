using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	//Transforms to load into the game as the characters
	public Transform PunchKnight;
	public Transform MirrorMage;
	public Transform Tinker;
	public Transform DragonTamer;
	
	//Screen Transforms
	public Transform gameOver;
	public Transform charSelect;
	public Transform titleScreen;
	public Transform tutoBG;
	
	//Backgrounds
	public Transform Background;
	public Sprite Background1;
	public Sprite Background1B;
	public Sprite Background2;
	public Sprite Background2B;
	Transform level1BG;
	
	//Level mode (boss vs normal)
	public int gameMode;
	//Modes 0, 1, 2, 3, 4 = Title Screen, Character Selection, Game, Death, Tutorial
	public int mode = 0;
	
	//Tutorial script holding thingy
	public Transform tutorial;
	
	//HUD stuff
	//Buttons
	//Start
	public Transform startButton;
	public Sprite startButtonHover;
	public Sprite startButtonNormal;
	public Sprite startButtonInactive;
	Transform sButton;
	
	//Tutorial
	public Transform tutorialButton;
	public Sprite tutButtonHover;
	public Sprite tutButtonNormal;
	Transform tutButton;
	
	//Dragon
	public Transform DTP1;
	public Transform DTP2;
	
	//Punch
	public Transform PKP1;
	public Transform PKP2;
	
	//Mirror
    public Transform MMP1;
    public Transform MMP2;
    
	//Tinker
    public Transform TKP1;
    public Transform TKP2;

	//Icons for team select
	public Transform PK;
	public Transform MM;
	public Transform TK;
	public Transform DT;
	
	//Textures for boxes of selected characters
	public Texture PKT;
	public Texture TKT;
	public Texture MMT;
	public Texture DTT;

	//Cursors for players 1 & 2 in team select
	public Transform target1;
	public Transform target2;

	//GUI style for in-game HUD
	public GUIStyle player1Style;
	public GUIStyle player2Style;
	public GUIStyle style;

	//Player 1 & 2 characters so we can get in-game info about them live!
	Transform player1;
	Transform player2;
	
	//Cursors
	Transform player1Cursor;
	Transform player2Cursor;
	
	//Used for icon button locations/bounds
	Transform PKIcon;
	Transform MMIcon;
	Transform TKIcon;
	Transform DTIcon;
	
	//Used to check for/delete game screens
	Transform charaSelect;
	Transform tiScre;
	Transform tutBG;

	//Instantiation position vector
	Vector3 pos;
	
	//Holds the current player value, if both are filled then the game can begin
	int player1Select = -1;
	int player2Select = -1;
	bool begin = false;
	
	//Sound manager, plays the sounds
	GameObject sound;
	
	//Enemy manager, starts the enemy wave timer
	GameObject boxy;

	//death screen
	bool start = false;
	bool death = false;
	public Texture b;
	// Use this for initialization
	void Start ()
	{
		boxy = GameObject.FindGameObjectWithTag("Manager");
		sound = GameObject.Find ("Sound");
		Screen.SetResolution (960, 720, false);
		Screen.showCursor = false;
		player1Cursor = Instantiate (target1) as Transform;
		player2Cursor = Instantiate (target2) as Transform;
		player1Cursor.renderer.sortingOrder = 1;
		player2Cursor.renderer.sortingOrder = 1;

		pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		
		
	}
	public void ReturnFromTutorial()
	{
		player1Cursor = Instantiate (target1) as Transform;
		player2Cursor = Instantiate (target2) as Transform;
		player1Cursor.renderer.sortingOrder = 1;
		player2Cursor.renderer.sortingOrder = 1;
		
		pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
	}
	
	void OnGUI()
	{
		if (mode == 0)
		{
			if (!sound.GetComponent<SoundManager>().IsPlaying("CharSelect"))
			{
				sound.GetComponent<SoundManager>().PlaySound("CharSelect");
				sound.GetComponent<SoundManager>().LoopSound("CharSelect");
			}
			if (tiScre == null) tiScre = Instantiate(titleScreen) as Transform;
			if (Input.GetButton ("XboxFire1A") || Input.GetButton ("XboxFire1B") || Input.GetButton ("XboxFire1X") || Input.GetButton ("XboxFire1Y") || Input.GetButton ("XboxFire2A") || Input.GetButton ("XboxFire2B") || Input.GetButton ("XboxFire2X") || Input.GetButton ("XboxFire2Y") || Input.GetKey(KeyCode.S))
			{
				//We're entering character select here
				mode = 1;
				Destroy (tiScre.gameObject);
				sButton = Instantiate(startButton, new Vector3(0, -pos.y / 1.5f), Quaternion.identity) as Transform;
//				tutButton = Instantiate(tutorialButton, new Vector3(0, -pos.y / 2f), Quaternion.identity) as Transform;
				PKIcon = Instantiate(PK, new Vector3(0, pos.y * 2.65f / 4), Quaternion.identity) as Transform;
				MMIcon = Instantiate (MM, new Vector3(0, pos.y * 1.1f / 20), Quaternion.identity) as Transform;
				TKIcon = Instantiate (TK, new Vector3(0, -pos.y / 4), Quaternion.identity) as Transform;
				DTIcon = Instantiate(DT, new Vector3(0, pos.y * 1.44f / 4), Quaternion.identity) as Transform;
				charaSelect = Instantiate(charSelect, new Vector3(0, 0), Quaternion.identity) as Transform;

			}
		}
		if (mode == 1)
		{
		//THIS IS CHARACTER SELECT
		
			if (player1Select == 0) GUI.Box (new Rect(-Screen.width / 40, Screen.height / 17, PKT.width, PKT.height), PKT, style);
			if (player1Select == 1) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, MMT.width, MMT.height), MMT, style);
			if (player1Select == 2) GUI.Box (new Rect(-Screen.width / 40, Screen.height / 17, TKT.width, TKT.height), TKT, style);
			if (player1Select == 3) GUI.Box (new Rect(-Screen.width / 40, Screen.height / 17, DTT.width, DTT.height), DTT, style);
			if (player2Select == 0) GUI.Box (new Rect(Screen.width * 1.03f / 2, Screen.height / 17, PKT.width, PKT.height), PKT, style);
			if (player2Select == 1) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, MMT.width, MMT.height), MMT, style);
			if (player2Select == 2) GUI.Box (new Rect(Screen.width * 1.03f / 2, Screen.height / 17, TKT.width, TKT.height), TKT, style);
			if (player2Select == 3) GUI.Box (new Rect(Screen.width * 1.03f / 2, Screen.height / 17, DTT.width, DTT.height), DTT, style);		

		}
		if (mode == 2 && player1 != null && player2 != null)
		{
			player1 = GameObject.FindGameObjectWithTag("Player1").transform;
			player2 = GameObject.FindGameObjectWithTag("Player2").transform;
			
			//Check for death
//			if (player1.GetComponent<PlayerStats>().lives <= 0 || player2.GetComponent<PlayerStats>().lives <= 0)
//			{
//				Instantiate (gameOver);
//				mode = 3;
//			}
			
			//Player 1 HUD
			style.fontSize = 24;
//			GUI.Box (new Rect(Screen.width / 20, Screen.height * 5.5f / 8, Screen.width / 8, Screen.height / 10), player1.GetComponent<PlayerStats>().swapRole, style);
			style.fontSize = 48	;	 
			GUI.Box (new Rect(Screen.width * 1.1f / 8, Screen.height * 6.45f / 8, Screen.width / 8, Screen.height / 8), player1.GetComponent<PlayerStats>().lives.ToString(), style);
			GUI.Box (new Rect(Screen.width * 1.1f / 8, Screen.height * 7.15f / 8, Screen.width / 8, Screen.height / 8), player1.GetComponent<PlayerStats>().ammo.ToString(), style);
			GUI.Box (new Rect(Screen.width / 25, Screen.height * 2.75f / 8, Screen.width / 8, Screen.height / 8), Mathf.RoundToInt(player1.GetComponent<PlayerStats>().score).ToString(), style);
			
			//Player 2 HUD
			style.fontSize = 24;
//			GUI.Box (new Rect(Screen.width * 6.5f / 8, Screen.height * 5.5f / 8, Screen.width / 8, Screen.height / 10), player2.GetComponent<PlayerStats>().swapRole, style);
			style.fontSize = 48;
			GUI.Box (new Rect(Screen.width * 5.9f / 8, Screen.height * 6.45f / 8, Screen.width / 8, Screen.height / 8), player2.GetComponent<PlayerStats>().lives.ToString(), style);
			GUI.Box (new Rect(Screen.width * 5.9f / 8, Screen.height * 7.15f / 8, Screen.width / 8, Screen.height / 8), player2.GetComponent<PlayerStats>().ammo.ToString(), style);
			GUI.Box (new Rect(Screen.width * 82 / 100, Screen.height * 2.75f / 8, Screen.width / 8, Screen.height / 8), Mathf.RoundToInt(player2.GetComponent<PlayerStats>().score).ToString(), style);	
		}
		if(death == true){
			GUI.DrawTexture(new Rect(0, 0, 435, 326), b,ScaleMode.StretchToFill);

		}

		
	}

	// Update is called once per frame
	void Update () {
		Screen.showCursor = false;
		if(death){
			if(Input.anyKey){
				Application.LoadLevel ("Peregrin's Scene");
			}
		}
		if(start){
			if(player1.GetComponent<PlayerStats>().lives < 0 || player2.GetComponent<PlayerStats>().lives < 0){
				death = true;
			}
		}

		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
		if (mode == 1)
		{
			if (Input.GetButtonDown("XboxFire1A") && PKIcon.renderer.bounds.Contains(player1Cursor.position) && player1Select != 0) player1Select = 0;
			if (Input.GetButtonDown("XboxFire1A") && MMIcon.renderer.bounds.Contains(player1Cursor.position) && player1Select != 1) player1Select = 1;
			if (Input.GetButtonDown("XboxFire1A") && TKIcon.renderer.bounds.Contains(player1Cursor.position) && player1Select != 2) player1Select = 2;
			if (Input.GetButtonDown("XboxFire1A") && DTIcon.renderer.bounds.Contains(player1Cursor.position) && player1Select != 3) player1Select = 3;
			if ((Input.GetButtonDown("XboxFire2A") || Input.GetKey(KeyCode.KeypadEnter)) && PKIcon.renderer.bounds.Contains(player2Cursor.position) && player2Select != 0) player2Select = 0;
			if ((Input.GetButtonDown("XboxFire2A") || Input.GetKey(KeyCode.KeypadEnter)) && MMIcon.renderer.bounds.Contains(player2Cursor.position) && player2Select != 1) player2Select = 1;
			if ((Input.GetButtonDown("XboxFire2A") || Input.GetKey(KeyCode.KeypadEnter)) && TKIcon.renderer.bounds.Contains(player2Cursor.position) && player2Select != 2) player2Select = 2;
			if ((Input.GetButtonDown("XboxFire2A") || Input.GetKey(KeyCode.KeypadEnter)) && DTIcon.renderer.bounds.Contains(player2Cursor.position) && player2Select != 3) player2Select = 3;
			if (Input.GetKey (KeyCode.S))
			{
				player1Select = 0;
				player2Select = 1;
			}
			if (player1Select != -1 && player2Select != -1)
			{
				sButton.GetComponent<SpriteRenderer>().sprite = startButtonNormal;
				if (player1Cursor.renderer.bounds.Intersects(sButton.renderer.bounds))
				{
					sButton.GetComponent<SpriteRenderer>().sprite = startButtonHover;
					if (Input.GetButtonUp("XboxFire1A")) begin = true;
				}
				if (player2Cursor.renderer.bounds.Intersects(sButton.renderer.bounds))
				{
					sButton.GetComponent<SpriteRenderer>().sprite = startButtonHover;
					if (Input.GetButtonUp("XboxFire2A")) begin = true;
				}
			}
			
//			if (player1Cursor.renderer.bounds.Intersects(tutButton.renderer.bounds) || player2Cursor.renderer.bounds.Intersects(tutButton.renderer.bounds))
//			{
//				tutButton.GetComponent<SpriteRenderer>().sprite = tutButtonHover;
//				if (player1Cursor.renderer.bounds.Intersects(tutButton.renderer.bounds) && Input.GetButtonUp("XboxFire1A"))
//				{
//					if (Input.GetButtonUp("XboxFire1A"))
//					{
//						Instantiate(tutoBG, new Vector3(0, 0), Quaternion.identity);
//						mode = 3;
//						Destroy (PKIcon.gameObject);
//						//				Destroy(MMIcon.gameObject);
//						Destroy (TKIcon.gameObject);
//						Destroy (DTIcon.gameObject);
//						Destroy (player1Cursor.gameObject);
//						Destroy (player2Cursor.gameObject);
//						Destroy (charaSelect.gameObject);
//						Instantiate(tutorial);
//					}
//				}
//				else if (player2Cursor.renderer.bounds.Intersects(tutButton.renderer.bounds) && Input.GetButtonUp("XboxFire2A"))
//				{
//
//					if (Input.GetButtonUp("XboxFire2A"))
//					{
//						Instantiate(tutoBG, new Vector3(0, 0), Quaternion.identity);
//						mode = 3;
//						Destroy (PKIcon.gameObject);
//						Destroy(MMIcon.gameObject);
//						Destroy (TKIcon.gameObject);
//						Destroy (DTIcon.gameObject);
//						Destroy (player1Cursor.gameObject);
//						Destroy (player2Cursor.gameObject);
//						Destroy (charaSelect.gameObject);
//						Instantiate(tutorial);
//					}
//				}
//			}
//			else tutButton.GetComponent<SpriteRenderer>().sprite = tutButtonNormal;
			
			if (player1Select != -1 && player2Select != -1 && begin == true || Input.GetKey(KeyCode.D))
			{
				sound.GetComponent<SoundManager>().StopSound("CharSelect");
				sound.GetComponent<SoundManager>().PlaySound("Theme1");
				sound.GetComponent<SoundManager>().LoopSound("Theme1");
				level1BG = (Transform)Instantiate (Background);
				level1BG.transform.localScale = new Vector3(1.6f, 1.6f);
				
				if (player1Select == 0)
				{
					player1 = Instantiate(PunchKnight) as Transform;
					Instantiate (PKP1);
					
				}
				if (player1Select == 1)
				{
					player1 = Instantiate(MirrorMage) as Transform;
					Instantiate(MMP1);
				}
				if (player1Select == 2)
				{
					player1 = Instantiate(Tinker) as Transform;
					Instantiate(TKP1);
				}
				if (player1Select == 3)
				{
					player1 = Instantiate(DragonTamer) as Transform;
					Instantiate (DTP1);
				}
				if (player2Select == 0)
				{
					player2 = Instantiate(PunchKnight) as Transform;
					Instantiate(PKP2);
				}
				if (player2Select == 1)
				{
					player2 = Instantiate(MirrorMage) as Transform;
					Instantiate(MMP2);
				}
				if (player2Select == 2)
				{
					player2 = Instantiate(Tinker) as Transform;
					Instantiate(TKP2);
				}
				if (player2Select == 3)
				{
					player2 = Instantiate(DragonTamer) as Transform;
					Instantiate(DTP2);
				}
				player1.transform.tag = "Player1";
				player2.transform.tag = "Player2";
				Destroy (PKIcon.gameObject);
				Destroy(MMIcon.gameObject);
				Destroy (TKIcon.gameObject);
				Destroy (DTIcon.gameObject);
				Destroy (player1Cursor.gameObject);
				Destroy (player2Cursor.gameObject);
				Destroy (charaSelect.gameObject);
				Destroy (sButton.gameObject);
				Destroy(tutButton.gameObject);
				start = true;
				mode = 2;
				boxy.GetComponent<spawn>().starter();
			}
			//			int playerOne;
			//			int playerTwo;
		}
		if (mode < 2)
		{
			player1Cursor.position += new Vector3(Input.GetAxis ("XboxHorizontal1")*Time.deltaTime*10, Input.GetAxis ("XboxVertical1")*Time.deltaTime*10, 0);
			player2Cursor.position += new Vector3 (Input.GetAxis ("XboxHorizontal2")*Time.deltaTime*10, Input.GetAxis ("XboxVertical2")*Time.deltaTime*10, 0);
			if (Input.GetKey(KeyCode.UpArrow)) player2Cursor.transform.Translate(Vector3.up*Time.deltaTime*10);
			if (Input.GetKey(KeyCode.LeftArrow)) player2Cursor.transform.Translate(Vector3.left*Time.deltaTime*10);
			if (Input.GetKey(KeyCode.DownArrow)) player2Cursor.transform.Translate(Vector3.down*Time.deltaTime*10);
			if (Input.GetKey(KeyCode.RightArrow)) player2Cursor.transform.Translate(Vector3.right*Time.deltaTime*10);
		}
	}
}
