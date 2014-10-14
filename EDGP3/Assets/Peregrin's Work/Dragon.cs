using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour {

	Transform parent;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag("Player1").name == "DragonTamer(Clone)" && GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStats>().role == "Attacker") transform.parent = GameObject.FindGameObjectWithTag("Player1").transform;
		else transform.parent = GameObject.FindGameObjectWithTag("Player2").transform;
		Debug.Log (transform.parent);
	
	}
	
	void BulletCheck()
	{
		GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
		GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
		
		for (int i = 0; i < bulletA.Length; i++)
		{
			if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				Destroy (bulletA[i].gameObject);
			}
			else if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				parent.GetComponent<DragonTamer>().ammo += 1;
				Destroy (bulletA[i].gameObject);
			}
		}
		for (int i = 0; i < bulletB.Length; i++)
		{
			if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				parent.GetComponent<DragonTamer>().ammo += 1;
				Destroy(bulletB[i].gameObject);
			}
			else if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				Destroy(bulletB[i].gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.parent);
	
	}
}
