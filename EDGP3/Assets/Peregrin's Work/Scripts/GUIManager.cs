using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	//Transforms to load into the game as the characters
	public Transform PunchKnight;
	public Transform MirrorMage;
	public Transform Tinker;
	public Transform DragonTamer;
	
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
	private int mode = 0;
	
	//Tutorial script holding thingy
	public Transform tutorial;
	
	//HUD
	//Dragon
	public Transform DTP1;
	public Transform DTP2;
	public Transform gameOver;
	
	//Punch
	public Transform PKP1;
	public Transform PKP2;
	//Mirror
    //public Transform MMP1;
    //public Transform MMP2;
    
	//Tinker
    //public Transform TKP1;
	//public Transform TKP2;

	//Icons for team select
	public Transform PK;
	public Transform MM;
	public Transform TK;
	public Transform DT;
	
	//Textures for boxes in team select
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

	//Holds the current player value, if both are filled then the game can begin
	Transform player1;
	Transform player2;
	
	//Cursors
	Transform player1Cursor;
	Transform player2Cursor;
	
	//Used to check where the cursors are when they are activated
	Transform PKIcon;
	Transform MMIcon;
	Transform TKIcon;
	Transform DTIcon;
	
	//Background


	//Cursor position stuff
	Vector3 pos;
	Vector3 mPos;
	Vector3 cPos;
	
	int player1Select = -1;
	int player2Select = -1;
	bool begin = false;
	
	GameObject sound;
	GameObject boxy;
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
		mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		cPos = Camera.main.ScreenToWorldPoint (player2Cursor.renderer.bounds.center);
		mPos.z = 0;
		cPos.z = 0;
		
		
	}
	
	void OnGUI()
	{
		if (mode == 0)
		{
			GUI.Box (new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 3), "Press any key to start...", style);
			if (Event.current.type == EventType.KeyDown || Input.GetButton("XboxFire1") || Input.GetButton("XboxFire2") || Input.GetButton("XboxFire3") || Input.GetButton("XboxFire4"))
			{
				mode = 1;
				PKIcon = Instantiate(PK, new Vector3(0, pos.y / 5), Quaternion.identity) as Transform;
				MMIcon = Instantiate (MM, new Vector3(0, -pos.y * 2 / 5), Quaternion.identity) as Transform;
				TKIcon = Instantiate (TK, new Vector3(0, -pos.y / 5), Quaternion.identity) as Transform;
				DTIcon = Instantiate(DT, new Vector3(0, 0), Quaternion.identity) as Transform;
				
			}
		}
		if (mode == 1)
		{
			style.fontSize = 48;
			
			if (player1Select != -1 && player2Select != -1)
			{

				if (GUI.Button (new Rect(Screen.width * 2 / 5, Screen.height * 3 / 4, Screen.width / 5, Screen.height / 8), "Begin", style))
				{
					begin = true;
					style.fontSize = 0;
				}
			}
			GUI.Box (new Rect(Screen.width / 3, Screen.height / 15, Screen.width / 3, Screen.height / 8), "Pick your character!", style);
			if (player1Select == 0) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, PKT.width, PKT.height), PKT, style);
			if (player1Select == 1) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, MMT.width, MMT.height), MMT, style);
			if (player1Select == 2) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, TKT.width, TKT.height), TKT, style);
			if (player1Select == 3) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, DTT.width, DTT.height), DTT, style);
			if (player2Select == 0) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, PKT.width, PKT.height), PKT, style);
			if (player2Select == 1) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, MMT.width, MMT.height), MMT, style);
			if (player2Select == 2) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, TKT.width, TKT.height), TKT, style);
			if (player2Select == 3) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, DTT.width, DTT.height), DTT, style);
			
			if (GUI.Button (new Rect(Screen.width / 2.35f, Screen.height / 5, Screen.width / 7, Screen.height / 10), "Tutorial"))
			{
				mode = 3;
				Destroy (PKIcon.gameObject);
				Destroy(MMIcon.gameObject);
				Destroy (TKIcon.gameObject);
				Destroy (DTIcon.gameObject);
				Destroy (player1Cursor.gameObject);
				Destroy (player2Cursor.gameObject);
				Instantiate(tutorial);
			}
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
			GUI.Box (new Rect(Screen.width / 20, Screen.height * 5.5f / 8, Screen.width / 8, Screen.height / 10), player1.GetComponent<PlayerStats>().swapRole, style);
			style.fontSize = 48	;	 
			GUI.Box (new Rect(Screen.width * 1.1f / 8, Screen.height * 6.45f / 8, Screen.width / 8, Screen.height / 8), player1.GetComponent<PlayerStats>().lives.ToString(), style);
			GUI.Box (new Rect(Screen.width * 1.1f / 8, Screen.height * 7.15f / 8, Screen.width / 8, Screen.height / 8), player1.GetComponent<PlayerStats>().ammo.ToString(), style);
			GUI.Box (new Rect(Screen.width / 25, Screen.height * 2.75f / 8, Screen.width / 8, Screen.height / 8), "123,456", style);
			
			//Player 2 HUD
			style.fontSize = 24;
			GUI.Box (new Rect(Screen.width * 6.5f / 8, Screen.height * 5.5f / 8, Screen.width / 8, Screen.height / 10), player2.GetComponent<PlayerStats>().swapRole, style);
			style.fontSize = 48;
			GUI.Box (new Rect(Screen.width * 5.9f / 8, Screen.height * 6.45f / 8, Screen.width / 8, Screen.height / 8), player2.GetComponent<PlayerStats>().lives.ToString(), style);
			GUI.Box (new Rect(Screen.width * 5.9f / 8, Screen.height * 7.15f / 8, Screen.width / 8, Screen.height / 8), player2.GetComponent<PlayerStats>().ammo.ToString(), style);
			GUI.Box (new Rect(Screen.width * 82 / 100, Screen.height * 2.75f / 8, Screen.width / 8, Screen.height / 8), "789, 012", style);	
		}
		
	}

	// Update is called once per frame
	void Update () {
	
		if (Time.time - GameObject.Find ("Manager").GetComponent<spawn>().time >= 30f) gameMode = 1;
	
		Screen.showCursor = false;
		
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
		if (mode == 1)
		{
			if (Input.GetMouseButtonDown(0) && PKIcon.renderer.bounds.Contains(mPos) && player1Select != 0) player1Select = 0;
			if (Input.GetMouseButtonDown(0) && MMIcon.renderer.bounds.Contains(mPos) && player1Select != 1) player1Select = 1;
			if (Input.GetMouseButtonDown(0) && TKIcon.renderer.bounds.Contains(mPos) && player1Select != 2) player1Select = 2;
			if (Input.GetMouseButtonDown(0) && DTIcon.renderer.bounds.Contains(mPos) && player1Select != 3) player1Select = 3;
			if ((Input.GetButtonDown("XboxFire1") || Input.GetKey(KeyCode.KeypadEnter)) && PKIcon.renderer.bounds.Contains(cPos) && player2Select != 0) player2Select = 0;
			if ((Input.GetButtonDown("XboxFire1") || Input.GetKey(KeyCode.KeypadEnter)) && MMIcon.renderer.bounds.Contains(cPos) && player2Select != 1) player2Select = 1;
			if ((Input.GetButtonDown("XboxFire1") || Input.GetKey(KeyCode.KeypadEnter)) && TKIcon.renderer.bounds.Contains(cPos) && player2Select != 2) player2Select = 2;
			if ((Input.GetButtonDown("XboxFire1") || Input.GetKey(KeyCode.KeypadEnter)) && DTIcon.renderer.bounds.Contains(cPos) && player2Select != 3) player2Select = 3;
			if (Input.GetKey (KeyCode.Tab)) player2Select = 0;
			if (Input.GetKeyDown(KeyCode.RightShift)) player2Select = 3;

			if (player1Select != -1 && player2Select != -1 && begin == true)
			{
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
				}
				if (player1Select == 2)
				{
					player1 = Instantiate(Tinker) as Transform;
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
				}
				if (player2Select == 2)
				{
					player2 = Instantiate(Tinker) as Transform;
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
				mode = 2;
				boxy.GetComponent<spawn>().starter();
			}
			//			int playerOne;
			//			int playerTwo;
		}
		if (mode < 2)
		{
			pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
			mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			cPos = player2Cursor.transform.position;
			mPos.z = 0;
			cPos.z = 0;
			mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mPos.z = 0;
			player1Cursor.position = mPos;
			player2Cursor.position += new Vector3 (Input.GetAxis ("XboxHorizontal")*Time.deltaTime*10, Input.GetAxis ("XboxVertical")*Time.deltaTime*10, 0);
			if (Input.GetKey(KeyCode.UpArrow)) player2Cursor.transform.Translate(Vector3.up*Time.deltaTime*10);
			if (Input.GetKey(KeyCode.LeftArrow)) player2Cursor.transform.Translate(Vector3.left*Time.deltaTime*10);
			if (Input.GetKey(KeyCode.DownArrow)) player2Cursor.transform.Translate(Vector3.down*Time.deltaTime*10);
			if (Input.GetKey(KeyCode.RightArrow)) player2Cursor.transform.Translate(Vector3.right*Time.deltaTime*10);
		}
	}
}
