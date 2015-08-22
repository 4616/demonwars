using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	public enum State
	{
		Wander,
		Move,
		Combat
	};
	
	
	public string tag = "Minion";
	public float health = 10f;
	public State state;
	public Player owner;
	public float wanderMagnitude = 0.1f;
	public float movePerterbation = 0.05f;
	public float moveSpeed = 0.1f;
	public Vector2 moveDelta;
	public GameObject target;
	public float damage = 1f;
	
	
	// Use this for initialization
	void Start () {
		state = State.Wander;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Wander:
			Vector2 delta = deltaV ();
			gameObject.transform.position += new Vector3 (delta.x, delta.y);
			break;
		case State.Move:
			if (moveDelta != null) {
				gameObject.transform.position += new Vector3 (moveDelta.x, moveDelta.y);
			}
			break;
		case State.Combat:
			if (target != null) {
				// if target in range 
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, moveSpeed);
				// else
				//findNewTarget ();
			} else {
				// findNewTarget ();
				// wander state if nothing in range
				state = State.Wander;
			}
			break;
		default:
			break;
		}
	}
	
	private void findNewTarget() {
		
	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		if(health < 0){
			Destroy(gameObject,.5f);
		}
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (state != null && state != State.Combat && other.gameObject.tag == "Minion") {
			Minion minon = other.gameObject.GetComponent<Minion>();
			if (minon.owner != this.owner) {
				// Come at me bro!
				state = State.Combat;
				target = other.gameObject;
			}
		}
	}
	
	Vector2 deltaV() {
		Vector2 velocity = new Vector2 (Random.Range (-1f,1f), Random.Range (-1f,1f));
		velocity.Normalize ();
		velocity *= (state == State.Wander ? wanderMagnitude : movePerterbation);
		return velocity;
	}
}
