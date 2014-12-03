using UnityEngine;
using System.Collections;

public class Wings : MonoBehaviour {
	public float arccd,icd;
	float time;
	public bool on;
	public bool check;
	public Sprite warning;
	Animator anim;
	float timea;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		check = true;
		renderer.enabled = false;
		on = false;
		arccd = Random.value*1.5f+3f;
		time = Time.time;
		anim.SetBool("Freeze",true);

	}
	
	// Update is called once per frame
	void Update () {
		if(on == false){

			anim.SetBool("Freeze",true);
			GetComponent<SpriteRenderer>().sprite = warning;

			renderer.enabled = false;
			timea = Time.time;

		}
		if(on == false && time <= Time.time - (2f)){
			Debug.Log("check");
			renderer.enabled = true;
			
		}
		if(on == false && time <= Time.time - arccd){
			check = false;
			anim.SetBool("Freeze",false);
			on = true;
			time = Time.time;
			renderer.enabled = true;

		}
		if(on == true && time <= Time.time - arccd){
			on = false;
			time = Time.time;
			//renderer.enabled = false;
			check = true;
		}
	}
}
