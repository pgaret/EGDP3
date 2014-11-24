using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float scrollSpeed;
	public int mode;
	public Sprite theSprite;
	
	private GameObject manager;
	private GameObject cam;
	private Transform background;
	
	private bool haveReplicated = false;
	
	private float time;
	private float time1 = 1;
	private float time2 = 300f;
	private float time3 = 600f;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("Manager");
		cam = GameObject.Find ("Main Camera");
		time = manager.GetComponent<spawn>().time;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
/*		if (Time.time - time >= time1)
		{
			mode = 1;
			theSprite = cam.GetComponent<GUIManager>().Background1B;
		}
		if (Time.time - time >= time2)
		{
			mode = 2;
		}
		if (Time.time - time >= time3)
		{
			mode = 3;
		}
		if (mode == 0 && GameObject.Find ("Boss1(Clone)") == null)
		{
			transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background1;
		}
		else if (mode == 1)
		{
			GameObject[] bgs  = GameObject.FindGameObjectsWithTag("Background");
			foreach (GameObject bg in bgs) bg.GetComponent<Background>().scrollSpeed = bg.GetComponent<Background>().scrollSpeed * 2;
		}
		else if (mode == 2)
		{
			transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background2;
			
		}
		else if (mode == 3)
		{
			transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background2B;
			GameObject[] bgs  = GameObject.FindGameObjectsWithTag("Background");
			foreach (GameObject bg in bgs) bg.GetComponent<Background>().scrollSpeed = bg.GetComponent<Background>().scrollSpeed * 4;
		}
		transform.localScale = new Vector3(1.6f, 1.6f);
		GetComponent<SpriteRenderer>().sortingOrder = -1;
		if (mode == 0 || mode == 2 || mode == 4)
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
			if (transform.position.y > 1.36f) transform.Translate (Vector3.down*Time.deltaTime*scrollSpeed * 2);
		}
*/	}
}
