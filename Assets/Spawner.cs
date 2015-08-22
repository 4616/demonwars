using UnityEngine;
using System.Collections;

public class Spawner : Obstacle {
	public GameObject MinonPrefab;
	public bool ownable = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(owner.sorrow >= 1f){
			this.Spawn();
			//owner.sorrow -= 1f;
	
		//}
	}
	void Spawn () {
		Instantiate (MinonPrefab, this.transform.position, this.transform.rotation);
	}
}
