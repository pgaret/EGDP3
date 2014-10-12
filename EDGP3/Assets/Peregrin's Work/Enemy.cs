using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed;
	public float height;
	public GameObject projectileA;
	public GameObject projectileB;
	public float attackCD = 1;
	public bool fire = false;
	public int attacktype = 0;
	float attackTimer = 0;
	private GameObject shippe1;
	private GameObject shippe2;
	private GameObject shippeatk;
	bool affinity = false;
	private Vector3 target;


	// Use this for initialization
	void Start () {
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (10, 10));
		pos.z = 0;
		//transform.position = pos;
		shippe1 = GameObject.FindGameObjectWithTag ("Player1");
		shippe2 = GameObject.FindGameObjectWithTag ("Player2");
		transform.rotation = Quaternion.Euler(new Vector3(180,0,0));

		int random = Random.Range (0, 2);
		if (random == 0) shippeatk = shippe1;
		else shippeatk = shippe2;
	}
	
	// Update is called once per frame
	void Update () {
		int random = Random.Range (0, 2);
		if (random == 0) affinity = false;
		else affinity = true;



		if (attackCD < Time.time - attackTimer && fire == true){
				GameObject bullet;
			if (attacktype == 0){

				if (affinity == false){
					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;

				}
				else {
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;


					}

			}else if(attacktype == 1){
				rotations();
				if (affinity == false) {

					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
					Transform a = transform;

					Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + 10));
					bullet = Instantiate(projectileA, transform.position, up) as GameObject;

					Quaternion down = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z - 10));
					bullet = Instantiate(projectileA, transform.position, down) as GameObject;

				}
				else {
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
					Transform a = transform;

					Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + 10));
					bullet = Instantiate(projectileB, transform.position, up) as GameObject;

					Quaternion down = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z - 10));
					bullet = Instantiate(projectileB, transform.position, down) as GameObject;

				}

			}else if(attacktype == 2){
				rotations();
				if (affinity == false) {
					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
					bullet.GetComponent<EnemyBullet>().setatktype(attacktype);
				}else {
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
					bullet.GetComponent<EnemyBullet>().setatktype(attacktype);
				}
			}else if(attacktype == 3){

				for(int i = 0; i <360 ; i += 20){
					Transform a = transform;
					
					Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + i));
					bullet = Instantiate(projectileA, transform.position, up) as GameObject;
				}
			}

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
	void rotations(){
		float angle = 0;
		GameObject bullet;
		Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
		angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
		transform.Rotate(0,0, -angle);
	}


}
