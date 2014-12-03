using UnityEngine;
using System.Collections;

public class MMShield : MonoBehaviour {

	GameObject myParent;

	// Use this for initialization
	void Start ()
	{
		GameObject option1 = GameObject.FindGameObjectWithTag("Player1");
		if (option1.GetComponent<PlayerStats>().role == "Defender") transform.parent = option1.transform;
		else transform.parent = GameObject.FindGameObjectWithTag("Player2").transform;
		
//		Debug.Log (transform.parent.name);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
