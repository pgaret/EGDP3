using UnityEngine;
using System.Collections;

public class PlayerTut : MonoBehaviour {

	public int whichPlayer;
	public bool move = false;
	public bool shoot = false;
	public bool affinity = false;
	public bool swap = false;
	public bool special = false;
	
	public float speed;
	
	GameObject top;
	GameObject left;
	GameObject right;
	GameObject down;
	
	Vector3 size;
	GameObject avoid;

	// Use this for initialization
	void Start ()
	{
		size = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
		Debug.Log (size.x +" "+size.y);
		avoid = GameObject.FindGameObjectWithTag("Background");
		
		top = GameObject.Find ("Up");
		down = GameObject.Find ("Down");
		left = GameObject.Find("Left");
		right = GameObject.Find ("Right");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (whichPlayer == 1 && move == true)
		{
			if (Input.GetAxis("XboxVertical1") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal1") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxVertical1") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal1") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
		}
		if (whichPlayer == 2 && move == true)
		{
			if (Input.GetAxis("XboxVertical2") > 0 && transform.position.y < top.transform.position.y) transform.Translate(Vector3.up*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal2") < 0 && transform.position.x > left.transform.position.x) transform.Translate(Vector3.left*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxVertical2") < 0 && transform.position.y > down.transform.position.y) transform.Translate(Vector3.down*Time.deltaTime*speed);
			if (Input.GetAxis ("XboxHorizontal2") > 0 && transform.position.x < right.transform.position.x) transform.Translate(Vector3.right*Time.deltaTime*speed);
		}
	}
}
