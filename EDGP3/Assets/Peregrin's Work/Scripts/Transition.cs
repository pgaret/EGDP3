using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	public Sprite[] slides;
	public float timing;
	float timer;
	int counter = 1;

	// Use this for initialization
	void Start ()
	{
		timer = Time.time + timing;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > timer)
		{
			if (counter < slides.Length)
			{
				GetComponent<SpriteRenderer>().sprite = slides[counter];
				counter++;
				timer = Time.time + timing;
			}
			else
			{
				GetComponent<SpriteRenderer>().sprite = slides[0];
				Destroy (gameObject);
				GameObject manager = GameObject.Find("Manager");
				manager.GetComponent<spawn>().stage = 1;
				manager.GetComponent<spawn>().summon = true;
			}
		}
	}
}
