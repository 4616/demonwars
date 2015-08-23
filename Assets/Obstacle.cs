using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public Player owner = null;
	public bool ownable = false;
	public bool damageable = false;
	public float health = 100f;



	// Use this for initialization
	void Start () {
		//tag = "GenericObstacle";


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeOwnership(Player newowner) {
		//print (newowner);
		print (ownable);
		if (ownable) {
			this.owner = newowner;
			print (this.tag);
			
		}
		
		
	}


	public void TakeDamage(float damage) {
	if (damageable) {
		health -= damage;
		if(health < 0){
			Destroy(gameObject,.5f);
		}
	}
  }

}
