using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	public Sprite[] slides;
	public float timing;
	float timer;
	int counter = 1;
	public Texture a;


	public bool end = false;

	GameObject manager; 
	// Use this fr initialization
	void Start ()
	{
		manager	= GameObject.Find("Manager");

		timer = Time.time + timing;
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if(end){
			if(Input.anyKey){
				Application.LoadLevel ("Peregrin's Scene");
			}
		}
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
				manager.GetComponent<spawn>().stage += 1;
				GameObject.FindGameObjectWithTag("Background").GetComponent<Background>().mode -= 3;
				GameObject.FindGameObjectWithTag("Background").GetComponent<Background>().needSwap = true;
				if(manager.GetComponent<spawn>().stage <3){
					manager.GetComponent<spawn>().summon = true;
					Destroy (gameObject);
					manager.GetComponent<spawn>().killall();
					GameObject.FindGameObjectWithTag("Background").GetComponent<Background>().bossKilled = true;
					manager.GetComponent<spawn>().i = 0;
				}
				if(manager.GetComponent<spawn>().stage >= 3){
					end = true;
					//Debug.Log(end);
				}
					

			}
		}

	}
	void OnGUI()
	{

		if(end == true){
			GUI.DrawTexture(new Rect(0, 0, 435, 326), a,ScaleMode.StretchToFill);
			//Debug.Log("run");
		}

	}
}
