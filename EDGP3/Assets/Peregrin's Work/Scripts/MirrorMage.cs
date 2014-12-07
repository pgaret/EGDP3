using UnityEngine;
using System.Collections;

public class MirrorMage : MonoBehaviour {
	
	public Transform projectile;
	public Transform special;
	public Transform shield;
	public string shipType;
	public float damage;
	
	public float width = .1f;
	public Color color = Color.white;
	
	public Material lazor;
	public Sprite mirrorL;
	public Sprite mirrorR;
	public Sprite node;
	
	public string mode;
	
	GameObject sound;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	LineRenderer leftSide;
	LineRenderer rightSide;
	
	LineRenderer topLeft;
	LineRenderer topRight;
	
	GameObject left;
	GameObject right;
	
	float bulletTimer;
	float bulletFloat = 1;
	
	// Use this for initialization
	void Start () {
	
		left = GameObject.Find ("Left");
		right = GameObject.Find ("Right");
		
		sound = GameObject.Find ("Sound");
		if (tag == "Player1")
		{
			mode = "Attack";
			LazerTime();
			bulletTimer = bulletFloat + Time.time;
		}
		else
		{
			Shield ();
		}


	}
	
	void Special()
	{

	}
	
	public void Shield()
	{
		Instantiate(shield, transform.GetChild(0).position, Quaternion.identity);
		Instantiate(shield, transform.GetChild(1).position, Quaternion.identity);
	}
	
	void LazerTime()
	{
		left.GetComponent<SpriteRenderer>().sprite = mirrorL;
		right.GetComponent<SpriteRenderer>().sprite = mirrorR;
		left.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = node;
		//From player to mirrors lenses
		leftSide = left.AddComponent<LineRenderer>();
		rightSide = right.AddComponent<LineRenderer>();
		//From mirrors to top lenses
		topLeft = left.transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
		topRight = right.transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
		//Narrow the beams
		topLeft.SetWidth(width, width); topLeft.SetColors(color, color); topLeft.material = lazor;
		topRight.SetWidth(width, width); topRight.SetColors(color, color); topRight.material = lazor;
		leftSide.SetWidth(width, width); leftSide.SetColors(color, color); leftSide.material = lazor;
		rightSide.SetWidth(width, width); rightSide.SetColors(color, color); rightSide.material = lazor;
	}
	
	void NotLazerTime()
	{
		Destroy (leftSide);
		Destroy (rightSide);
		Destroy (topRight);
		Destroy (topLeft);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GetComponent<PlayerStats>().coinScore >= GetComponent<PlayerStats>().coin1 && width == .1f) width = .2f;
		if (GetComponent<PlayerStats>().coinScore >= GetComponent<PlayerStats>().coin2 && damage == .05f) damage = .1f;
		if (GetComponent<PlayerStats>().coinScore >= GetComponent<PlayerStats>().coin3 && width == .2f) width = .3f;
		
	
		if (GetComponent<PlayerStats>().role == "Defender" && GameObject.FindGameObjectsWithTag("MMShield").Length == 0)
		{
			Shield ();
			NotLazerTime();
			left.GetComponent<SpriteRenderer>().sprite = null;
			right.GetComponent<SpriteRenderer>().sprite = null;
			left.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
		}
		if (GetComponent<PlayerStats>().role  == "Attacker" && GameObject.FindGameObjectsWithTag("MMShield").Length > 0)
		{
			LazerTime();
			GameObject[] shields = GameObject.FindGameObjectsWithTag("MMShield");
			foreach (GameObject shield in shields) Destroy (shield);
		}	
		if (GetComponent<PlayerStats>().role == "Attacker" && GetComponent<PlayerStats>().ammo > 0)
		{
			RaycastHit hit;
	
			if (Physics.Raycast(transform.position, leftSide.transform.position - transform.position, out hit,  Vector3.Distance(transform.position, leftSide.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= damage*2;
				else if (hit.transform.tag == "Boss1") hit.transform.gameObject.GetComponent<Boss1>().subhealth(damage);
				else if (hit.transform.tag == "Boss2") hit.transform.gameObject.GetComponent<Boss2>().subhealth(damage);
				else if (hit.transform.tag == "Boss3") hit.transform.gameObject.GetComponent<Boss3>().subhealth(damage);
			}
			if (Physics.Raycast(transform.position, rightSide.transform.position - transform.position, out hit,  Vector3.Distance(transform.position, rightSide.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= damage*2;
				else if (hit.transform.tag == "Boss1") hit.transform.gameObject.GetComponent<Boss1>().subhealth(damage);
				else if (hit.transform.tag == "Boss2") hit.transform.gameObject.GetComponent<Boss2>().subhealth(damage);
				else if (hit.transform.tag == "Boss3") hit.transform.gameObject.GetComponent<Boss3>().subhealth(damage);
			}
			if (Physics.Raycast(leftSide.transform.position, topLeft.transform.position - leftSide.transform.position, out hit, Vector3.Distance(leftSide.transform.position, topLeft.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= damage;
				else if (hit.transform.tag == "Boss1") hit.transform.gameObject.GetComponent<Boss1>().subhealth(damage);
				else if (hit.transform.tag == "Boss2") hit.transform.gameObject.GetComponent<Boss2>().subhealth(damage);
				else if (hit.transform.tag == "Boss3") hit.transform.gameObject.GetComponent<Boss3>().subhealth(damage);
			}
			if (Physics.Raycast(rightSide.transform.position, topRight.transform.position - rightSide.transform.position, out hit, Vector3.Distance(rightSide.transform.position, topRight.transform.position)))
			{
				if (hit.transform.tag == "EnemyShipA") hit.transform.gameObject.GetComponent<Enemy>().health -= damage;
				else if (hit.transform.tag == "Boss1") hit.transform.gameObject.GetComponent<Boss1>().subhealth(damage);
				else if (hit.transform.tag == "Boss2") hit.transform.gameObject.GetComponent<Boss2>().subhealth(damage);
				else if (hit.transform.tag == "Boss3") hit.transform.gameObject.GetComponent<Boss3>().subhealth(damage);
			}
			
			//Beam follows the player
			Vector3 pos = transform.position;
			pos.z = .1f;
			
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
			pos.y = -transform.position.y;
			pos.z = 0f;
			left.transform.GetChild(1).position = pos;
			right.transform.GetChild(1).position = pos;
			
			//Beam goes to the top
			topLeft.SetPosition(1, left.transform.GetChild(1).transform.position);
			topRight.SetPosition(1, right.transform.GetChild(1).transform.position);
			
			if (Time.time > bulletTimer)
			{
				GetComponent<PlayerStats>().ammo -= 1;
				bulletTimer = Time.time + bulletFloat;
			}
			
			if (GetComponent<PlayerStats>().ammo == 0) NotLazerTime();
		}
		
		
	}
}
