using UnityEngine;
using System.Collections;

public class Wings : MonoBehaviour {
	public float arccd;
	float time;
	bool on;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
		on = false;
		arccd = Random.value*5f+2f;
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(on == false){
			renderer.enabled = false;

		}
		if(on == false && time <= Time.time - arccd){
			on = true;
			time = Time.time;
			renderer.enabled = true;

		}
		if(on == true && time <= Time.time - arccd){
			on = false;
			time = Time.time;
			renderer.enabled = false;

		}
	}
}
