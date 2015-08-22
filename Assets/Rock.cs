using UnityEngine;
using System.Collections;

public class Rock : Obstacle {
	public string tag = "Rock";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision otherObj) {
		if (otherObj.gameObject.tag == "Minion") {
			Minion minon = otherObj.gameObject.GetComponent<Minion>();
			minon.state = new WanderState ();
		}
	}

}
