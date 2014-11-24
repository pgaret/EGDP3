using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO; 


public class spawn : MonoBehaviour {
	public GameObject[] enemy;
	public GameObject Bossa;
	public GameObject Bossb;
	public GameObject Bossc;
	int x = 10;
	int y = 10;
	float[] EnterTimes;
	public float enter;
	public float time;
	public int i = 0;
	public bool summon = false;
	public int stage = 0;
	// Use this for initialization
	void Start () {

		time = Time.time;
		EnterTimes = new float[28];
		//Wave 1
		EnterTimes[0] = 1f;
		EnterTimes[1] = 3f;
		EnterTimes[2] = 5f;
		EnterTimes[3] = 6f;
		//Wave 2
		EnterTimes[4] = 14f;
		EnterTimes[5] = 16f;
		EnterTimes[6] = 18f;
		EnterTimes[7] = 19f;
		//Wave 3
		EnterTimes[8] = 28f;
		//Wave 4
		EnterTimes[9] = 41f;
		EnterTimes[10] = 41f;
		//Wave 5
		EnterTimes[11] = 48f;
		EnterTimes[12] = 48f;
		//Wave 6
		EnterTimes[13] = 58f;
		EnterTimes[14] = 59f;
		//Wave 7
		EnterTimes[15] = 66f;
		EnterTimes[16] = 67f;
		//Wave 8
		EnterTimes[17] = 75f;
		EnterTimes[18] = 76f;
		//Wave 9
		EnterTimes[19] = 84f;
		EnterTimes[20] = 85f;
		EnterTimes[21] = 86f;
		EnterTimes[22] = 87f;
		//Wave 10
		EnterTimes[23] = 97f;
		EnterTimes[24] = 98f;
		EnterTimes[25] = 99f;
		EnterTimes[26] = 100f;
		
		//Boss 1
		EnterTimes[27] = 115f;
		
		

	}
	
