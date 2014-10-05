using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO; 


public class spawn : MonoBehaviour {
	public GameObject enemy;
	int x = 10;
	int y = 10;
	// Use this for initialization
	void Start () {
		Diagonal(11,1);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Diagonal(int numene,int dir){

		for(int i = numene; i > 6; i--){
			GameObject test;
			if(dir == 0)
				test = Instantiate(enemy, new Vector3(i, i, 0), Quaternion.identity) as GameObject;
			else
				test = Instantiate(enemy, new Vector3(-i, i, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().changeloc(new Vector3(i, -i, 0));
		}
	}
	
}
