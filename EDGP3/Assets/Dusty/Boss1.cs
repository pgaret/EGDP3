using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {
	public float health;
	public float summoncd,firecd,raincd;
	public float time1,time2,time3;
	public bool alive;
	public GameObject projectileA;
	public GameObject projectileB;
	public GameObject transition;
	private GameObject shippe1;
	private GameObject shippe2;
	private GameObject shippeatk;
	GameObject Master;
	bool affinity = false;
	private Vector3 target;
	public int speed;
	// Use this for initialization
	void Start () {
		time1 = Time.time;
		time2 = Time.time;
		time3 = Time.time + .3f;
		Master = GameObject.FindGameObjectWithTag("Manager");
		Master.GetComponent<spawn>().summon = false;
		//transform.rotation = Quaternion.Euler(new Vector3(180,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		if(summoncd <= Time.time - time1){
			Master.GetComponent<spawn>().RandomSummon();
			time1 = Time.time;
		}
		if(firecd <= Time.time - time2){
			//Master.GetComponent<spawn>().RandomSummon();
			time2 = Time.time;
		}
		if(raincd <= Time.time - time3){
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
		
		if (health == 0)
		{
			Destroy(gameObject);
			Instantiate(transition);
		}

	}
	public void subhealth(){
		health--;
	}
	public void changeloc(Vector3 a){
		target = a;
		
	}
}
