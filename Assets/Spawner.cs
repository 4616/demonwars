using UnityEngine;
using System.Collections;

public class Spawner : Obstacle {
	public GameObject MinonPrefab;

	// Use this for initialization
	void Start () {
		ownable = true;
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(owner.sorrow >= 1f){
//			this.Spawn();
//			owner.sorrow -= 1f;
//	
//		}
	}
	void Spawn () {
		Instantiate (MinonPrefab, this.transform.position, this.transform.rotation);
	}
}
