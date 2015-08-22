using UnityEngine;
using System.Collections;

public class Water : Obstacle {

	public float damage = 1000000000000f;


	// Use this for initialization
	void Start () {
		tag = "Water";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision otherObj) {
		if (otherObj.gameObject.tag == "Minion") {
			Minion minon = otherObj.gameObject.GetComponent<Minion>();
			minon.TakeDamage(this.damage);
		}
	}


}
