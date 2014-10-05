using UnityEngine;
using System.Collections;

public class Boxy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "EnemyShipA"){
			other.gameObject.GetComponent<Enemy>().fireset(true);
		}
	}
	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "EnemyShipA"){
			other.gameObject.GetComponent<Enemy>().fireset(false);
		}
	} 
}
