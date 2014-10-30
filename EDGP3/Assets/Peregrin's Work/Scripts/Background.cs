using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float scrollSpeed;
	private bool haveReplicated = false;
	public int mode;
	
	private Transform background;

	// Use this for initialization
	void Start ()
	{
		GameObject cam = GameObject.Find ("Main Camera");
		mode = cam.GetComponent<GUIManager>().gameMode;
		if (mode == 0) transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background1;
		if (mode == 1) transform.GetComponent<SpriteRenderer>().sprite = cam.GetComponent<GUIManager>().Background1B;
		
		transform.localScale = new Vector3(1.6f, 1.6f);
		GetComponent<SpriteRenderer>().sortingOrder = -1;
	}
	
	// Update is called once per frame
	void Update ()
	{

		
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
		if (mode == 1 || mode == 3 || mode == 5)
		{
			if (transform.position.y > 1.36f) transform.Translate (Vector3.down*Time.deltaTime*scrollSpeed);
		}
	}
}
