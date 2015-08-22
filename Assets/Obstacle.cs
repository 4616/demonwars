using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public Player owner = null;
	public bool ownable = false;
	public

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeOwnership(Player newowner) {
		if (ownable) {
			this.owner = newowner;
		}

		}

	public void TakeDamage(float damage) {


	}
}
