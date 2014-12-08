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
	float[] EnterTimes2;
	float[] EnterTimes3;
	public float enter;
	public float time;
	public int i = 0;
	public bool summon = false;
	public int stage = 0;
	// Use this for initialization
	void Start () {

		time = Time.timeSinceLevelLoad;
		//  LEVEL 1 //
		EnterTimes = new float[35];
		//Wave 01
		EnterTimes[0] = 1f+4;
		EnterTimes[1] = 3f+4;
		EnterTimes[2] = 5f+4;
		EnterTimes[3] = 6f+4;
		//Wave 02
		EnterTimes[4] = 14f+4;
		EnterTimes[5] = 16f+4;
		EnterTimes[6] = 18f+4;
		EnterTimes[7] = 19f+4;
		//Wave 03
		EnterTimes[8] = 14f+19;
		EnterTimes[9] = 16f+19;
		EnterTimes[10] = 18f+19;
		EnterTimes[11] = 19f+19;
		//Wave 04
		EnterTimes[12] = 28f+19;
		//Wave 05
		EnterTimes[13] = 41f+19;
		EnterTimes[14] = 41f+19;
		//Wave 06
		EnterTimes[15] = 48f+19;
		EnterTimes[16] = 48f+19;
		//Wave 07
		EnterTimes[17] = 58f+19;
		EnterTimes[18] = 59f+19;
		//Wave 08
		EnterTimes[19] = 66f+19;
		EnterTimes[20] = 67f+19;
		//Wave 09
		EnterTimes[21] = 75f+19;
		EnterTimes[22] = 76f+19;
		//Wave 10
		EnterTimes[23] = 84f+19;
		EnterTimes[24] = 85f+19;
		EnterTimes[25] = 86f+19;
		EnterTimes[26] = 87f+19;
		//Wave 11
		EnterTimes[27] = 97f+19;
		EnterTimes[28] = 98f+19;
		EnterTimes[29] = 99f+19;
		EnterTimes[30] = 100f+19;
		//Boss 1
		EnterTimes[31] = 140f;

		//  LEVEL 2 //
		EnterTimes2 = new float[40];
		//Wave 01
		EnterTimes2[00] = 03f;
		EnterTimes2[01] = 04f;
		//Wave 02
		EnterTimes2[02] = 10f;
		EnterTimes2[03] = 11f;
		EnterTimes2[04] = 12f;
		//Wave 03
		EnterTimes2[05] = 19f;
		EnterTimes2[06] = 20f;
		EnterTimes2[07] = 21f;
		EnterTimes2[08] = 22f;
		//Wave 04
		EnterTimes2[09] = 26f;
		EnterTimes2[10] = 27f;
		EnterTimes2[11] = 28f;
		EnterTimes2[12] = 29f;
		//Wave 05
		EnterTimes2[13] = 35f;
		EnterTimes2[14] = 36f;
		EnterTimes2[15] = 38f;
		EnterTimes2[16] = 39f;
		EnterTimes2[17] = 41f;
		//Wave 06		
		EnterTimes2[18] = 46f;
		EnterTimes2[19] = 48f;
		EnterTimes2[20] = 50f;
		EnterTimes2[21] = 52f;
		//Wave 07
		EnterTimes2[22] = 56f;
		EnterTimes2[23] = 57f;
		EnterTimes2[24] = 58f;
		EnterTimes2[25] = 59f;
		//Wave 08
		EnterTimes2[26] = 64f;
		EnterTimes2[27] = 65f;
		EnterTimes2[28] = 66f;
		EnterTimes2[29] = 67f;
		//Wave 09
		EnterTimes2[30] = 71f;
		EnterTimes2[31] = 74f;
		//Wave 10
		EnterTimes2[32] = 80f;
		EnterTimes2[33] = 81f;
		EnterTimes2[34] = 82f;
		EnterTimes2[35] = 83f;
		EnterTimes2[36] = 84f;
		//Boss 2
		EnterTimes2[37] = 94f;
		
		//  LEVEL 3 //
		EnterTimes3 = new float[30];
		//Wave 01
		EnterTimes3[00] = 05f;
		EnterTimes3[01] = 06f;
		EnterTimes3[02] = 07f;
		EnterTimes3[03] = 08f;
		//Wave 02
		EnterTimes3[04] = 13f;
		EnterTimes3[05] = 14f;
		EnterTimes3[06] = 16f;
		//Wave 03
		EnterTimes3[07] = 21f;
		EnterTimes3[08] = 23f;
		//Wave 04
		EnterTimes3[09] = 27f;
		EnterTimes3[10] = 29f;
		EnterTimes3[11] = 31f;
		EnterTimes3[12] = 33f;
		//Wave 04
		EnterTimes3[13] = 38f;
		EnterTimes3[14] = 40f;
		EnterTimes3[15] = 42f;
		EnterTimes3[16] = 44f;
		//Boss 3
		EnterTimes3[17] = 53f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(i);
	
		if(i < EnterTimes.Length && EnterTimes[i] <= Time.time - time && summon && stage == 0){
			WaveS1(i);
			i++;
		}
		if(i < EnterTimes2.Length && EnterTimes2[i] <= Time.time - time && summon && stage == 1){
			WaveS2(i);
			//print (i);
			i++;
		}
		if(i < EnterTimes3.Length && EnterTimes3[i] <= Time.time - time && summon && stage == 2){
			WaveS3(i);
			i++;
		}

	}
	public void starter(){
		i = 0;
		time = Time.time;
		summon = true;

	}
	public void con(){
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
	 * path should be always set to 1, if we ever decide on another path, this is in place for different special paths
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
	
	//Using function Setpath
	//Numene: number of enemies this call of the function spawns
	//X: position along the x axis, where -4 is at the left side and 4 is at the right side
	//Y: position along the y axis, where 4 is at the top and 0 is at the middle
	//Path: See the checker function in Enemy.cs
	//Type: Type of bullet, where 0 is Down, 1 is Cone, 2 is irrelevent, 3 is radial, 4 is sprinkler, 5 is rain
	//Anim: the transform (which has all the animated enemies), see the enemies folder to find the number for each enemy
	//and the manager script for which index in the enemy array holds that given enemy
	
	// ~ LEVEL 1 WAVES ~ //
	void WaveS1(int i){	
		//Wave 01
		if(i == 0){
			Setpath(1,-4,1,1,0,4, false); //4 is "blue" bat, false is blue
			//Boss1();
		}
		if(i == 1){
			Setpath(1,4,2,2,0,4, false);
		}
		if (i == 2){
			Setpath(1,-4,3,3,0,4, false);
		}
		if (i == 3){
			Setpath(1,4,4,4,0,4, false);
		}
		//Wave 02
		if (i == 4){
			Setpath(1,-4,1,1,0,0, true); //0 is red bat, true is red
		}
		if (i == 5){
			Setpath(1,4,2,2,0,0, true);
		}
		if (i == 6){
			Setpath(1,-4,3,3,0,0, true);
		}
		if (i == 7){
			Setpath(1,4,4,4,0,0, true);
		}
		//Wave 03
		if (i == 8){
			Setpath(1,-4,1,5,0,0, true);
			//Boss1();
		}
		if (i == 9){
			Setpath(1,4,2,6,0,0, true);
		}
		if (i == 10){
			Setpath(1,-4,3,7,0,4, false);
		}
		if (i == 11){
			Setpath(1,4,4,8,0,4, false);
		}
		//Wave 04
		if (i == 12){
			Setpath(1,-2,5,9,3,12, false); //12 is blue slime
			Setpath(1,-4,1,1,0,0, true);
			Setpath(1,4,2,2,0,0, true);
		}
		//Wave 05
		if (i == 13){
			Setpath(1,-4,2,10,3,8, true);
			Setpath(1,4,2,11,3,8, true);
		}
		if (i == 14){
			Setpath(1,-4,1,1,0,4, false);
			Setpath(1,4,2,2,0,4, false);
		}
		//Wave 06
		if(i == 15){
			Setpath(1,-4,0,12,1,4, false);
			Setpath(1,4,1,13,0,0, true);
		}
		if (i == 16){
			Setpath(1,-3,2,14,0,0, true);
			Setpath(1,4,3,15,1,4, false);
		}
		//Wave 07
		if (i == 17){
			Setpath(1,-4,4,16,1,0, true);
			Setpath(1,4,4,17,1,0, true);
		}
		if (i == 18){
			Setpath(1,0,4,18,4,12, false);
		}
		//Wave 08
		if(i == 19){
			Setpath(1,-3,2,14,0,4, false);
			Setpath(1,-4,1,1,0,4, false);
			Setpath(1,4,1,13,0,4, false);
		}
		if(i == 20){
			Setpath(1,4,2,2,0,0, true);
			Setpath(1,4,3,15,1,0, true);
		}
		//Wave 09
		if (i == 21){
			Setpath(1,4,2,11,3,4, false);
			Setpath(1,-4,1,5,0,0, true);
			Setpath(1,-4,3,7,0,0, true);
		}
		if (i == 22){
			Setpath(1,4,4,17,1,4, false);
			Setpath(1,4,2,11,3,12, false);
		}
		//Wave 10
		if (i == 23){
			Setpath(1,-2,6,19,1,12, false);
		}
		if (i == 24){
			Setpath(1,-1,6,20,1,8, true);
		}
		if (i == 25){
			Setpath(1,1,6,21,1,12, false);
		}
		if (i == 26){
			Setpath(1,2,6,22,1,8, true);
		}
		//Wave 11
		if(i == 27){
			Setpath(1,-4,2,10,3,12, false);
			Setpath(1,4,4,8,0,4, false);
		}
		if (i == 28){
			Setpath(1,4,4,4,0,0, true);
		}
		if (i == 29){
			Setpath(1,4,2,6,0,0, true);
		}
		if (i == 30){
			Setpath(1,4,2,2,0,0, false);
			Setpath(1,-4,2,10,3,8, true);
		}
		if (i == 31){
			Boss1 ();
		}
		
	}
	
	// ~ LEVEL 2 WAVES ~ //
	void WaveS2(int i){
//		//Wave 01
		if(i == 00){
			Setpath(1,-4,1,1,0,2, true); //2 is brown (red) bat
			Setpath(1,4,3,2,0,2, true);
			//Boss2(50);
		}
		if(i == 01){
			Setpath(1,-4,2,1,0,5, false); //5 is purple (blue) bat
			Setpath(1,4,4,2,0,5, false);
		}
//		//Wave 02
		if (i == 02){
			Setpath(1,-4,2,5,0,5, false); //blue bat on left
			Setpath(1,4,2,11,1,11, true); //11 is brown (red) slime
		}
		if (i == 03){
			Setpath(1,-4,2,5,0,2, true); //red bat on left
		}
		if (i == 04){
			Setpath(1,0,5,20,4,14, false); //14 is purple (blue) slime
		}
//		//Wave 03
		if (i == 05){
			Setpath(1,-4,5,5,0,17, true); //17 is red wisp
		}
		if (i == 06){
			Setpath(1,-4,1,5,0,17, true);
		}
		if (i == 07){
			Setpath(1,-4,2,1,0,17, true);
		}
		if (i == 08){
			Setpath(1,-4,3,1,0,17, true);
		}
//		//Wave 04
		if (i == 09){
			Setpath(1,4,5,7,0,21, false); //21 is blue wisp
		}
		if (i == 10){
			Setpath(1,4,1,7,0,21, false);
		}
		if (i == 11){
			Setpath(1,4,2,2,0,21, false);
		}
		if (i == 12){
			Setpath(1,4,3,2,0,21, false);
		}
		//Wave 05
		if (i == 13){
			Setpath(1,-2,5,9,0,17, true);
		}
		if (i == 14){
			Setpath(1,-2,5,9,0,21, false);
		}
		if (i == 15){
			Setpath(1,-2,5,9,0,17, true);
			Setpath(1,4,4,17,1,5, false); //swooping blue bat
		}
		if (i == 16){
			Setpath(1,-2,5,9,0,21, false);
		}
		if (i == 17){
			Setpath(1,4,4,17,1,2, true); //swooping red bat
		}
		//Wave 06
		if (i == 18){
			Setpath(1,-2,6,19,3,11, true);
		}
		if (i == 19){
			Setpath(1,-4,2,1,1,5, false);
			Setpath(1,-4,2,14,0,21, false);
		}
		if (i == 20){
			Setpath(1,2,6,19,3,11, true);
		}
		if (i == 21){
			Setpath(1,4,2,2,1,5, false);
			Setpath(1,4,2,14,0,21, false);
		}
		//Wave 07
		if (i == 22){
			Setpath(1,4,4,4,0,17, true); //red wisp
		}
		if (i == 23){
			Setpath(1,-4,4,16,3,11, true); //red slime
		}
		if (i == 24){
			Setpath(1,4,4,17,3,14, false); //blue slime
		}
		if (i == 25){
			Setpath(1,4,4,4,0,17, true); //red wisp
		}
		//Wave 08
		if (i == 26){
			Setpath(1,-2,5,9,1,2, true); //red bat
		}
		if (i == 27){
			Setpath(1,-2,5,9,0,17, true); //red wisp
		}
		if (i == 28){
			Setpath(1,-2,5,9,1,2, true); //red bat
		}
		if (i == 29){
			Setpath(1,-2,5,9,0,17, true); //red wisp
			Setpath(1,4,2,11,3,14, false); //blue slime
		}
		//Wave 09
		if (i == 30){
			Setpath(1,-4,3,3,1,11, false); //red slime
			Setpath(1,-3,2,14,0,2, true); //red bat
		}
		if (i == 32){
			Setpath(1,-4,1,1,3,5, false); //blue slime
			Setpath(1,-3,2,14,0,5, false); //red bat
		}
		//Wave 10
		if (i == 32){
			Setpath(1,-1,6,20,4,11, true); //red slime
			Setpath(1,1,6,21,4,14, false); //blue slime
		}
		if (i == 33){
			Setpath(1,4,4,8,0,17, true); //red wisp
			Setpath(1,-4,4,16,0,21, false); //blue wisp
		}
		if (i == 34){
			Setpath(1,4,4,8,0,17, true); //red wisp
			Setpath(1,-4,4,16,0,21, false); //blue wisp
		}
		if (i == 35){
			Setpath(1,4,4,8,0,17, true); //red wisp
			Setpath(1,-4,4,16,0,21, false); //blue wisp
		}
		if (i == 36){
			Setpath(1,4,4,8,0,17, true); //red wisp
			Setpath(1,-4,4,16,0,21, false); //blue wisp
		}
		//Boss 2
		if(i == 37){
			Boss2(200);
		}
		
	}
	
	// ~ LEVEL 3 WAVES ~ //
	void WaveS3(int i){
		//Wave 01
		if (i == 00){
			Setpath(1,-4,1,1,0,7, false); //7 is pink/blue bat

		}
		if (i == 01){
			Setpath(1,4,2,2,4,6, true); //6 is black/red bat
		}
		if (i == 02){
			Setpath(1,-4,3,3,0,6, true); //red bat
		}
		if (i == 03){
			Setpath(1,4,4,4,4,7, false); //blue bat
		}
		//Wave 02
		if (i == 04){
			Setpath(1,-4,1,1,0,15, true); //15 is black/red slime
		}
		if (i == 05){
			Setpath(1,4,2,2,4,16, false); //16 is pink/blue slime
		}
		if (i == 06){
			Setpath(1,-4,4,16,3,7, false); //blue bat
		}
		//Wave 03
		if (i == 07){
			Setpath(1,-4,2,10,3,19, true); //19 is brown/red wisp
			Setpath(1,4,2,11,3,19, true); //red wisp
		}
		if (i == 08){
			Setpath(1,-4,2,10,3,24, false); //24 is purple/blue wisp
			Setpath(1,4,2,11,3,24, false); //blue wisp
		}
		//Wave 04
		if (i == 09){
			Setpath(1,-4,3,7,0,4, false);
			//Setpath(1,-4,1,5,0,0, true);
		}
		if (i == 10){
			Setpath(1,-4,2,10,3,8, true);
			//Setpath(1,-4,2,10,3,12, false);
		}
		if (i == 11){
			Setpath(1,-4,3,7,0,4, false);
		}
		if (i == 12){
			Setpath(1,4,2,11,3,12, false);
			//Setpath(1,4,2,6,0,0, true);
		}
		//Wave 05
		if (i == 13){
			Setpath(1,-4,3,7,0,5, false);
			//Setpath(1,-4,1,5,0,2, true);
		}
		if (i == 14){
			Setpath(1,-4,2,10,3,11, true);
			//Setpath(1,-4,2,10,3,14, false);
		}
		if (i == 15){
			Setpath(1,-4,3,7,0,5, false);
		}
		if (i == 16){
			Setpath(1,4,2,11,3,14, false);
			//Setpath(1,4,2,6,0,2, true);
		}
		//Boss 3
		if (i == 17){
			Boss3();
		}
	}
	
	public void killall(){
		GameObject[] a = GameObject.FindGameObjectsWithTag("EnemyShipA");
		for(int i = a.Length - 1; i > 0; i--){
			Destroy(a[i].gameObject);
		}
		if(GameObject.FindGameObjectWithTag("Boss1") != null){
			Destroy(GameObject.FindGameObjectWithTag("Boss1"));
		}
		if(GameObject.FindGameObjectWithTag("Boss2") != null){
			Destroy(GameObject.FindGameObjectWithTag("Boss2"));
		}
		if(GameObject.FindGameObjectWithTag("Boss3") != null){
			Destroy(GameObject.FindGameObjectWithTag("Boss3"));
		}


	}
	//Boss 1 randomly picks from predetermined groups of slimes to summon
	public void RandomSummon(){
		int random = Random.Range (0, 2);
		int tempRand;
		
		// (1) Four slimes move sideways
		if (random == 0) {
			tempRand = Random.Range (0, 2);
			if (tempRand == 0) {
				Setpath(1,0,4,1,0,8, true); //red slime, right
				Setpath(1,0,4,3,0,12, false); //blue slime, right
				Setpath(1,0,4,2,0,12, false); //blue slime, left
				Setpath(1,0,4,4,0,8, true); //red slime, left
			}
			else {
				Setpath(1,0,4,1,0,12, false); //blue slime, right
				Setpath(1,0,4,3,0,8, true); //red slime, right
				Setpath(1,0,4,2,0,8, true); //red slime, left
				Setpath(1,0,4,4,0,12, false); //blue slime, left
			}			
		}
		// (2) Two slimes
		else if(random == 1){
			tempRand = Random.Range (0, 2);
			if (tempRand == 0) {
				Setpath(1,-2,5,9,3,12, false); //blue
				Setpath(1,4,2,11,3,8, true); //red
			}
			else {
				Setpath(1,-2,5,9,3,8, true); //red
				Setpath(1,4,2,11,3,12, false); //blue
			}
		}
		// (3) --
		else if(random == 2){
			//Down(2,Random.Range (-3, 3),Random.Range (0, 5));
		}
		else {
			Setpath(1, Random.Range (-3, 3), Random.Range (-2, 2), Random.Range (1, 5), Random.Range (0, 3), 12, false);
			Setpath(1, Random.Range (-3, 3), Random.Range (-2, 2), Random.Range (1, 5), Random.Range (0, 3), 8, true);
		}

	}
	
}
