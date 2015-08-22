using UnityEngine;
using System.Collections;

public class House : Obstacle {
	public float sorrow = 1;
	public float sorrowgen = 0;
	public float sorrowgenratechange = .00001;
	public bool ownable = true;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		sorrowgen += sorrowgenratechange;
		sorrow += sorrowgen;

		if (owner != null){
			owner.sorrow += sorrow;
			sorrow = 0;
	
	}
}
