using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Transform PunchKnight;
	
	public GUIStyle style1;
	
	int tutMode = 0;
	
	float timer;
	Vector3 pos1;
	Vector3 pos2;
	
	bool haveInstantiated = false;
	Transform p1;
	Transform p2;
	
	bool p1shoot= false;
	bool p2shoot = false;
	
	bool p1move = false;
	bool p2move = false;
	
	bool p1special = false;
	bool p2special = false;

	// Use this for initialization
	void Start () {
	
		timer = Time.time;
		pos1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 3, Screen.height / 2));
		pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2 / 3, Screen.height / 2));
		pos1.z = 0; pos2.z = 0;
	
	}
	
	void OnGUI() {
		style1.fontSize = 28;
	
		if (Time.time - timer < 4f && tutMode < 1)
		{
			GUI.Box (new Rect(0,0, Screen.width, Screen.height), "\n\nWelcome to Gold and Glory\n\n\nThe Cooperative\nBullet Hell", style1);
		}
		else if (Time.time - timer < 10f && tutMode < 1)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1 is over here on the keyboard", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2 is on this side on the Xbox controller", style1);
			if (haveInstantiated == false)
			{
				p1 = (Transform)Instantiate(PunchKnight, pos1, Quaternion.identity); p2 = (Transform)Instantiate(PunchKnight, pos2, Quaternion.identity);
				p1.GetComponent<PlayerStats>().tutSpecial = true; p2.GetComponent<PlayerStats>().tutSpecial = true;
				p1.GetComponent<PlayerStats>().tutSwap = true; p2.GetComponent<PlayerStats>().tutSwap = true;
				haveInstantiated = true;
			}
		}
		else if (Time.time - timer > 10f && (!p1move || !p2move))
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1:\nMove with WASD", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2:\nMove with left stick", style1);
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A)) p1move = true;
			if (Input.GetAxis("XboxHorizontal") != 0 || Input.GetAxis("XboxVertical") != 0) p2move = true;
			if (p1move && p2move) tutMode += 1;
		}
		if (p1move && p2move && tutMode < 2)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1:\nShoot with space", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2:\nSwap shield affinity with A", style1);
			if (Input.GetKey(KeyCode.Space)) p1shoot = true;
			if (Input.GetButton("XboxFire1") || Input.GetKey(KeyCode.P)) p2shoot = true;
			timer = Time.time;
			if (p1shoot && p2shoot) tutMode += 1;
			
		}
		if (p1shoot && p2shoot && Time.time - timer < 4f && tutMode < 3)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "As you can guess, ammo isn't infinite", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "And Player 2 wants to shoot sometimes as well", style1);
		}
		if (p1shoot && p2shoot && Time.time - timer >= 4f && Time.time - timer < 8f)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "To engage in a swap, both players must hit the swap button within a limited time of each other.", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "One player creates the swap request, the other player accepts it, and the players swap.", style1);
		}
		if (p1shoot && p2shoot && Time.time - timer > 8f)
		{
			p1.GetComponent<PlayerStats>().tutSwap = false; p2.GetComponent<PlayerStats>().tutSwap = false;
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1:\nHit the X key.", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2:\nHity the B button", style1);
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
