using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO; 


public class spawn : MonoBehaviour {
	public GameObject[] enemy = new GameObject[4];
	public GameObject Bossa;
	public GameObject Bossb;
	int x = 10;
	int y = 10;
	float[] EnterTimes;
	public float enter;
	float time;
	public int i = 0;
	public bool summon = false;
	// Use this for initialization
	void Start () {

		time = Time.time;
		EnterTimes = new float[3];
		EnterTimes[0] = 4f;
		EnterTimes[1] = 8f;
		EnterTimes[2] = 12f;

	}
	
	// Update is called once per frame
	void Update () {

		if(i < EnterTimes.Length && EnterTimes[i] + enter <= Time.time && summon){
			WaveM(i);
			time = Time.time;
			i++;
		}
	}
	public void starter(){
		time = Time.time;
		summon = true;
	}
	/*
	 * numene is amount of enemies that spawn
	 * start is the position in which the enemy spawns in the X axis
	 * type is enemy/bullet type
	 */
	public void Down(int numene,int start,int type){
		numene += 6;
		for(int i = numene; i > 6; i--){
			GameObject test;
			test = Instantiate(enemy[type], new Vector3(start, i, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().changeloc(new Vector3(start, -i, 0));
			test.GetComponent<Enemy>().attacktype = type;
		}
	}
	/*
	 * numene is amount of enemies that spawn
	 * start is the position in which the enemy spawns in the Y axis
	 * dir is either 0 or 1 for direction
	 * type is enemy/bullet type
	 */
	public void Side(int numene,int start,int dir,int type){
		numene += 7;
		int a;
		if(dir == 0){
			a = 1;
		}else
			a = -1;
		for(int i = numene; i > 7; i--){
			GameObject test;
			test = Instantiate(enemy[type], new Vector3(i*a, start, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().changeloc(new Vector3(-i*a, start, 0));
			test.GetComponent<Enemy>().attacktype = type;
		}
	}
	/*
	 * numene is amount of enemies that spawn
	 * dir is either 0 or 1 for direction
	 * type is enemy/bullet type
	 */
	public void Diagonal(int numene,int dir,int type){
		numene += 6;
		for(int i = numene; i > 6; i--){
			GameObject test;
			if(dir == 0)
				test = Instantiate(enemy[type], new Vector3(i, i, 0), Quaternion.identity) as GameObject;
			else
				test = Instantiate(enemy[type], new Vector3(-i, i, 0), Quaternion.identity) as GameObject;

			if(dir == 0)
				test.GetComponent<Enemy>().changeloc(new Vector3(-i, -i, 0));
			else
				test.GetComponent<Enemy>().changeloc(new Vector3(i, -i, 0));
			test.GetComponent<Enemy>().attacktype = type;
		}
	}
	/*
	 * numene is amount of enemies that spawn
	 * dir is either 0 or 1 for direction
	 * path should be always set to 1, if we ever decide on another path, this is inplace for different special paths
	 * type is enemy/bullet type
	 */
	public void Setpath(int numene,int dir,int path,int type){
		numene += 6;
		for(int i = numene; i > 6; i--){
			GameObject test;
			if(dir == 0)
				test = Instantiate(enemy[type], new Vector3(i, i, 0), Quaternion.identity) as GameObject;
			else
				test = Instantiate(enemy[type], new Vector3(-i, i, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().path = path;
			//test.GetComponent<Enemy>().changeloc(new Vector3(i, -i, 0));
			test.GetComponent<Enemy>().attacktype = type;
		}
	}
	public void Boss1(){
		GameObject test;
		test = Instantiate(Bossa, new Vector3(0, 6, 0), Quaternion.identity) as GameObject;
		test.GetComponent<Boss1>().changeloc(new Vector3(0, 4, 0));
	}
	public void Boss2(int health){
		GameObject test;
		test = Instantiate(Bossb, new Vector3(0, 6, 0), Quaternion.identity) as GameObject;
		test.GetComponent<Boss2>().sethealth(health);
		//test.GetComponent<Boss1>().changeloc(new Vector3(0, 4, 0));
	}

	void WaveM(int i){
		if(i == 0){
			Diagonal(2,1,2);
			//Boss2 (10);
			//Side (1,0,3,1);
			//Setpath(2,0,3,1);
		}
		if(i == 1){
			//Diagonal(2,1,3);
			//Down (1,0,5);
			Side (1,0,3,1);
			//Setpath(2,0,3,1);
		}
		if (i == 2){
			Boss2(10);
			//Diagonal(2,1,3);
			//Down (1,0,5);
			//Side (1,0,3,1);
			//Setpath(2,0,3,1);
		}
	}
	public void RandomSummon(){
		int random = Random.Range (0, 3);
		if (random == 0) {
			Diagonal(2,Random.Range (0, 1),Random.Range (0, 4));
		}
		else if(random == 1){
			Side(2,Random.Range (0, 5),Random.Range (0, 1),Random.Range (0, 4));
		}
		else if(random == 2){
			Down(2,Random.Range (-4, 4),Random.Range (0, 4));
		}
		else {
			Setpath(2,Random.Range (0, 1),1,Random.Range (0, 4));
		}

	}
	
}
