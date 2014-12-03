using UnityEngine;
using System.Collections;

public class Boxy2 : MonoBehaviour {

	// Use this for initialization
	void OnTriggerExit(Collider other){
		
		Destroy (other.gameObject);
		
	} 

}
