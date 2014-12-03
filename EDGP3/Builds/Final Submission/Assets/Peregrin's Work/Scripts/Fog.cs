using UnityEngine;
using System.Collections;

public class Fog : MonoBehaviour {

	public Sprite starterSprite;
	public Transform fogBlock;

	bool hasReplicated = false;
	Vector3 relative;

	// Use this for initialization
	void Start ()
	{
//		Vector3 position = transform.position;
		for (int j = -1; j < 2; j++)
		{
			for (int k = -1; k < 2; k++)
			{
				Transform block = (Transform)Instantiate (fogBlock, new Vector3(transform.position.x + j*transform.collider2D.bounds.extents.x, transform.position.y + k*collider2D.bounds.extents.y/1.5f), Quaternion.identity);
				block.parent = transform;
			}
		}
	
		for (int i = 0; i < transform.childCount; i++)
		{
			Animator anim = transform.GetChild(i).GetComponent<Animator>();
			anim.speed = .1f;
		}
		
		relative = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width, 0));
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		transform.Translate(Vector3.right*Time.deltaTime);
		if (transform.position.x - transform.collider2D.bounds.extents.x >= relative.x / 4 && hasReplicated == false && transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == starterSprite)
		{
			Instantiate(transform, new Vector3(transform.position.x - 2*transform.collider2D.bounds.extents.x - .03f, transform.position.y), Quaternion.identity);
			hasReplicated = true;
		}
//		if (transform.position.x - transform.collider2D.bounds.extents.x*2 > relative.x) Destroy (gameObject);
//		Debug.Log (transform.position.x+"   "+transform.collider2D.bounds.extents.x+"   "+(relative.x/4));
		
	}
}
