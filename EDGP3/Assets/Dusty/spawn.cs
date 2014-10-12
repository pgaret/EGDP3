using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO; 


public class spawn : MonoBehaviour {
	public GameObject enemy;
	int x = 10;
	int y = 10;
	public float enter;
	float time;
	public bool summon = false;
	// Use this for initialization
	void Start () {
		time = Time.time;

		
	}
	
	// Update is called once per frame
	void Update () {

		if(time + enter <= Time.time && summon){
			Diagonal(1,1,3);
			Down (1,0,3);
			time = Time.time;
		}
	}
	public void starter(){
		time = Time.time;
		summon = true;
	}
	public void Down(int numene,int start,int type){
		numene += 6;
		for(int i = numene; i > 6; i--){
			GameObject test;
			test = Instantiate(enemy, new Vector3(start, i, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().changeloc(new Vector3(start, -i, 0));
			test.GetComponent<Enemy>().attacktype = type;
		}
	}
	public void Diagonal(int numene,int dir,int type){
		numene += 6;
		for(int i = numene; i > 6; i--){
			GameObject test;
			if(dir == 0)
				test = Instantiate(enemy, new Vector3(i, i, 0), Quaternion.identity) as GameObject;
			else
				test = Instantiate(enemy, new Vector3(-i, i, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().changeloc(new Vector3(i, -i, 0));
			test.GetComponent<Enemy>().attacktype = type;
		}
	}
	
}
