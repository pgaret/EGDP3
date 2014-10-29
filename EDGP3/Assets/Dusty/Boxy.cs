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
		if(other.gameObject.tag == "Boss2"){
			GameObject a = GameObject.FindGameObjectWithTag("Manager");
			print ("Something");
			a.GetComponent<spawn>().Boss2(other.GetComponent<Boss2>().health);
		}
		Destroy (other.gameObject);

	} 
	void OnCollisionExit(Collision other) {
		if(other.gameObject.tag == "Boss2"){
			GameObject a = GameObject.FindGameObjectWithTag("Manager");
			print ("Something");
			a.GetComponent<spawn>().Boss2(other.gameObject.GetComponent<Boss2>().health);
		}
		Destroy (other.gameObject);}



}
