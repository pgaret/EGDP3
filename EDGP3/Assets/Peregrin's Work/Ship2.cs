using UnityEngine;
using System.Collections;

public class Ship2 : MonoBehaviour {
//NOT BEING USED SHIP1 IS ON BOTH PLAYERS!!!!!!!!!!!
	
	public float speed;
	public Transform projectile;
	public bool shipType;
	public int ammo = 10;
	public Sprite Blue;
	public Sprite BlueB;
	public Sprite BlueA;

	
	float attackTimer = .5f;
	float attackCD = 0;
	
	bool affinity = true;


	GameObject ship1;
	
	// Use this for initialization
	void Start () {
		shipType = true;
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width / 8, Screen.height / 2, 0));
		pos.z = 0;
		transform.position = pos;
		
	}
	
	// Update is called once per frame
	void Update (){
		if (shipType == false)
		{
			if (affinity == true) gameObject.GetComponent<SpriteRenderer>().sprite = BlueA;
			else gameObject.GetComponent<SpriteRenderer>().sprite = BlueB;
		}
		if (shipType == true) gameObject.GetComponent<SpriteRenderer>().sprite = Blue;
		if (shipType == false)
		{
			GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
			GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
			
			for (int i = 0; i < bulletA.Length; i++)
			{
				if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == false)
				{
					Destroy (bulletA[i].gameObject);
				}
				else if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == true)
				{
					ammo += 1;
					Destroy (bulletA[i].gameObject);
				}
			}
			for (int i = 0; i < bulletB.Length; i++)
			{
				if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == false)
				{
					ammo += 1;
					Destroy(bulletB[i].gameObject);
				}
				else if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && affinity == true)
				{
					Destroy(bulletB[i].gameObject);
				}
			}
			
			if (ammo == 10)
			{
				shipType = true;
				GameObject.Find ("Ship1").GetComponent<Ship1>().shipType = false;
				GameObject.Find ("Ship1").GetComponent<Ship1>().ammo = 0;
			}
			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
			{
				affinity = !affinity;
			}
		}
		else
		{	
			if (Input.GetKey(KeyCode.Space) && attackTimer < Time.time - attackCD && ammo > 0)
			{
				Instantiate (projectile, transform.position, Quaternion.identity);
				attackCD = Time.time;
				ammo -= 1;
			}
			if (ammo == 0) 
			{
				shipType = false;
				GameObject.Find ("Ship1").GetComponent<Ship1>().shipType = true;
				GameObject.Find ("Ship1").GetComponent<Ship1>().ammo = 10;
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
		if (Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.up*Time.deltaTime*speed);
		if (Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left*Time.deltaTime*speed);
		if (Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.down*Time.deltaTime*speed);
		if (Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right*Time.deltaTime*speed);
		
	}
}
