using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed;
	public float height;
	public float health;
	public GameObject projectileA;
	public GameObject projectileB;
	public GameObject coin;
	public GameObject deadEnemy;
	public int type;
	public float attackCD = 1;
	public bool fire = false;
	public int attacktype = 0;
	float attackTimer = 0;
	private GameObject shippe1;
	private GameObject shippe2;
	private GameObject shippeatk;
	public bool affinity = false;
	private Vector3 target;
	public int path;
	public int point = 0;

	// Use this for initialization
	void Start () {
		string thisName = name.Substring(5);
		thisName = thisName.Remove(thisName.Length - 7);
		type = int.Parse(thisName);
		Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (10, 10));
		pos.z = 0;
		//transform.position = pos;
		shippe1 = GameObject.FindGameObjectWithTag ("Player1");
		shippe2 = GameObject.FindGameObjectWithTag ("Player2");
		//transform.rotation = Quaternion.Euler(new Vector3(180,0,0));

		int random = Random.Range (0, 2);
		if (random == 0) shippeatk = shippe1;
		else shippeatk = shippe2;
	}
	
	// Update is called once per frame
	void Update () {
//		int random = Random.Range (0, 2);
	//	if (random == 0) affinity = false;
		//else affinity = true;


		checker (point);
		if (attackCD < Time.time - attackTimer && fire == true){
				GameObject bullet;
			//Down Attack
			if (attacktype == 0){

				if (affinity == false){
					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;

				}
				else {
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;


					}
				//cone attack
			}else if(attacktype == 1){
				//rotations();
				if (affinity == false) {
					float angle = 0;
					Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
					angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;



					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate(0,0,-angle+180);


					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate(0,0,-angle+10+180);


					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate(0,0,-angle-10+180);

				}
				else {
					float angle = 0;
					Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
					angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
					
					
					
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate(0,0,-angle+180);
					
					
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate(0,0,-angle+10+180);
					
					
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate(0,0,-angle-10+180);

				}
				//Wave Attack
			}else if(attacktype == 2){
				//rotations();
				float angle = 0;
				Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
				angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
				if (affinity == false) {
					bullet = Instantiate(projectileA, transform.position, transform.rotation) as GameObject;
					bullet.GetComponent<EnemyBullet>().setatktype(attacktype);
					bullet.transform.Rotate(0,0,-angle+180);
				}else {
					bullet = Instantiate(projectileB, transform.position, transform.rotation) as GameObject;
					bullet.GetComponent<EnemyBullet>().setatktype(attacktype);
					bullet.transform.Rotate(0,0,-angle+180);
				}
				//Radius Attack
			}else if(attacktype == 3){

				for(int i = 0; i <360 ; i += 20){
					Transform a = transform;


					Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + i));
					if (affinity == false) {
						bullet = Instantiate(projectileA, transform.position, up) as GameObject;
					}else {
						bullet = Instantiate(projectileB, transform.position, up) as GameObject;
					}
				}
				//Sprinkler Attack
			}else if(attacktype == 4){
				//rotations();
				Transform a = transform;
				for(int j = 0; j < 3; j++){
					int i = Random.Range (-10, 10);
					Quaternion up = Quaternion.Euler( new Vector3(a.rotation.eulerAngles.x, a.rotation.eulerAngles.y,a.rotation.eulerAngles.z + i*5));
					if (affinity == false) {
						bullet = Instantiate(projectileA, transform.position, up) as GameObject;
					}else {
						bullet = Instantiate(projectileB, transform.position, up) as GameObject;
					}
					bullet.GetComponent<EnemyBullet>().setatktype(attacktype);
				}
				//Rain Attack
			}else if(attacktype == 5){

				for(int j = 0; j < 13; j++){

//					random = Random.Range (0, 2);
	//				if (random == 0) affinity = false;
		//			else affinity = true;

					Vector3 b = new Vector3(j-6,5,0);
					Vector3 bb = new Vector3(j-6 +.5f,5,0);
					if (affinity == false) {
						bullet = Instantiate(projectileA, b, transform.rotation) as GameObject;
						bullet = Instantiate(projectileA, bb, transform.rotation) as GameObject;
					}else {
						bullet = Instantiate(projectileB, b, transform.rotation) as GameObject;
						bullet = Instantiate(projectileB, bb, transform.rotation) as GameObject;
					}
					bullet.GetComponent<EnemyBullet>().setatktype(attacktype);
				}
				
			}
		

		attackTimer = Time.time;
		}
		GameObject[] bullets = GameObject.FindGameObjectsWithTag ("BulletC");
		for (int i = 0; i < bullets.Length; i++)
		{
			if (bullets[i].renderer.bounds.Intersects(transform.renderer.bounds))
			{
				if (bullets[i].name != "TinkerBullet(Clone)") health -= 1;
				if (health <= 0)
				{
					GameObject.Find ("Sound").GetComponent<SoundManager>().PlaySound("Explosion");
//					bullets[i].GetComponent<PlayerBullet>().origin.GetComponent<PlayerStats>().get
					Instantiate(coin, transform.position, Quaternion.identity);
					GameObject theDead = (GameObject)Instantiate(deadEnemy, new Vector3(transform.position.x + renderer.bounds.extents.x, transform.position.y), Quaternion.identity);
					theDead.GetComponent<DeadEnemy>().type = type;
					Destroy (gameObject);
				}
				if (bullets[i].name != "TinkerBullet(Clone)") Destroy (bullets[i].gameObject);
			}
		}
		float step = speed * Time.deltaTime;
		//transform.rotation = Quaternion.Lerp(transform.rotation,qr,Time.deltaTime*turnspeed);
		transform.position = Vector3.MoveTowards(transform.position, target, step);
		if(path > 0 && target == transform.position)
			point++;
	}
	void checker(int i){
		if(path == 1){
			if(i == 0)
				target = new Vector3(6,1,0);
		}
		if (path == 2){
			if (i == 0)
				target = new Vector3(-6,2,0);
		}
		if(path == 3){
			if(i == 0)
				target = new Vector3(6,3,0);
		}
		if (path == 4){
			if (i == 0)
				target = new Vector3(-6,4,0);
		}
		if (path == 5){
			if (i == 0)
				target = new Vector3(-2,0,0);
			if (i == 1)
				target = new Vector3(6,0,0);
		}
		if (path == 6){
			if (i == 0)
				target = new Vector3(2,1,0);
			if (i == 1)
				target = new Vector3(-6,1,0);
		}
		if (path == 7){
			if (i == 0)
				target = new Vector3(-2,2,0);
			if (i == 1)
				target = new Vector3(6,2,0);
		}
		if (path == 8){
			if (i == 0)
				target = new Vector3(2,3,0);
			if (i == 1)
				target = new Vector3(-6,3,0);
		}
		if (path == 9){
			if (i == 0)
				target = new Vector3(-2,2,0);
			if (i == 1)
				target = new Vector3(1,2,0);
			if (i == 2)
				target = new Vector3(1,6,0);
		}
		if (path == 10){
			if (i == 0)
				target = new Vector3(-2,2,0);
			if (i == 1)
				target = new Vector3(-2,6,0);
		}
		if (path == 11){
			if (i == 0)
				target = new Vector3(2,2,0);
			if (i == 1)
				target = new Vector3(2,6,0);
		}
		if (path == 12){
			if (i == 0)
				target = new Vector3(-2,1,0);
			if (i == 1)
				target = new Vector3(6,1,0);
		}
		if (path == 13){
			if (i == 0)
				target = new Vector3(2,1,0);
			if (i == 1)
				target = new Vector3(-6,1,0);
		}
		if (path == 14){
			if (i == 0)
				target = new Vector3(-2,2,0);
			if (i == 1)
				target = new Vector3(6,2,0);
		}
		if (path == 15){
			if (i == 0)
				target = new Vector3(2,3,0);
			if (i == 1)
				target = new Vector3(-6,3,0);
		}
		if (path == 16){
			if (i == 0)
				target = new Vector3(-2,2,0);
			if (i == 1)
				target = new Vector3(2,2,0);
			if (i == 2)
				target = new Vector3(5,4,0);
		}
		if (path == 17){
			if (i == 0)
				target = new Vector3(2,2,0);
			if (i == 1)
				target = new Vector3(-2,2,0);
			if (i == 2)
				target = new Vector3(-5,4,0);
		}
		if (path == 18){
			if (i == 0)
				target = new Vector3(0,1,0);
		}
		if (path == 19){
			if (i == 0)
				target = new Vector3(-2,1,0);
		}
		if (path == 20){
			if (i == 0)
				target = new Vector3(-1,1,0);
		}
		if (path == 21){
			if (i == 0)
				target = new Vector3(1,1,0);
		}
		if (path == 22){
			if (i == 0)
				target = new Vector3(2,1,0);
		}
		
		
	}
	public void changeloc(Vector3 a){
		target = a;

	}
	public void fireset(bool a){
		fire = a;

	}
	void rotations(){
		float angle = 0;
		Vector3 relative = transform.InverseTransformPoint(shippeatk.transform.position);
		angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
		transform.Rotate(0,0, -angle);
	}
}
