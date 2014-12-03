using UnityEngine;
using System.Collections;

public class Ballo : MonoBehaviour {
	public GameObject flames;
	int numb;
	public float cd;
	public float timealive;
	public float spawntime;
	float summontime;
	float time;
	bool alive = false;

	// Use this for initialization
	void Start () {
		numb = 0;
		time = Time.time;
		summontime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(spawntime <= Time.time - summontime){
			alive = true;
		}
		if(timealive <= Time.time - summontime){
			Destroy(gameObject);
		}
		if(alive && cd <= Time.time - time){
			fireradius();
			time = Time.time;
		}

	}
	void fireradius(){
		GameObject bullet;
		for(int i = numb; i <(360 + numb); i += 90){
			Transform a = transform;
			Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + i));
			bullet = Instantiate(flames, transform.position, up) as GameObject;
		}
		numb += 30;
	}
}
