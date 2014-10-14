using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	//Transforms to load into the game as the characters
	public Transform PunchKnight;
	public Transform MirrorMage;
	public Transform Tinker;
	public Transform DragonTamer;

	//Icons for team select
	public Transform PK;
	public Transform MM;
	public Transform TK;
	public Transform DT;

	//Cursors for players 1 & 2 in team select
	public Transform target1;
	public Transform target2;

	//GUI style for in-game HUD
	public GUIStyle player1Style;
	public GUIStyle player2Style;
	public GUIStyle style;
	
	public Texture PKT;
	public Texture TKT;
	public Texture MMT;
	public Texture DTT;
	
	//Modes 0, 1, 2, 3 = Title Screen, Character Selection, Game, Death
	private int mode = 0;

	Transform player1;
	Transform player2;
	Transform player1Cursor;
	Transform player2Cursor;
	Transform PKIcon;
	Transform MMIcon;
	Transform TKIcon;
	Transform DTIcon;

	Vector3 pos;
	Vector3 mPos;
	Vector3 cPos;
	
	int player1Select = -1;
	int player2Select = -1;
	bool begin = false;
	
	GameObject boxy;
	// Use this for initialization
	void Start ()
	{
		boxy = GameObject.FindGameObjectWithTag("Manager");
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
				PKIcon = Instantiate(PK, new Vector3(0, pos.y / 5), Quaternion.identity) as Transform;
				MMIcon = Instantiate (MM, new Vector3(0, pos.y * 2 / 5), Quaternion.identity) as Transform;
				TKIcon = Instantiate (TK, new Vector3(0, 0), Quaternion.identity) as Transform;
				DTIcon = Instantiate(DT, new Vector3(0, -pos.y / 5), Quaternion.identity) as Transform;
			}
		}
		if (mode == 1)
		{
			if (player1Select != -1 && player2Select != -1)
			{
				if (GUI.Button (new Rect(Screen.width / 2, Screen.height * 3 / 4, Screen.width / 4, Screen.height / 8), "Begin")) begin = true;
			}
			GUI.Box (new Rect(Screen.width / 3, Screen.height / 15, Screen.width / 3, Screen.height / 8), "Pick your character!", style);
			if (player1Select == 0) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, PKT.width, PKT.height), PKT);
			if (player1Select == 1) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, MMT.width, MMT.height), MMT);
			if (player1Select == 2) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, TKT.width, TKT.height), TKT);
			if (player1Select == 3) GUI.Box (new Rect(Screen.width / 8, Screen.height / 4, DTT.width, DTT.height), DTT);
			if (player2Select == 0) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, PKT.width, PKT.height), PKT);
			if (player2Select == 1) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, MMT.width, MMT.height), MMT);
			if (player2Select == 2) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, TKT.width, TKT.height), TKT);
			if (player2Select == 3) GUI.Box (new Rect(Screen.width * 3 / 4, Screen.height / 4, DTT.width, DTT.height), DTT);
		}
		if (mode == 2 && player1 != null && player2 != null)
		{
			player1 = GameObject.FindGameObjectWithTag("Player1").transform;
			player2 = GameObject.FindGameObjectWithTag("Player2").transform;
			string player1Name = player1.name.Remove (player1.name.IndexOf("(Clone"), player1.name.Length - player1.name.IndexOf("(Clone"));
			string player2Name = player2.name.Remove (player2.name.IndexOf("(Clone"), player2.name.Length - player2.name.IndexOf("(Clone"));
			GUI.Box (new Rect (0, 0, Screen.width / 4, Screen.height), 
			         player1Name+"\n\n\nScore:\n   123454\n\n\n\n"+
					 "Level 1\nnIncreased Damage\n\nLevel 2\n200 Ammo cap\n\nLevel 3\nIncreased Damage\n\n\n"+ 
					 player1.GetComponent<PlayerStats>().ammo.ToString()+"\n\n\n\n"+
					 "Swap: "+player1.GetComponent<PlayerStats>().swapRole, player1Style);					 

			GUI.Box (new Rect (Screen.width * 3 / 4, 0, Screen.width / 4, Screen.height),  
			         player2Name+"\n\n\nScore:\n   123454\n\n\n\n"+
			         "Level 1\nnIncreased Damage\n\nLevel 2\n200 Ammo cap\n\nLevel 3\nIncreased Damage\n\n\n"+ 
			         player2.GetComponent<PlayerStats>().ammo.ToString()+"\n\n\n\n"+
			         "Swap: "+player2.GetComponent<PlayerStats>().swapRole, player2Style);	
			         
			
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
			if (Input.GetMouseButtonDown(0) && DTIcon.renderer.bounds.Contains(mPos) && player1Select != 3) player1Select = 3;
			if (Input.GetButtonDown("XboxFire1") && PKIcon.renderer.bounds.Contains(cPos) && player2Select != 0) player2Select = 0;
			if (Input.GetButtonDown("XboxFire1") && MMIcon.renderer.bounds.Contains(cPos) && player2Select != 1) player2Select = 1;
			if (Input.GetButtonDown("XboxFire1") && TKIcon.renderer.bounds.Contains(cPos) && player2Select != 2) player2Select = 2;
			if (Input.GetButtonDown("XboxFire1") && DTIcon.renderer.bounds.Contains(cPos) && player2Select != 3) player2Select = 3;
			if (Input.GetKey (KeyCode.Tab)) player2Select = 0;
			if (Input.GetKeyDown(KeyCode.RightShift)) player2Select = 0;

			if (player1Select != -1 && player2Select != -1 && begin == true)
			{
				if (player1Select == 0) player1 = Instantiate(PunchKnight) as Transform;
				if (player1Select == 1) player1 = Instantiate(MirrorMage) as Transform;
				if (player1Select == 2) player1 = Instantiate(Tinker) as Transform;
				if (player1Select == 3) player1 = Instantiate(DragonTamer) as Transform;
				if (player2Select == 0) player2 = Instantiate(PunchKnight) as Transform;
				if (player2Select == 1) player2 = Instantiate(MirrorMage) as Transform;
				if (player2Select == 2) player2 = Instantiate(Tinker) as Transform;
				if (player2Select == 3) player2 = Instantiate(DragonTamer) as Transform;
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
