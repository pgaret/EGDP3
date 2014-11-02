using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Transform PunchKnight;
	
	float timer;
	Vector3 pos1;
	Vector3 pos2;
	
	bool haveInstantiated = false;
	Transform p1;
	Transform p2;
	
	bool p1shoot= false;
	bool p2shoot = false;
	
	bool pimove = false;
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
	
		if (Time.time - timer < 2f) GUI.Box (new Rect(Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.height / 10), "Welcome to Gold and Glory");
		else if (Time.time - timer < 4f) GUI.Box (new Rect(Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.height / 10), "On the left we have player 1, on the right player 2");
		else if (Time.time - timer < 6f)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1");
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2");
			if (haveInstantiated == false)
			{
				p1 = (Transform)Instantiate(PunchKnight, pos1, Quaternion.identity); p2 = (Transform)Instantiate(PunchKnight, pos2, Quaternion.identity);
				p1.GetComponent<PlayerStats>().tutSpecial = true; p2.GetComponent<PlayerStats>().tutSpecial = true;
				
				haveInstantiated = true;
			}
		}
		else if (Time.time - timer > 6f && (!p1shoot || !p2shoot))
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Shoot with space");
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Swap shield affinity with A");
			if (Input.GetKey(KeyCode.Space)) p1shoot = true;
			if (Input.GetButton("XboxFire1")) p2shoot = true;
		}
		if (p1shoot && p2shoot)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Move with WASD");
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Move with left stick");
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
