using UnityEngine;
using System.Collections;

public class PKShield : MonoBehaviour {
	
	public GUIStyle style;
	public Sprite A;
	public Sprite B;
	
	Transform parent;

	// Use this for initialization
	void Start () {
		GameObject option1 = GameObject.FindGameObjectWithTag("Player1");
		GameObject option2 = GameObject.FindGameObjectWithTag("Player2");
		if (option1.GetComponent<PlayerStats>().role == "Defender")
		{
			transform.parent = option1.transform;
			transform.position = new Vector3(transform.parent.renderer.bounds.center.x, transform.parent.renderer.bounds.center.y + transform.parent.renderer.bounds.extents.y + transform.renderer.bounds.extents.y);	
		}
		else
		{
			transform.parent = option2.transform;
			transform.position = new Vector3(transform.parent.renderer.bounds.center.x, transform.parent.renderer.bounds.center.y + transform.parent.renderer.bounds.extents.y + transform.renderer.bounds.extents.y);	
		}
	}
	
	void OnGUI()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 max = new Vector3(Screen.width, Screen.height);
		GUI.Box (new Rect(pos.x, max.y - pos.y, 50, 50), transform.parent.GetComponent<PlayerStats>().affinity.ToString(), style);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.parent.renderer.bounds.center.x, transform.parent.renderer.bounds.center.y + transform.parent.renderer.bounds.extents.y + transform.renderer.bounds.extents.y);
		Vector3 scale = new Vector3(.001f, .001f);
		Vector3 finalScale = new Vector3(1.01f, 1.01f);
		if (transform.localScale.x > finalScale.x && transform.localScale.y > finalScale.y) transform.localScale -= scale;
		
		GameObject[] bulletA = GameObject.FindGameObjectsWithTag ("BulletA");
		GameObject[] bulletB = GameObject.FindGameObjectsWithTag ("BulletB");
		
		for (int i = 0; i < bulletA.Length; i++)
		{
			if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && transform.parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				Destroy (bulletA[i].gameObject);
			}
			else if (bulletA[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && transform.parent.GetComponent<PlayerStats>().affinity == 'A')
			{
				transform.parent.GetComponent<PlayerStats>().ammo += 1;
				Destroy (bulletA[i].gameObject);
			}
		}
		for (int i = 0; i < bulletB.Length; i++)
		{
			if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && transform.parent.GetComponent<PlayerStats>().affinity == 'B')
			{
				transform.parent.GetComponent<PlayerStats>().ammo += 1;
				Destroy(bulletB[i].gameObject);
			}
			else if (bulletB[i].renderer.bounds.Intersects(gameObject.renderer.bounds) && transform.parent.GetComponent<PlayerStats>().affinity == 'A')
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
