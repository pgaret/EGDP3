using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	bool onTheMove = false;
	Vector3 destination;
	Transform whoHitMe;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!onTheMove) transform.Translate(Vector3.down*Time.deltaTime);
		if (onTheMove)
		{
			transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime*5);
			if (transform.position == destination)
			{
				Destroy (gameObject);
				whoHitMe.GetComponent<PlayerStats>().GotCoin();
			}
		}
		if (!renderer.isVisible) Destroy(gameObject);
	}
	
	public void MoveTowards(Vector3 goTo, Transform hitMe)
	{
		onTheMove = true;
		destination = goTo;
		transform.tag = "Untagged";
		whoHitMe = hitMe;
	}
}
