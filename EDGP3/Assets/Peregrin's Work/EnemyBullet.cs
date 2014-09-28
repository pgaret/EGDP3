using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
	
	public float speed;
	public Vector2 clickedPoint;

	float timer;
	
	// Use this for initialization
	void Start () {
		timer = Time.time;
		int random = Random.Range (0, 2);
		if (random == 0) clickedPoint = GameObject.Find ("Ship1").transform.position;
		if (random == 1) clickedPoint = GameObject.Find ("Ship2").transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timer > 10.0f) Destroy (gameObject);
		transform.position = Vector2.MoveTowards(transform.position, clickedPoint, speed * Time.deltaTime);
		if (transform.position.x == clickedPoint.x && transform.position.y == clickedPoint.y) Destroy (gameObject);
		
	}
}
