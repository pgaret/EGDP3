using UnityEngine;
using System.Collections;

public class DTShield : MonoBehaviour {


	// Use this for initialization
	void Start ()
	{
		GameObject option1 = GameObject.FindGameObjectWithTag("Player1");
		GameObject option2 = GameObject.FindGameObjectWithTag("Player2");
		
		if (option1.GetComponent<PlayerStats>().role == "Defender")
		{
			for (int i = 0; i < option1.transform.childCount; i++)
			{
				if (option1.transform.GetChild(i).transform.childCount < 1)
				{
					transform.parent = option1.transform.GetChild(i);
					i = option1.transform.childCount;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
