using UnityEngine;
using System.Collections;

public class House : Obstacle {
	public float sorrow = 1f;
	public float sorrowgen = 0f;
	public float sorrowgenratechange = .00001f;



	// Use this for initialization
	void Start () {
		tag = "House";
		ownable = true;

	
	}
	
	// Update is called once per frame
	void Update () {
		sorrowgen += sorrowgenratechange;
		sorrow += sorrowgen;

		if (owner != null){
			owner.sorrow += sorrow;
			sorrow = 0f;
	
		}
	}

	void OnCollisionEnter2D(Collision2D otherObj) {
		Debug.Log ("Collision with a house!");
		//if (otherObj.gameObject.tag == "Minion") {
		//	Minion minion = otherObj.gameObject.GetComponent<Minion>();
		//	this.TakeOwnership(minion.owner);
		//}
	}
//	void OnCollisionEnter(Collision otherObj) {
//		print ("Collision with a house!");
//		if (otherObj.gameObject.tag == "Minion") {
//			Minion minion = otherObj.gameObject.GetComponent<Minion>();
//			this.TakeOwnership(minion.owner);
//		}
//	}
}
