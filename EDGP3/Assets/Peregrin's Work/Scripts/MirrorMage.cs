using UnityEngine;
using System.Collections;

public class MirrorMage : MonoBehaviour {
	
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	
	public float width = .1f;
	public Color color = Color.white;
	
	public Sprite mirror;
	
	GameObject sound;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	LineRenderer leftSide;
	LineRenderer rightSide;
	
	LineRenderer topLeft;
	LineRenderer topRight;
	
	GameObject left;
	GameObject right;
	
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Sound");
		left = GameObject.Find ("Left");
		right = GameObject.Find ("Right");
		if (transform.tag == "Player2") Shield ();
		//From player to mirrors lenses
		leftSide = left.AddComponent<LineRenderer>();
		rightSide = right.AddComponent<LineRenderer>();
		//From mirrors to top lenses
		topLeft = left.transform.GetChild(0).gameObject.AddComponent<LineRenderer>();
		topRight = right.transform.GetChild(0).gameObject.AddComponent<LineRenderer>();
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
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShipA");
		RaycastHit hit;

		if (Physics.Raycast(transform.position, leftSide.transform.position - transform.position, out hit))
		{
			if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
		}
		if (Physics.Raycast(transform.position, rightSide.transform.position - transform.position, out hit))
		{
			if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
		}
		if (Physics.Raycast(leftSide.transform.position, topLeft.transform.position - transform.position, out hit))
		{
			if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
		}
		if (Physics.Raycast(rightSide.transform.position, topRight.transform.position - transform.position, out hit))
		{
			if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= .05f;
		}
		
		
		
		
		//Beam follows the player
		Vector3 pos = transform.position;
		pos.z = -width;
		
		leftSide.SetPosition(0, pos);
		rightSide.SetPosition(0, pos);
		
		//Beam goes to left and right sides
		leftSide.SetPosition(1, left.transform.position);
		rightSide.SetPosition(1, right.transform.position);
		
		//Beam starts at left and right sides
		topLeft.SetPosition(0, left.transform.position);
		topRight.SetPosition(0, right.transform.position);
		
		//Change position of top destination
		pos = left.transform.GetChild(1).position;
		pos.x = transform.position.x;
		left.transform.GetChild(1).position = pos;
		pos = right.transform.GetChild(1).position;
		pos.x = transform.position.x;
		right.transform.GetChild(1).position = pos;
		
		//Beam goes to the top
		topLeft.SetPosition(1, left.transform.GetChild(1).transform.position);
		topRight.SetPosition(1, right.transform.GetChild(1).transform.position);
		
		
	}
}
