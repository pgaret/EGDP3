using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float scrollSpeed;
	public int mode;
	public bool needSwap = false;
	public Sprite theSprite;
	public bool bossKilled = false;
	
	public Sprite background1;
	public Sprite background1b;
	public Sprite background2;
	public Sprite background2b;
	public Sprite background3;
	public Sprite background3a;
	public Sprite background3b;
	public Sprite background3c;
	
	private GameObject manager;
	private GameObject cam;
	private Transform background;
	
	private bool haveReplicated = false;
	
	private float time;
	private float time1 = 120f;
	private float time2 = 84f;
	private float time3 = 600f;

	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("Manager");
		time = manager.GetComponent<spawn>().time;
		
		transform.localScale = new Vector3(1.6f, 1.6f);
		GetComponent<SpriteRenderer>().sortingOrder = -1;
		
		mode = manager.GetComponent<spawn>().stage;
		
		if (mode == 0 && Time.time - time >= time1) mode = 4;
		if (mode == 1 && Time.time - time >= time2) mode = 5;
		if (mode == 2 && Time.time - time >= time3) mode = 6;
		
		
	
	}
	
	// Update is called once per frame
	void Update ()
	{
			
		if (needSwap == true)
		{
			Debug.Log (mode);
			if (mode == 1) GetComponent<SpriteRenderer>().sprite = background2;
			else if (mode == 2) GetComponent<SpriteRenderer>().sprite = background3;
			needSwap = false;
		}
		
		Transform bg;
		if (mode != 4 && mode != 5 && mode != 6)
		{
			if (transform.position.y < -1.36f && haveReplicated == false)
			{
				if (mode == 0)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f, 1), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background1;
				}
				else if (mode == 1)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f, 1), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background2;
				}
				else if (mode == 2)
				{
					bg = (Transform)Instantiate (transform, new Vector3(0, 11.4f, 1), Quaternion.identity);
					bg.GetComponent<SpriteRenderer>().sprite = background3;
				}
				haveReplicated = true;
			}
			transform.Translate(Vector3.down*Time.deltaTime*scrollSpeed);
		}
		else
		{
			if (mode == 4)
			{
				GetComponent<SpriteRenderer>().sprite = background1b;
			}
			else if (mode == 5)
			{
				GetComponent<SpriteRenderer>().sprite = background2b;
			}
			else if (mode == 6)
			{
				GetComponent<SpriteRenderer>().sprite = background3b;
			}
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
