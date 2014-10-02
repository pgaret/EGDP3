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

	
	//Modes 0, 1, 2, 3 = Title Screen, Character Selection, Game, Death
	private int mode = 0;

	Transform player1;
	Transform player2;
	Transform player1Cursor;
	Transform player2Cursor;
	
	// Use this for initialization
	void Start ()
	{
		Screen.SetResolution (600, 800, false);
		Screen.showCursor = false;
		player1Cursor = Instantiate (target1) as Transform;
		player2Cursor = Instantiate (target2) as Transform;
		player1Cursor.renderer.sortingOrder = 1;
		player2Cursor.renderer.sortingOrder = 1;
	}
	
	void OnGUI()
	{
		if (mode == 0)
		{
			if (Event.current.type == EventType.KeyDown)
			{
				Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
				mode = 1;
				Instantiate(PK, new Vector3(-pos.x / 2, pos.y / 4), Quaternion.identity);
				Instantiate (MM, new Vector3(pos.x / 2, pos.y / 4), Quaternion.identity);
				Instantiate (TK, new Vector3(-pos.x / 2, -pos.y / 4), Quaternion.identity);
			}
		}
		if (mode == 1)
		{
			int playerOne;
			int playerTwo;
		}
		if (mode == 2)
		{
			GUI.Box (new Rect (Screen.width * 5 / 6, 0, Screen.width / 6, Screen.height), 
					 "Ammo1:\n\n" + 
					 player1.GetComponent<Ship1>().ammo.ToString()+"\n\n\n\n" + 
					 "Lives: \n\n\n\n"+
					 "Ammo2: \n\n" + 
					 player2.GetComponent<Ship1>().ammo.ToString(), style);
		}
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		pos.z = 0;
		player1Cursor.position = pos;
		player2Cursor.position += new Vector3 (Input.GetAxis ("Horizontal")*Time.deltaTime*10, Input.GetAxis ("Vertical")*Time.deltaTime*10, 0);
	}
}
