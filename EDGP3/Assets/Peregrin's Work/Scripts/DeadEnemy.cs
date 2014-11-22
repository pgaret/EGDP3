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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (type != -1 && hasAnim == false)
		{
//			if (type == 4 || type == 12 || type == 21) anim.SetInteger("type", 0);
//			if (type == 2 || type == 11 || type == 19) anim.SetInteger("type", 0);
//			if (type == 3 || type == 10 || type == 20) anim.SetInteger("type", 1);
//			if (type == 6 || type == 13 || type == 22) anim.SetInteger("type", 2);
//			if (type == 15 || type == 24 || type == 24) anim.SetInteger("type", 3);
//			if (type == 7 || type == 16 || type == 25) anim.SetInteger("type", 4);
//			if (type == 5 || type == 14 || type == 23) anim.SetInteger("type", 5);
//			if (type == 0 || type == 8 || type == 17) anim.SetInteger("type", 6);
//			if (type == 1 || type == 9 || type == 18) anim.SetInteger("type", 7);
//			hasAnim = true;
		}
		if (Time.time > timer) Destroy (gameObject);
		
		
	}
}
