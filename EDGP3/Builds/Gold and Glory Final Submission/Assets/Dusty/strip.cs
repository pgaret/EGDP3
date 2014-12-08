﻿using UnityEngine;
using System.Collections;

public class strip : MonoBehaviour {

	// Use this for initialization
	public float scrollSpeed;
	public float tileSizeZ;
	
	private Vector2 savedOffset;
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
		savedOffset = renderer.sharedMaterial.GetTextureOffset ("_MainTex");
	}
	
	void Update ()
	{
		float x = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ * 4);
		x = x / tileSizeZ;
		x = Mathf.Floor (x);
		x = x / 4;
		Vector2 offset = new Vector2 (x, savedOffset.y);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.back * newPosition;
	}
	
	void OnDisable () {
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}
}