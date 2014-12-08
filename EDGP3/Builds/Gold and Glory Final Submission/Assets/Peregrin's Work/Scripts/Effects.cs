using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	
	LineRenderer lr;

	// Use this for initialization
	void Start ()
	{
		Vector3 pos;
		pos = transform.position;
//		pos.z = 1;
		transform.position = pos;
		renderer.sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindGameObjectWithTag("Player1") && player1 == null)
		{
			player1 = GameObject.FindGameObjectWithTag("Player1");
			player2 = GameObject.FindGameObjectWithTag("Player2");
			lr = gameObject.AddComponent<LineRenderer>();
			lr.sortingLayerName = "Foreground";
		}
		if (player1 != null)
		{
			Vector3 pos1 = Camera.main.WorldToScreenPoint(player1.transform.position);
			Vector3 pos2 = Camera.main.WorldToScreenPoint(player2.transform.position);
			pos1.z = 0;
			pos2.z = 0;
			Debug.Log (pos1 + " " + pos2);
			lr.SetPosition(0, player1.transform.position);
			lr.SetPosition(1, player2.transform.position);
			lr.SetWidth(.1f, .1f);
			lr.useWorldSpace = false;
			lr.transform.position = new Vector3(transform.position.x, transform.position.y, -.1f);
		}
		
	}
}
