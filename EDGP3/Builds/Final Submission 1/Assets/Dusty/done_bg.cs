﻿using UnityEngine;
using System.Collections;

public class done_bg : MonoBehaviour {

	// Use this for initialization
	public float scrollSpeed;
	public float tileSizeZ;
	
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
	}
	
	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.right * newPosition;
	}
}
