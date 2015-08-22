using UnityEngine;
using System.Collections;

public class Spawner : Obstacle {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(owner.sorrow >= 1){
			//spawn a unit here.

			owner.sorrow -= 1;
	
	}
}

}
