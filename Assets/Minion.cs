using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	public string tag = "Minion";
	public float health = 10f;
	public MinionState state;

	// Use this for initialization
	void Start () {
		state = new WanderState ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 delta = state.setVelocity ();
		gameObject.transform.position += new Vector3(delta.x, delta.y);
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if(health < 0){
			Destroy(gameObject,.5f);

		}

	}

	void OnTriggerEnter(Collider other) {

	}
}