	// Update is called once per frame
	void Update () {
		if(i < EnterTimes.Length && EnterTimes[i] <= Time.time - time && summon && stage == 0){
			WaveS1(i);
			i++;
		}
		if(i < EnterTimes.Length && EnterTimes[i] <= Time.time - time && summon && stage == 1){
			WaveS2(i);
			i++;
		}
		if(i < EnterTimes.Length && EnterTimes[i] <= Time.time - time && summon && stage == 2){
			WaveS3(i);
			i++;
		}
	}
	public void starter(){
		time = Time.time - time;
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
	public void Setpath(int numene,int x, int y,int path,int type, int anim, bool affinity){
		numene += 6;
		for(int i = numene; i > 6; i--){
			GameObject test;
			test = Instantiate(enemy[anim], new Vector3(x, y, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().path = path;
			//test.GetComponent<Enemy>().changeloc(new Vector3(i, -i, 0));
			test.GetComponent<Enemy>().attacktype = type;
			test.GetComponent<Enemy>().affinity = affinity;
		}
	}
	public void Boss1(){
		GameObject test;
		test = Instantiate(Bossa, new Vector3(0, 7, 0), Quaternion.identity) as GameObject;
		test.GetComponent<Boss1>().changeloc(new Vector3(0, 4, 0));
	}
	public void Boss2(float health){
		GameObject test;
		test = Instantiate(Bossb, new Vector3(0, 7, 0), Quaternion.identity) as GameObject;
		test.GetComponent<Boss2>().sethealth(health);
		//test.GetComponent<Boss1>().changeloc(new Vector3(0, 4, 0));
	}
	public void Boss3(){
		GameObject test;
		test = Instantiate(Bossc, new Vector3(0, 7, 0), Quaternion.identity) as GameObject;
		test.GetComponent<Boss3>().changeloc(new Vector3(0, 4, 0));
		//test.GetComponent<Boss1>().changeloc(new Vector3(0, 4, 0));
	}

	void WaveS1(int i){
	//Using function Setpath
	//Numene: number of enemies this call of the function spawns
	//X: position along the x axis, where -4 is at the left side and 4 is at the right side
	//Y: position along the y axis, where 4 is at the top and 0 is at the middle
	//Path: See the checker function in Enemy.cs
	//Type: Type of bullet, where 0 is Down, 1 is Cone, 2 is irrelevent, 3 is radial, 4 is sprinkler, 5 is rain
	//Anim: the transform (which has all the animated enemies), see the enemies folder to find the number for each enemy
	//and the manager script for which index in the enemy array holds that given enemy
	
		//Wave 1
		if(i == 0){
			Setpath(1,-4,1,1,0,0); //0 is red bat
//			Boss1();
		}
		if(i == 1){
			Setpath(1,4,2,2,0,0);
			
		}
		if (i == 2){
			Setpath(1,-4,3,3,0,0);
		}
		if (i == 3){
			Setpath(1,4,4,4,0,0);
		}
		//Wave 2
		if (i == 4){
			Setpath(1,-4,1,5,0,0);
			Boss1();
		}
		if (i == 5){
			Setpath(1,4,2,6,0,0);
		}
		if (i == 6){
			Setpath(1,-4,3,7,0,0);
		}
		if (i == 7){
			Setpath(1,4,4,8,0,0);
		}
		//Wave 3
		if (i == 8){
			Setpath(1,-2,5,9,3,10); //3 is green slime
			Setpath(1,-4,1,1,0,0);
			Setpath(1,4,2,2,0,0);
		}
		//Wave 4
		if (i == 9){
			Setpath(1,-4,2,10,3,10);
			Setpath(1,4,2,11,3,10);
		}
		if (i == 10){
			Setpath(1,-4,1,1,0,0);
			Setpath(1,4,2,2,0,0);
		}
		//Wave 5
		if(i == 11){
			Setpath(1,-4,0,12,1,0);
			Setpath(1,4,1,13,0,0);
		}
		if (i == 12){
			Setpath(1,-3,2,14,0,0);
			Setpath(1,4,3,15,1,0);
		}
		//Wave 6
		if (i == 13){
			Setpath(1,-4,4,16,1,0);
			Setpath(1,4,4,17,1,0);
		}
		if (i == 14){
			Setpath(1,0,4,18,3,10);
		}
		//Wave 7
		if(i == 15){
			Setpath(1,-3,2,14,0,0);
			Setpath(1,-4,1,1,0,0);
			Setpath(1,4,1,13,0,0);
		}
		if(i == 16){
			Setpath(1,4,2,2,0,0);
			Setpath(1,4,3,15,1,0);
		}
		//Wave 8
		if (i == 17){
			Setpath(1,4,2,11,3,10);
			Setpath(1,-4,1,5,0,0);
			Setpath(1,-4,3,7,0,0);
		}
		if (i == 18){
			Setpath(1,4,4,17,1,0);
			Setpath(1,4,2,11,3,10);
		}
		//Wave 9
		if (i == 19){
			Setpath(1,-2,4,19,1,12); //12 is blue slime
		}
		if (i == 20){
			Setpath(1,-1,4,20,1,12);
		}
		if (i == 21){
			Setpath(1,1,4,21,1,12);
		}
		if (i == 22){
			Setpath(1,2,4,22,1,12);
		}
		//Wave 10
		if(i == 23){
			Setpath(1,-4,2,10,3,10);
			Setpath(1,4,4,8,0,1);
		}
		if (i == 24){
			Setpath(1,4,4,4,0,0);
		}
		if (i == 25){
			Setpath(1,4,2,6,0,0);
		}
		if (i == 26){
			Setpath(1,4,2,2,0,0);
		}
		if (i == 27){
			Boss1 ();
		}
		//Wave 11
		if (i == 27){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 28){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 30){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 32){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 33){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 34){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 35){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}
		if (i == 36){
//			Diagonal(2,1,3);
//			Down (1,0,2);
//			Side (2,0,3,1);
//			Setpath(2,0,3,1);
		}												
//		if (i == 37){
//			Boss1();
//		}
	}
	void WaveS2(int i){
		if(i == 0){
			//			Setpath(1,-4,1,1,0,0); //0 is red bat
			Boss1();
		}
		if(i == 1){
			Setpath(1,4,2,2,0,0);
		}
		if (i == 2){
			Setpath(1,-4,3,3,0,0);
		}
		if (i == 3){
			Setpath(1,4,4,4,0,0);
		}
		//Wave 2
		if (i == 4){
			Setpath(1,-4,1,5,0,0);
		}
		if (i == 5){
			Setpath(1,4,2,6,0,0);
		}
		if (i == 6){
			Setpath(1,-4,3,7,0,0);
		}
		if (i == 7){
			Setpath(1,4,4,8,0,0);
		}
		//Wave 3
		if (i == 8){
			Setpath(1,-2,5,9,3,10); //3 is green slime
			Setpath(1,-4,1,1,0,0);
			Setpath(1,4,2,2,0,0);
		}
		//Wave 4
		if (i == 9){
			Setpath(1,-4,2,10,3,10);
			Setpath(1,4,2,11,3,10);
		}
		if (i == 10){
			Setpath(1,-4,1,1,0,0);
			Setpath(1,4,2,2,0,0);
		}
		//Wave 5
		if(i == 11){
			Setpath(1,-4,0,12,1,0);
			Setpath(1,4,1,13,0,0);
		}
		if (i == 12){
			Setpath(1,-3,2,14,0,0);
			Setpath(1,4,3,15,1,0);
		}
		//Wave 6
		if (i == 13){
			Setpath(1,-4,4,16,1,0);
			Setpath(1,4,4,17,1,0);
		}
		if (i == 14){
			Setpath(1,0,4,18,3,10);
		}
		//Wave 7
		if(i == 15){
			Setpath(1,-3,2,14,0,0);
			Setpath(1,-4,1,1,0,0);
			Setpath(1,4,1,13,0,0);
		}
		if(i == 16){
			Setpath(1,4,2,2,0,0);
			Setpath(1,4,3,15,1,0);
		}
		//Wave 8
		if (i == 17){
			Setpath(1,4,2,11,3,10);
			Setpath(1,-4,1,5,0,0);
			Setpath(1,-4,3,7,0,0);
		}
		if (i == 18){
			Setpath(1,4,4,17,1,0);
			Setpath(1,4,2,11,3,10);
		}
		//Wave 9
		if (i == 19){
			Setpath(1,-2,4,19,1,12); //12 is blue slime
		}
		if (i == 20){
			Setpath(1,-1,4,20,1,12);
		}
		if (i == 21){
			Setpath(1,1,4,21,1,12);
		}
		if (i == 22){
			Setpath(1,2,4,22,1,12);
		}
		//Wave 10
		if(i == 23){
			Setpath(1,-4,2,10,3,10);
			Setpath(1,4,4,8,0,1);
		}
		if (i == 24){
			Setpath(1,4,4,4,0,0);
		}
		if (i == 25){
			Setpath(1,4,2,6,0,0);
		}
		if (i == 26){
			Setpath(1,4,2,2,0,0);
		}
		if (i == 27){
			Boss2 (10);
		}
	}
	void WaveS3(int i){
		if (i == 0){
			Boss3();
		}
	}
	public void RandomSummon(){
		int random = 3;
		if (random == 0) {
			Diagonal(2,Random.Range (0, 1),Random.Range (0, 5));
		}
		else if(random == 1){
			Side(2,Random.Range (1, 5),Random.Range (0, 1),Random.Range (0, 5));
		}
		else if(random == 2){
			Down(2,Random.Range (-3, 3),Random.Range (0, 5));
		}
		else {
			Setpath(1, Random.Range (-3, 3), Random.Range (-2, 2), Random.Range (1, 5), Random.Range (0, 3), 10);
		}

	}
	
}
