using UnityEngine;
using System.Collections;

public class ice : MonoBehaviour {
	public Sprite spritea;
	public Sprite spriteb;
	public Sprite spritec;

	// Use this for initialization
	void Start () {
		int rand = Random.Range(0,3);
		if(rand == 0){
			GetComponent<SpriteRenderer>().sprite = spritea;
		}
		if(rand == 1){
			GetComponent<SpriteRenderer>().sprite = spriteb;
		}
		if(rand == 2){
			GetComponent<SpriteRenderer>().sprite = spritec;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) {
		if(other.tag == "BulletC")
			Destroy(other.gameObject);
	}
}
