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
		for(int i = 10; i > 5; i--){

			GameObject test = Instantiate(enemy, new Vector3(i, i, 0), Quaternion.identity) as GameObject;
			test.GetComponent<Enemy>().changeloc(new Vector3(i, i, 0));
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
