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
				GameObject[] dragons = GameObject.FindGameObjectsWithTag("Dragon");
				for (int i = 0; i < dragons.Length; i++)
				{
					for (int j = 0; j < dragons[i].transform.childCount; j++)
					{
						if (dragons[i].transform.GetChild(j).name == "DTShield(Clone") Destroy (dragons[i].transform.GetChild(j).gameObject);
					}	
				}
			}
			if (role == "Defender")transform.GetComponent<DragonTamer>().Shield();
			
		}
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletA");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds)) lives -= 1;
		}
		bullets = GameObject.FindGameObjectsWithTag("BulletB");
		foreach (GameObject bullet in bullets)
		{
			if (bullet.renderer.bounds.Intersects(renderer.bounds)) lives -= 1;
		}
	}
}
