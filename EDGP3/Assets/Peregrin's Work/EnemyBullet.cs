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
	float fireTime;
	float Y;
	// Use this for initialization
	void Start () {
		fireTime = Time.time;
		startPos = transform.position;
		timer = Time.time;
		int random = Random.Range (0, 2);


	}
	
	// Update is called once per frame
	void Update () {

		if(bullettype != 2){
			rigidbody.AddForce(transform.up * bulletspeed);
			rigidbody.velocity = Vector3.zero;
		}
		else if (bullettype == 2){
			Y = Mathf.Sin((Time.time-fireTime)*10)*3;
			transform.position = new Vector3(transform.position.x, Y, transform.position.z);
			rigidbody.AddForce(transform.up * bulletspeed*1.5f);
			rigidbody.velocity = Vector3.zero;
			
			
			
		}
		
	}
	public void setatktype(int a){
		bullettype = a;

	}


}
