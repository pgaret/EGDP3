using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public float scrollSpeed;
	
	private bool haveReplicated = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float height = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;
		transform.Translate (Vector3.up*Time.deltaTime*scrollSpeed);
		
		if (transform.position.y - renderer.bounds.extents.y > -height && haveReplicated == false)
		{
			Instantiate (transform, new Vector3(0, -11), Quaternion.identity);
			haveReplicated = true;
		}
		if (!renderer.isVisible) Destroy (gameObject);
	}
}
