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
			//Blue
			if (type == 5 || type == 13 || type == 22) anim.SetInteger("type", 1);
			//Brown
			if (type == 3 || type == 12 || type == 20) anim.SetInteger("type", 2);
			//Green
			if (type == 4 || type == 11 || type == 21) anim.SetInteger("type",3);
			//Grey
			if (type == 7 || type == 14 || type == 23) anim.SetInteger("type", 4);
			//Metallic
			if (type == 16 || type == 25 || type == 25) anim.SetInteger("type", 5);
			//Pink
			if (type == 8 || type == 17 || type == 27) anim.SetInteger("type", 6);
			//Purple
			if (type == 6 || type == 15 || type == 24) anim.SetInteger("type", 7);
			//Yellow
			if (type == 2 || type == 10 || type == 19) anim.SetInteger("type", 8);
			hasAnim = true;
		}
		if (Time.time > timer) Destroy (gameObject);
		
		
	}
}
