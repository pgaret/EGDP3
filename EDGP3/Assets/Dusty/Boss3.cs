using UnityEngine;
using System.Collections;

public class Boss3 : MonoBehaviour {
	public float health;
	public float maxhealth;
	public float summoncd,firecd,raincd;
	public float time1,time2,time3,time4;
	public bool alive;
	public GameObject projectileA;
	public GameObject projectileB;
	public GameObject fires;
	public GameObject ice;
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
	public int phase = 0;
	// Use this for initialization
	void Start () {
		time1 = Time.time;
		time2 = Time.time;
		time3 = Time.time;
		time4 = Time.time;
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
		if(raincd <= Time.time - time2 && phase == 1){
			rainice();
			
			time2 = Time.time;
		}
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
	public void changeloc(Vector3 a){
		target = a;
		
	}
}
