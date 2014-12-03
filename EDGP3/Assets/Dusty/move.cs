using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	public GameObject master;
	// Use this for initialization
	void Start () {
		master = GameObject.FindGameObjectWithTag("fog");
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer>().sprite = master.GetComponent<SpriteRenderer>().sprite;
	}
}
