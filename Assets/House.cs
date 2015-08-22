using UnityEngine;
using System.Collections;

public class House : Obstacle {
	public int sorrow = 1;
	public int sorrowgen = 0;
	public int sorrowgenratechange = .01;


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
