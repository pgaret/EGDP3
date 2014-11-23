using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float scrollSpeed;
	private bool haveReplicated = false;
	public int mode;
	
	private GameObject manager;
	private GameObject cam;
	private Transform background;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("Manager");
		cam = GameObject.Find ("Main Camera");
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		mode = manager.GetComponent<spawn>().stage;
		if (mode == 0 && GameObject.Find ("Boss1(Clone)") == null) transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background1;
		else if (mode == 0 && GameObject.Find ("Boss1(Clone") != null)
		{
			transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background1B;
			GameObject[] bgs  = GameObject.FindGameObjectsWithTag("Background");
			foreach (GameObject bg in bgs) bg.GetComponent<Background>().scrollSpeed = bg.GetComponent<Background>().scrollSpeed* 2;
		}
		else if (mode == 1 && GameObject.Find ("Boss2(Clone") == null)
		{
			GameObject[] bgs  = GameObject.FindGameObjectsWithTag("Background");
			foreach (GameObject bg in bgs) bg.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background2;
			transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background2;
			
		}
		else if (mode == 1 && GameObject.Find ("Boss1(Clone") == null)
		{
			transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background2B;
			GameObject[] bgs  = GameObject.FindGameObjectsWithTag("Background");
			foreach (GameObject bg in bgs) bg.GetComponent<Background>().scrollSpeed = bg.GetComponent<Background>().scrollSpeed* 2;
		}
		transform.localScale = new Vector3(1.6f, 1.6f);
		GetComponent<SpriteRenderer>().sortingOrder = -1;
		if (GameObject.Find("Boss1(Clone") == null && GameObject.Find ("Boss2(Clone") == null && GameObject.Find ("Boss3Clone"))
		{
			transform.Translate (Vector3.down*Time.deltaTime*scrollSpeed);
			
			if (transform.position.y < -1.36f && haveReplicated == false)
			{
				Instantiate (transform, new Vector3(0, 11.4f), Quaternion.identity);
				haveReplicated = true;
			}
			if (!renderer.isVisible) Destroy (gameObject);

		}
		else
		{
			if (transform.position.y > 1.36f) transform.Translate (Vector3.down*Time.deltaTime*scrollSpeed);
		}
	}
}
