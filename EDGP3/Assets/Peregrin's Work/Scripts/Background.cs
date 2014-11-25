using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float scrollSpeed;
	public int mode;
	public Sprite theSprite;
	public bool bossKilled = false;
	
	public Sprite background1;
	public Sprite background1b;
	public Sprite background2;
	public Sprite background2b;
	
	private GameObject manager;
	private GameObject cam;
	private Transform background;
	
	private bool haveReplicated = false;
	
	private float time;
	private float time1 = 2f;
	private float time2 = 300f;
	private float time3 = 600f;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("Manager");
		time = manager.GetComponent<spawn>().time;
		
		transform.localScale = new Vector3(1.6f, 1.6f);
		GetComponent<SpriteRenderer>().sortingOrder = -1;
		
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		mode = manager.GetComponent<spawn>().stage;
					
		if (GetComponent<SpriteRenderer>().sprite != background1b && GetComponent<SpriteRenderer>().sprite != background2b)
		{
			transform.Translate (Vector3.down*Time.deltaTime*scrollSpeed);
			Transform bg;
			if (transform.position.y < -1.36f && haveReplicated == false)
			{
//				Debug.Log (time1+" "+manager.GetComponent<spawn>().time);
				if (mode == 0 && Time.time - manager.GetComponent<spawn>().time < time1)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background1;
				}
				if (mode == 0 && Time.time - manager.GetComponent<spawn>().time > time1)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background1b;
				}
				if (mode == 1 && Time.time - manager.GetComponent<spawn>().time < time2)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background2;
				}
				if (mode == 1 && Time.time - manager.GetComponent<spawn>().time > time2)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background2b;
				}
				haveReplicated = true;
			}
		}
		else
		{
			if (transform.position.y > 1.36f)
			{
				transform.Translate (Vector3.down*Time.deltaTime*scrollSpeed);
			}
			if (bossKilled == true)
			{
				if (GetComponent<SpriteRenderer>().sprite == background1b)
				{
					GetComponent<SpriteRenderer>().sprite = background2;
					bossKilled = false;
				}
			}
			
		}
		if (!renderer.isVisible) Destroy (gameObject);


	}
}
