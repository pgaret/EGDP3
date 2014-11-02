using UnityEngine;
using System.Collections;

public class DTShield : MonoBehaviour {

	public Sprite A;
	public Sprite B;

	GameObject parent;
	
	public GUIStyle style;

	// Use this for initialization
	void Start ()
	{
		GameObject option1 = GameObject.FindGameObjectWithTag("Player1");
		GameObject option2 = GameObject.FindGameObjectWithTag("Player2");
		if (option1.GetComponent<PlayerStats>().role == "Defender" && option1.name == "DragonTamer(Clone)") parent = option1;
		Debug.Log (option1.GetComponent<PlayerStats>().role + " " + option1.name);
		if (option2.GetComponent<PlayerStats>().role == "Defender" && option2.name == "DragonTamer(Clone)") parent = option2;
		Debug.Log (option2.GetComponent<PlayerStats>().role + " " + option2.name);
		Debug.Log (parent.name);
		GameObject[] dragons = GameObject.FindGameObjectsWithTag("Dragon");
		for (int i = 0; i < dragons.Length; i++)
		{
			if (dragons[i].transform.childCount == 0)
			{
				transform.parent = dragons[i].transform;
				transform.position = new Vector3(transform.parent.renderer.bounds.center.x, transform.parent.renderer.bounds.center.y + transform.parent.renderer.bounds.extents.y + transform.renderer.bounds.extents.y);	
				i = dragons.Length;
			}
		}
	}
	
//	void OnGUI()
//	{
//		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
//		Vector3 max = new Vector3(Screen.width, Screen.height);
//		GUI.Box (new Rect(pos.x, max.y - pos.y, 50, 50), parent.GetComponent<PlayerStats>().affinity.ToString(), style);
//	}
	
	// Update is called once per frame
	void Update ()
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
				parent.GetComponent<PlayerStats>().ammo += 1;
				Destroy (bulletA[i].gameObject);
			}
		}
		for (int i = 0; i < bulletB.Length; i++)
		{
			if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				parent.GetComponent<PlayerStats>().ammo += 1;
				Destroy(bulletB[i].gameObject);
			}
			else if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				Destroy(bulletB[i].gameObject);
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			if (parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				parent.GetComponent<PlayerStats>().affinity = 'B';
				GetComponent<SpriteRenderer>().sprite = B;
			}
			else
			{
				parent.GetComponent<PlayerStats>().affinity = 'A';
				GetComponent<SpriteRenderer>().sprite = A;
			}
		}
	}
}
