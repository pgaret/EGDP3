using UnityEngine;
using System.Collections;

public class Tinker : MonoBehaviour {

	public Transform projectile;
	public Transform shield;
	public string shipType;
	
	public Sprite YellowL;
	public Sprite YellowR;
	public Sprite RedL;
	public Sprite RedR;
	public Sprite BlueL;
	public Sprite BlueR;
	
	Sprite attackRight;
	Sprite attackLeft;
	
	public int attackType = 0;
	
	float specialTimer = 0;
	float specialCounter = 0;
	
	GameObject sound;
	
	// Use this for initialization
	void Start () {
		sound = GameObject.Find ("Sound");
		if (transform.tag == "Player2") Shield ();	
	}
	
	public void Shield()
	{
		Instantiate(shield, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (attackType == 0)
		{
			attackLeft = BlueL; attackRight = BlueR;
		}
		else if (attackType == 1)
		{
			attackLeft = RedL; attackRight = RedR;
		}
		else 
		{
			attackLeft = YellowL; attackRight = YellowR;
		}
		shipType = transform.GetComponent<PlayerStats>().role;
		
		if (GetComponent<PlayerStats>().shootBool == true)
		{
			sound.GetComponent<SoundManager>().PlaySound("punch");
			foreach (Transform child in transform)
			{
				Transform thePunch;
				if (child.name == "Left")
				{
					thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
					thePunch.GetComponent<SpriteRenderer>().sprite = attackLeft;
				}
				else if (child.name == "Right")
				{
					thePunch = (Transform)Instantiate (projectile, child.position, Quaternion.identity);
					thePunch.GetComponent<SpriteRenderer>().sprite = attackRight;
					transform.GetComponent<PlayerStats>().ammo -= 1;
				}
				GetComponent<PlayerStats>().shootBool = false;
			}
		}
		
		if (GetComponent<PlayerStats>().specialBool == true)
		{
			attackType += 1;
			if (attackType > 2) attackType = 0;
			GetComponent<Animator>().SetInteger("type", attackType);
			GetComponent<PlayerStats>().specialBool = false;
		}
		
		GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
		GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
		for (int i = 0; i < bulletA.Length; i++)
		{
			if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds))
			{
				Destroy (bulletA[i].gameObject);
			}
		}
		for (int i = 0; i < bulletB.Length; i++)
		{
			if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds))
			{
				Destroy(bulletB[i].gameObject);
			}
		}

		
	}
}
