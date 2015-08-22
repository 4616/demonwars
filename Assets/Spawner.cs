using UnityEngine;
using System.Collections;

public class Spawner : Obstacle {
	public GameObject MinonPrefab;

	// Use this for initialization
	public void Init () {
		ownable = true;
		//this.TakeOwnership (owner);
	}
	
	// Update is called once per frame
	void Update () {
		//print (owner);
		if(owner != null && owner.sorrow >= 1f){
			this.Spawn();
			owner.sorrow -= 1f;
			//print (owner.sorrow);

		}
	}

	void Spawn () {
		Instantiate (MinonPrefab, this.transform.position, this.transform.rotation);
	}
}
