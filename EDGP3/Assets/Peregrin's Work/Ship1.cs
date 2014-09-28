using UnityEngine;
using System.Collections;

public class Ship1 : MonoBehaviour {
	
	public float speed;
	public Transform projectile;
	public bool shipType;
	public int ammo = 0;
	public Sprite Red;
	public Sprite RedB;
	public Sprite RedA;
	
	float attackTimer = .5f;
	float attackCD = 0;

	bool affinity = false;
	GameObject ship2;
	
	// Use this for initialization
	void Start () {
		shipType = false;
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width * 7 / 8, Screen.height / 2, 0));
		pos.z = 0;
		transform.position = pos;
		gameObject.GetComponent<SpriteRenderer> ().sprite = RedA;
		
	}
	
	void OnGUI()
	{
		GUI.Box (new Rect (Screen.width / 3, Screen.height / 4, Screen.width / 3, Screen.height / 8), "Player 1 Ammo: " + ammo.ToString() + "  Player 2 Ammo: " + GameObject.Find ("Ship2").GetComponent<Ship2> ().ammo.ToString ());

	}
	
	// Update is called once per frame
	void Update () {
		if (shipType == false)
		{
			if (affinity == true) gameObject.GetComponent<SpriteRenderer>().sprite = RedA;
			else gameObject.GetComponent<SpriteRenderer>().sprite = RedB;
		}
		if (shipType == true) gameObject.GetComponent<SpriteRenderer>().sprite = Red;
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
				GameObject.Find ("Ship2").GetComponent<Ship2>().shipType = false;
				GameObject.Find ("Ship2").GetComponent<Ship2>().ammo = 0;
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
				GameObject.Find ("Ship2").GetComponent<Ship2>().shipType = true;
				GameObject.Find ("Ship2").GetComponent<Ship2>().ammo = 10;
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
		if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up*Time.deltaTime*speed);
		if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left*Time.deltaTime*speed);
		if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down*Time.deltaTime*speed);
		if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right*Time.deltaTime*speed);
		
	}
}
