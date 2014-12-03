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
		
// 		Debug.Log (transform.parent.name);
	}
	
	// Update is called once per frame
	void Update ()
	{
		char affinity = transform.parent.GetComponent<PlayerStats>().affinity;
		if (affinity == 'A') GetComponent<Animator>().SetBool("typea", true);
		else GetComponent<Animator>().SetBool("typea", false);
		
		if (affinity == 'A')
		{
			GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletA");
			foreach (GameObject bullet in bullets)
			{
				if (bullet.collider.bounds.Intersects(renderer.bounds))
				{
					transform.parent.GetComponent<PlayerStats>().ammo += 1;
					Destroy (bullet);
				}
				
			}
		}
		else
		{
			GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletB");
			foreach (GameObject bullet in bullets)
			{
				if (bullet.collider.bounds.Intersects(renderer.bounds))
				{
					transform.parent.GetComponent<PlayerStats>().ammo += 1;
					Destroy(bullet);
				}
				
			}
		}

	}
}
