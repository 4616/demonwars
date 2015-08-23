using UnityEngine;
using System.Collections;

public class Rock : Obstacle {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	} 
	void OnTriggerEnter2D(Collider2D otherObj) {
		//Debug.Log (otherObj);
		if (otherObj.gameObject.tag == "Minion") {
			Minion minion = otherObj.gameObject.GetComponent<Minion>();
			minion.state = Minion.State.Wander;
			//Debug.Log("Wandering Minion!");
		}


}
}
