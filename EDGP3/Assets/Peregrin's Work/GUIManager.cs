using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	//Transforms to load into the game as the characters
	public Transform PunchKnight;
	public Transform MirrorMage;
	public Transform Tinker;

	//Icons for team select
	public Transform PK;
	public Transform MM;
	public Transform TK;

	//Cursors for players 1 & 2 in team select
	public Transform target1;
	public Transform target2;

	//GUI style for in-game HUD
	public GUIStyle style;
	
	public Texture PKT;
	public Texture TKT;
	public Texture MMT;
	
	//Modes 0, 1, 2, 3 = Title Screen, Character Selection, Game, Death
	private int mode = 0;

	Transform player1;
	Transform player2;
	Transform player1Cursor;
	Transform player2Cursor;
	Transform PKIcon;
	Transform MMIcon;
	Transform TKIcon;

	Vector3 pos;
	Vector3 mPos;
	Vector3 cPos;
	
	int player1Select = -1;
	int player2Select = -1;
	
	// Use this for initialization
	void Start ()
	{
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
			if (Event.current.type == EventType.KeyDown)
			{
				
				mode = 1;
				PKIcon = Instantiate(PK, new Vector3(-pos.x / 2, pos.y / 4), Quaternion.identity) as Transform;
				MMIcon = Instantiate (MM, new Vector3(pos.x / 2, pos.y / 4), Quaternion.identity) as Transform;
				TKIcon = Instantiate (TK, new Vector3(-pos.x / 2, -pos.y / 4), Quaternion.identity) as Transform;
			}
		}
		if (mode == 1)
		{
			GUI.Box (new Rect(Screen.width / 3, Screen.height / 8, Screen.width / 3, Screen.height / 8), "Pick your character!", style);
			if (player1Select == 0) GUI.Box (new Rect(Screen.width / 5, Screen.height * 4 / 5, Screen.width / 5, Screen.width / 5), PKT);
			if (player1Select == 1) GUI.Box (new Rect(Screen.width / 5, Screen.height * 4 / 5, Screen.width / 5, Screen.width / 5), MMT);
			if (player1Select == 2) GUI.Box (new Rect(Screen.width / 5, Screen.height * 4 / 5, Screen.width / 5, Screen.width / 5), TKT);
			if (player2Select == 0) GUI.Box (new Rect(Screen.width * 3 / 5, Screen.height * 4 / 5, Screen.width / 5, Screen.width / 5), PKT);
			if (player2Select == 1) GUI.Box (new Rect(Screen.width * 3 / 5, Screen.height * 4 / 5, Screen.width / 5, Screen.width / 5), MMT);
			if (player2Select == 2) GUI.Box (new Rect(Screen.width * 3 / 5, Screen.height * 4 / 5, Screen.width / 5, Screen.width / 5), TKT);
		}
		if (mode == 2 && player1 != null)
		{
			player1 = GameObject.FindGameObjectWithTag("Player1").transform;
			player2 = GameObject.FindGameObjectWithTag("Player2").transform;
			GUI.Box (new Rect (0, 0, Screen.width / 4, Screen.height), 
			         "PLAYER 1: \n\n\nLives: \n\n\n\n"+
					 "Ammo:\n\n" + 
					 player1.GetComponent<PlayerStats>().ammo.ToString()+"\n\n\n\n", style);

			GUI.Box (new Rect (Screen.width * 3 / 4, 0, Screen.width / 4, Screen.height),  
			         "PLAYER 2: \n\n\nLives: \n\n\n\n"+
			         "Ammo: \n\n" + 
			         player2.GetComponent<PlayerStats>().ammo.ToString(), style);
		}
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
		if (mode == 1)
		{
			if (Input.GetMouseButtonDown(0) && PKIcon.renderer.bounds.Contains(mPos) && player1Select != 0) player1Select = 0;
			if (Input.GetMouseButtonDown(0) && MMIcon.renderer.bounds.Contains(mPos) && player1Select != 1) player1Select = 1;
			if (Input.GetMouseButtonDown(0) && TKIcon.renderer.bounds.Contains(mPos) && player1Select != 2) player1Select = 2;
			if (Input.GetButtonDown("Fire1") && PKIcon.renderer.bounds.Contains(cPos) && player2Select != 0) player2Select = 0;
			if (Input.GetButtonDown("Fire1") && MMIcon.renderer.bounds.Contains(cPos) && player2Select != 1) player2Select = 1;
			if (Input.GetButtonDown("Fire1") && TKIcon.renderer.bounds.Contains(cPos) && player2Select != 2) player2Select = 2;
			if (Input.GetKeyDown(KeyCode.RightShift)) player2Select = 0;

			if (player1Select != -1 && player2Select != -1)
			{
				if (player1Select == 0) player1 = Instantiate(PunchKnight) as Transform;
				if (player1Select == 1) player1 = Instantiate(MirrorMage) as Transform;
				if (player1Select == 2) player1 = Instantiate(Tinker) as Transform;
				if (player2Select == 0) player2 = Instantiate(PunchKnight) as Transform;
				if (player2Select == 1) player2 = Instantiate(MirrorMage) as Transform;
				if (player2Select == 2) player2 = Instantiate(Tinker) as Transform;
				player1.transform.tag = "Player1";
				player2.transform.tag = "Player2";
				Destroy (PKIcon.gameObject);
				Destroy(MMIcon.gameObject);
				Destroy (TKIcon.gameObject);
				Destroy (player1Cursor.gameObject);
				Destroy (player2Cursor.gameObject);
				mode = 2;
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
			player2Cursor.position += new Vector3 (Input.GetAxis ("Horizontal")*Time.deltaTime*10, Input.GetAxis ("Vertical")*Time.deltaTime*10, 0);
		}
	}
}
