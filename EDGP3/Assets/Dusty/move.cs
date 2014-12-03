using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	GameObject master;
	GameObject mang;
	// Use this for initialization
	void Start () {
		master = GameObject.FindGameObjectWithTag("fog");
		mang = GameObject.FindGameObjectWithTag("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		if(mang.GetComponent<spawn>().stage == 1){
			renderer.enabled = true;
		}else{
			renderer.enabled = false;
		}
		GetComponent<SpriteRenderer>().sprite = master.GetComponent<SpriteRenderer>().sprite;
	}
}
