using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	
	public float speed;
	public Vector2 clickedPoint;
	public int bullettype;
	public int bulletspeed;
	public int bulletrange = 30;
	private Vector3 startPos;
	float timer;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		timer = Time.time;
		int random = Random.Range (0, 2);
		if (random == 0) clickedPoint = GameObject.Find ("Ship1").transform.position;
		if (random == 1) clickedPoint = GameObject.Find ("Ship2").transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(startPos,transform.position);
		if(distance >=bulletrange)
		{
			Destroy(gameObject);
		}

		rigidbody2D.AddForce(transform.up * bulletspeed);
		rigidbody2D.velocity = Vector3.zero;

		
	}
	public void setatktype(int a){
		bullettype = a;

	}
}
