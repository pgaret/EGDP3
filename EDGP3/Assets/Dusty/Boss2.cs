using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {
	public float health;
	public float maxhealth;
	public float summoncd,firecd,raincd,ramcd;
	public float time1,time2,time3,time4;
	public bool alive;
	Animator anim;

	public GameObject projectileA;
	public GameObject projectileB;
	public GameObject transition;
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
	public int phase = 0;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
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
		step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		if(rush == false && phase >= 1){
			if(transform.position == target){
				int random = Random.Range (0, 4);
				changeloc(path[random]);
				fireradius();
			}
		}
		if(summoncd <= Time.time - time1 && rush == false){
			//Master.GetComponent<spawn>().RandomSummon();
			//Master.GetComponent<spawn>().RandomSummon();
			time1 = Time.time;
		}
		if(firecd <= Time.time - time2 && rush == false){
			//Master.GetComponent<spawn>().RandomSummon();
			firecone();
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
		if(ramcd <= Time.time - time4 && rush == false && phase == 2){
			rush = true;
			int random = Random.Range (0, 2);
			if (random == 0) shippeatk = shippe1;
			else shippeatk = shippe2;
			rotations();
			anim.SetBool("Dash",true);
			time4 = Time.time;
			transform.Rotate(0,0,180);
		}
		if(rush){
			step = (speed + acc) * Time.deltaTime;
			acc += 3f;
			rigidbody.AddForce(-transform.up * step);
		}
		//Health phases
		if(health/maxhealth < .3f){
			phase = 2;
		}else
		if(health/maxhealth < .6f){
			phase = 1;
		}
		
		if (health <= 0)
		{
			Destroy(gameObject);
			Instantiate(transition);
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
	void firecone(){
		GameObject bullet;
		int random = Random.Range (0, 2);
		if (random == 0) affinity = false;
		else affinity = true;
		random = Random.Range (0, 2);
		if (random == 0) shippeatk = shippe1;
		else shippeatk = shippe2;
		if (affinity == false) {
			float angle = 0;
			Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+10+180);
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle-10+180);
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+15+180);
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle-15+180);
			
		}else {
			float angle = 0;
			Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+10+180);
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle-10+180);
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+15+180);
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle-15+180);
			
		}
	}
	void fireradius(){
		GameObject bullet;
		int random = Random.Range (0, 2);
		if (random == 0) affinity = false;
		else affinity = true;
		for(int i = 0; i <360 ; i += 20){
			Transform a = transform;
			Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + i));
			if (affinity == false) {
				bullet = Instantiate(projectileA, transform.position, up) as GameObject;
			}else {
				bullet = Instantiate(projectileB, transform.position, up) as GameObject;
			}
		}
	}
	public void subhealth(){
		health--;
	}
	public void sethealth(float a){
		health = a;
	}
}

