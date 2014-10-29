using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int ammo;
	public int health;
	public int lives;
	public char affinity;
	public string role;
	public string swapRole;
	public float swapTimer = 2f;
	public float swapCD = .5f;

	// Use this for initialization
	void Start () {
		swapRole = "no";
		affinity = 'A';
	
	}
	
	public void Swap()
	{
		swapRole = "no";
		if (role == "Attacker") role = "Defender";
		else role = "Attacker";
		if (transform.name == "PunchKnight(Clone)")
		{
			if (role != "Defender")Destroy(transform.FindChild("Shield(Clone)").gameObject);
			if (role == "Defender")transform.GetComponent<PunchKnight>().Shield();

		}
		if (transform.name == "DragonTamer(Clone)")
		{
			if (role != "Defender")
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					if (transform.GetChild(i).name == "Shield(Clone") Destroy (transform.GetChild(i).gameObject);
				}
			}
			if (role == "Defender")transform.GetComponent<DragonTamer>().Shield();
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
