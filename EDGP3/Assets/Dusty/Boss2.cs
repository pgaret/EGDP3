using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {
	public int health;
	public float summoncd,firecd,raincd,ramcd;
	public float time1,time2,time3,time4;
	public bool alive;
	public GameObject projectileA;
	public GameObject projectileB;
	private GameObject shippe1;
	private GameObject shippe2;
	private GameObject shippeatk;
	Vector3[] path;
	GameObject Master;
	bool affinity = false;
	private Vector3 target;
	public int speed;
	public int maxspeed;
	public bool rush;
	float acc = 0;
	float step;
	// Use this for initialization
	void Start () {
		rush = false;
		time1 = Time.time;
		time2 = Time.time;
		time3 = Time.time;
		time4 = Time.time;
		Master = GameObject.FindGameObjectWithTag("Manager");
		shippe1 = GameObject.FindGameObjectWithTag ("Player1");
		shippe2 = GameObject.FindGameObjectWithTag ("Player2");
		//transform.rotation = Quaternion.Euler(new Vector3(180,0,0));
		path = new Vector3[4];
		path[0] = new Vector3(0,4,0);
		path[1] = new Vector3(0,0,0);
		path[2] = new Vector3(4,0,0);
		path[3] = new Vector3(-4,0,0);
		changeloc(path[0]);
		step = speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(rush == false){
			step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);
			if(transform.position == target){
				int random = Random.Range (0, 4);
				changeloc(path[random]);
			}
		}
		if(summoncd <= Time.time - time1 && rush == false){
			Master.GetComponent<spawn>().RandomSummon();
			time1 = Time.time;
		}
		if(firecd <= Time.time - time2 && rush == false){
			//Master.GetComponent<spawn>().RandomSummon();
			time2 = Time.time;
		}
		if(raincd <= Time.time - time3 && rush == false){
			GameObject bullet;
			for(int j = 0; j < 13; j++){
				
				int random = Random.Range (0, 2);
				if (random == 0) affinity = false;
				else affinity = true;
				
				Vector3 b = new Vector3(j-6,5,0);
				Vector3 bb = new Vector3(j-6 +.5f,5,0);
				if (affinity == false) {
					bullet = Instantiate(projectileA, b, transform.rotation) as GameObject;
					bullet = Instantiate(projectileA, bb, transform.rotation) as GameObject;
				}else {
					bullet = Instantiate(projectileB, b, transform.rotation) as GameObject;
					bullet = Instantiate(projectileB, bb, transform.rotation) as GameObject;
				}
				bullet.GetComponent<EnemyBullet>().setatktype(1);
			}
			time3 = Time.time;
		}
		if(ramcd <= Time.time - time4 && rush == false){
			rush = true;
			int random = Random.Range (0, 2);
			if (random == 0) shippeatk = shippe1;
			else shippeatk = shippe2;
			rotations();

			time4 = Time.time;
		}
		if(rush){
			step = (speed + acc) * Time.deltaTime;
			acc += 1.4f;
			rigidbody.AddForce(transform.up * step);
		}
	}
	public void changeloc(Vector3 a){
		target = a;
		
	}
	void rotations(){
		float angle = 0;
		Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
		angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
		transform.Rotate(0,0, -angle);
	}
	public void sethealth(int a){
		health = a;
	}
}

