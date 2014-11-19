using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public Transform TutKnight;
	
	public GUIStyle style1;
	
	public Transform tutorialBG;
	
	public Sprite[] images;
	public Transform image;
	Transform theImage;
	
	public Transform target;
	Transform ourTarget;
	
	int tutMode = 0;
	
	float timer;
	bool timerReset = false;
	float timer2;
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
		timer2 = 0;
		theImage = Instantiate(image) as Transform;
		theImage.renderer.sortingOrder = 1;
		Instantiate(tutorialBG, new Vector3(0, 0), Quaternion.identity);
		timer = Time.time;
		pos1 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 8, Screen.height / 2));
		pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 7 / 8, Screen.height / 2));
		pos1.z = 0; pos2.z = 0;
	
	}
	
	void OnGUI() {
		style1.fontSize = 28;
	
		if (tutMode == 0)
		{
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (Time.time - timer > 1.0f) tutMode = 1;
			
		}
		if (tutMode == 1)
		{
			if (timerReset == false)
			{
				timer = Time.time;
				timerReset = true;
			}
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (haveInstantiated == false)
			{
				p1 = (Transform)Instantiate(TutKnight, pos1, Quaternion.identity); p2 = (Transform)Instantiate(TutKnight, pos2, Quaternion.identity);
				p1.GetComponent<PlayerTut>().whichPlayer = 1; p2.GetComponent<PlayerTut>().whichPlayer = 2;
				p1.renderer.sortingOrder = 3; p2.renderer.sortingOrder = 3;
				haveInstantiated = true;
			}
			if (Time.time - timer > 1.5f)
			{
				tutMode = 2;
				timerReset = false;
				p1.GetComponent<PlayerTut>().move = true;
				p2.GetComponent<PlayerTut>().move = true;
				ourTarget = Instantiate(target) as Transform;
				ourTarget.renderer.sortingOrder = 2;
			}
			
		}
		if (tutMode == 2)
		{
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (Input.GetAxis("XboxHorizontal1") != 0 || Input.GetAxis ("XboxVertical1") != 0) p1move = true;
			if (Input.GetAxis("XboxHorizontal2") != 0 || Input.GetAxis ("XboxVertical2") != 0) p2move = true;
			if (ourTarget.renderer.bounds.Contains(p1.position) && ourTarget.renderer.bounds.Contains(p2.position) && timerReset == false)
			{
				timer = Time.time;
				timerReset = true;
			}
			if (Time.time - timer > .5f && timerReset == true)
			{
				tutMode = 3;
				timerReset = false;
				p1.GetComponent<PlayerTut>().shoot = true;
				p2.GetComponent<PlayerTut>().affinity = true;
				Destroy (ourTarget.gameObject);
			}
		}
		if (tutMode == 3)
		{
			if (timerReset == false)
			{
				timer = Time.time;
				timerReset = true;
			}
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (Time.time - timer > 1.5f && timerReset == true)
			{
				timerReset = false;
				tutMode = 4;
			}	
		}
		if (tutMode == 4)
		{
			if (timerReset == false)
			{
				timer = Time.time;
				timerReset = true;
			}
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (Time.time - timer > 1.5f && timerReset == true)
			{
				timerReset = false;
				tutMode = 5;
			}	
		}
		if (tutMode == 5)
		{
			if (timerReset == false)
			{
				timer = Time.time;
				timerReset = true;
			}
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (Time.time - timer > 1.5f && timerReset == true)
			{
				timerReset = false;
				tutMode = 6;
			}	
		}
		if (tutMode == 6)
		{
			if (timerReset == false)
			{
				timer = Time.time;
				timerReset = true;
			}
			theImage.GetComponent<SpriteRenderer>().sprite = images[tutMode];
			if (Time.time - timer > 1.5f && timerReset == true)
			{
				timerReset = false;
				tutMode = 7;
			}	
		}
		if (tutMode == 7)
		{
			if (p1.GetComponent<PlayerStats>().hasSwapped)
			{
				GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1:\nNice swap!", style1);
				GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2:\nNice swap!", style1);
				timer2 = Time.time;
			}	
		}
		if (tutMode == 8)
		{
			p2shoot = false; p1shoot = false;
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Time to shield", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Time to shoot", style1);
		}
		if (tutMode == 9)
		{
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1:\nSpace bar to swap shield affinity", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2:\n A button to shoot", style1);
			if (Input.GetKey(KeyCode.Space)) p1shoot = true; if (Input.GetButton("XboxFire1")) p2shoot = true;
		}
		if (tutMode == 10)
		{
			p1.GetComponent<PlayerStats>().tutSpecial = false; p2.GetComponent<PlayerStats>().tutSpecial = false;
			GUI.Box (new Rect(Screen.width / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 1:\nX button to use special while in attack", style1);
			GUI.Box (new Rect(Screen.width * 6 / 10, Screen.height / 10, Screen.width * 3 / 10, Screen.height / 10), "Player 2:\nX button to use special", style1);
			if (Input.GetKey(KeyCode.X)) p1special = true; if (Input.GetButton("XboxFire3")) p2special = true;	
		}
		if (tutMode == 11)
		{
			GameObject.Find ("Main Camera").GetComponent<GUIManager>().mode = 0;
			GameObject.Find ("Main Camera").GetComponent<GUIManager>().ReturnFromTutorial();
			Destroy (gameObject);
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
