using UnityEngine;
using System.Collections;

public class PKShield : MonoBehaviour {
	
	public GUIStyle style;
	
	Transform parent;

	// Use this for initialization
	void Start () {
		GameObject[] knights = GameObject.FindGameObjectsWithTag("PunchKnight");
		for (int i = 0; i < knights.Length; i++)
		{
			if (knights[i].transform.parent.GetComponent<PlayerStats>().role == "Defender")
			{
				parent = knights[i].transform.parent;
				transform.parent = parent;
				transform.position = parent.position;
			}
		}
	
	}
	
	void OnGUI()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 max = new Vector3(Screen.width, Screen.height);
		GUI.Box (new Rect(pos.x, max.y - pos.y, 50, 50), parent.GetComponent<PlayerStats>().affinity.ToString(), style);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
