using UnityEngine;
using System.Collections;

public class MirrorMage : MonoBehaviour {
	
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	
	public float width = .1f;
	public Color color = Color.white;
	
	GameObject sound;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	LineRenderer leftSide;
	LineRenderer rightSide;
	
	LineRenderer topLeft;
	LineRenderer topRight;
	
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Sound");
		if (transform.tag == "Player2") Shield ();
		//From player to mirrors lenses
		leftSide = GameObject.Find ("Left").AddComponent<LineRenderer>();
		rightSide = GameObject.Find ("Right").AddComponent<LineRenderer>();
		//From mirrors to top lenses
		topLeft = GameObject.Find ("MML").AddComponent<LineRenderer>();
		topRight = GameObject.Find ("MMR").AddComponent<LineRenderer>();
		//Narrow the beams
		topLeft.SetWidth(width, width); topLeft.SetColors(color, color);
		topRight.SetWidth(width, width); topRight.SetColors(color, color);
		leftSide.SetWidth(width, width); leftSide.SetColors(color, color);
		rightSide.SetWidth(width, width); rightSide.SetColors(color, color);
		
	}
	
	void Special()
	{

	}
	
	public void Shield()
	{

	}
	
	void Shoot()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Beam follows the player
		Vector3 pos = transform.position;
		pos.z = -width;
		
		leftSide.SetPosition(0, pos);
		rightSide.SetPosition(0, pos);
		
		//Beam goes to left and right sides
		leftSide.SetPosition(1, GameObject.Find("Left").transform.position);
		rightSide.SetPosition(1, GameObject.Find ("Right").transform.position);
		
		//
		topLeft.SetPosition(0, GameObject.Find ("Left").transform.position);
		topRight.SetPosition(0, GameObject.Find("Right").transform.position);
		
		
	}
}
