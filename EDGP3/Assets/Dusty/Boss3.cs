using UnityEngine;
using System.Collections;

public class Boss3 : MonoBehaviour {
	public float health;
	public float maxhealth;
	public float summoncd,firecd,raincd,pewpew1cd,pewpew2cd,pewpewfcd;
	public float time1,time2,time3,time4,time5,time6;
	public bool alive;
	public GameObject projectileA;
	public GameObject projectileB;
	public GameObject fires;
	public GameObject ice;
	public GameObject lightning;
	private GameObject shippe1;
	private GameObject shippe2;
	private GameObject shippeatk;
	Vector3[] path;
	GameObject Master;
	bool affinity = false;
	private Vector3 target;
	public int speed;
	float acc = 0;
	float step;
	bool sumlight;
	public int phase = 0;
	bool firefire = false;
	int amount = 3;
	// Use this for initialization
	void Start () {
		shippe1 = GameObject.FindGameObjectWithTag ("Player1");
		shippe2 = GameObject.FindGameObjectWithTag ("Player2");
		time1 = Time.time;
		time2 = Time.time;
		time3 = Time.time;
		time4 = Time.time;
		time5 = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		if(firecd <= Time.time - time1 && phase == 0){
			Vector3 area = new Vector3(Random.Range (-4, 4),Random.Range (0, 5),0);
			Instantiate(fires, area, transform.rotation);

			time1 = Time.time;
		}
		if(pewpew1cd <= Time.time - time5 && phase == 0){
			pewpew1();
			
			time5 = Time.time;
		}
		if(pewpew2cd <= Time.time - time6 && phase == 1 && firefire == false){
			firefire = true;
			int random = Random.Range (0, 2);
			if (random == 0) affinity = false;
			else affinity = true;
			amount = 3;
			//time6 = Time.time;
		}
		if(firefire && pewpewfcd <= Time.time-time6){
			pewpew2 (affinity);
			amount--;
			time6 = Time.time;
			if(amount < 0){
				firefire = false;

				amount = 3;
			}
		}
		if(raincd <= Time.time - time2 && phase == 1){
			rainice();
			
			time2 = Time.time;
		}
		if(sumlight == false && phase == 2){
			sumlight = true;
			light();
		}
	}
	void light(){
		GameObject a;
		a = Instantiate(lightning, new Vector3(0,0,0), lightning.transform.rotation) as GameObject;

	}
	void rainice(){
		GameObject bullet;
		int safe = 0;
		int count = 0;
		for(int j = 0; j < 10; j++){
			
			int random = Random.Range (0, 4);
			if (random == 0) affinity = true;
			else affinity = false;
			
			Vector3 b = new Vector3(j-4,5,0);
			Vector3 bb = new Vector3(j-4 +.5f,5,0);
			if (((affinity == false || safe == 1) && count < 7) || count == 0) {
				bullet = Instantiate(ice, b, transform.rotation) as GameObject;
				bullet = Instantiate(ice, bb, transform.rotation) as GameObject;
				count++;
			}else {
				safe = 1;
			}
}
	}
	void pewpew1(){
		GameObject bullet;
		int random = Random.Range (0, 2);
		if (random == 0) affinity = false;
		else affinity = true;
		random = Random.Range (0, 2);
		if (random == 0) shippeatk = shippe1;
		else shippeatk = shippe2;
		if (affinity == false) {
			float angle = 0;
			Vector3 relative = transform.InverseTransformPoint(shippe1.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);

			relative = transform.InverseTransformPoint(shippe2.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			
		}else {
			float angle = 0;
			Vector3 relative = transform.InverseTransformPoint(shippe1.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);

			relative = transform.InverseTransformPoint(shippe2.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			
		}

	}
	void pewpew2(bool affinity){
		GameObject bullet;
		int random = Random.Range (0, 2);
		if (random == 0) shippeatk = shippe1;
		else shippeatk = shippe2;
		if (affinity == false) {
			float angle = 0;
			Vector3 relative = transform.InverseTransformPoint(shippe1.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			
			relative = transform.InverseTransformPoint(shippe2.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			
		}else {
			float angle = 0;
			Vector3 relative = transform.InverseTransformPoint(shippe1.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			
			relative = transform.InverseTransformPoint(shippe2.transform.position);
			angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
			bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
			bullet.transform.Rotate(0,0,-angle+180);
			
		}
	}
	public void changeloc(Vector3 a){
		target = a;
		
	}
}
