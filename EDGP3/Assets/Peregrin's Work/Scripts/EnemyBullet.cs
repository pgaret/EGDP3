﻿using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public Vector2 clickedPoint;
	public int bullettype;
	public int bulletspeed;
	private Vector3 startPos;
	float timer;
	float fireTime;
	float Y;
	// Use this for initialization
	void Start () {
		fireTime = Time.time;
		startPos = transform.position;
		timer = Time.time;


	}
	
	// Update is called once per frame
	void Update () {

		if(bullettype != 4 ){
			rigidbody.AddForce(-transform.up * bulletspeed);
			rigidbody.velocity = Vector3.zero;
		}
		else if (bullettype == 4){
			if(timer + .2f >= Time.time){
				rigidbody.AddForce(transform.up * bulletspeed);

			}else{
				rigidbody.AddForce(-transform.up * bulletspeed);

			}
			rigidbody.velocity = Vector3.zero;
		}

		
	}
	public void setatktype(int a){
		bullettype = a;

	}


}