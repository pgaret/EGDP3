using UnityEngine;
using System.Collections;

public class DeadEnemy : MonoBehaviour {

	public float cd;
	public AnimationClip[] explosion;
	public int type;
	
	Animator anim;
	bool hasAnim = false;
	float timer;

	// Use this for initialization
	void Start () 
	{
		timer = Time.time + cd;
		GetComponent<Animator>().speed = .75f;
		
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (type != -1 && hasAnim == false)
		{
			//Yellow
			if (type == 01 || type == 09 || type == 18) anim.SetInteger("type", 8);
			//Brown
			if (type == 02 || type == 11 || type == 19) anim.SetInteger("type", 2);
			//Green
			if (type == 03 || type == 10 || type == 20) anim.SetInteger("type", 3);
			//Blue
			if (type == 04 || type == 12 || type == 21) anim.SetInteger("type", 1);
			//Purple
			if (type == 05 || type == 14 || type == 23) anim.SetInteger("type", 7);
			//Dark Grey
			if (type == 06 || type == 15 || type == 24) anim.SetInteger("type", 4);
			//Pink
			if (type == 07 || type == 16 || type == 25) anim.SetInteger("type", 6);
			//Metallic
			if (type == 13 || type == 22 || type == 22) anim.SetInteger("type", 5);
						
			hasAnim = true;
		}
		if (Time.time > timer) Destroy (gameObject);
		
		
	}
}
