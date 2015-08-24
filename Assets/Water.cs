using UnityEngine;
using System.Collections;

public class Water : Obstacle {

	public float damage = 1000000000000f;


	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = this.RandomSprite();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherObj) {
		if (otherObj.gameObject.tag == "Minion") {
			Minion minion = otherObj.gameObject.GetComponent<Minion>();
			//minion.state = Minion.State.Wander;
			minion.TakeDamage(damage);
			Debug.Log("Drowning Minion!");
		}
	}

	Sprite RandomSprite(){
		Sprite[] Lakes = Resources.LoadAll<Sprite>("Lakes");
		int choice = Random.Range (0, Lakes.Length);
		return Lakes [choice];
	}	

}
