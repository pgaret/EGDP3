using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed;
	public float height;
	public Transform projectileA;
	public Transform projectileB;
	public float attackCD = 1;
	public bool fire = false;

	float attackTimer = 0;

	bool affinity = false;
	private Vector3 target;


	// Use this for initialization
	void Start () {
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (10, 10));
		pos.z = 0;
		//transform.position = pos;
	
	}
	
	// Update is called once per frame
	void Update () {
		int random = Random.Range (0, 2);
		if (random == 0) affinity = false;
		else affinity = true;

		if (attackCD < Time.time - attackTimer && fire == true)
		{
			if (affinity == false) Instantiate(projectileA, transform.position, Quaternion.identity);
			else Instantiate(projectileB, transform.position, Quaternion.identity);
			attackTimer = Time.time;
		}
		GameObject[] bullets = GameObject.FindGameObjectsWithTag ("BulletC");
		for (int i = 0; i < bullets.Length; i++)
		{
			if (bullets[i].renderer.bounds.Intersects(transform.renderer.bounds)) Destroy (gameObject);
		}
		float step = speed * Time.deltaTime;
		//transform.rotation = Quaternion.Lerp(transform.rotation,qr,Time.deltaTime*turnspeed);
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		checker ();
	}
	void checker(){
		if(transform.position == target){
			Destroy(gameObject);
		}
	}
	public void changeloc(Vector3 a){
		target = a;

	}
	public void fireset(bool a){
		fire = a;

	}

}
