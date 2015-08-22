using UnityEngine;
using System.Collections;

public class Water : Obstacle {
	public string tag = "Water";
	public float damage = 1000000000000f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision otherObj) {
		if (otherObj.gameObject.tag == "Minion") {
			otherObj.TakeDamage(float this.damage);
		}
	}


}
