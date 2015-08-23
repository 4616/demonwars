using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	public enum State
	{
		Wander,
		Move,
		Combat
	};
	
	
	//public string tag = "Minion";
	public float health = 10f;
	public State state;
	public Player owner;
	public float wanderMagnitude = 0f;
	public float movePerterbation = 0.05f;
	public float moveSpeed = 0.1f;
	public float tokenAtraction = 2f;
	public Vector2 moveDelta;
	public GameObject target;
	public float damage = 1f;
	public float brownianJumpMagnitude = 1f;
	public float probabilityToWander = 0.1f;
	public float range = 1.5f;
	
	
	// Use this for initialization
	void Start () {
		state = State.Wander;

		//tag = "Minion";
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Wander:
			if (Random.value < probabilityToWander) {
			    Vector2 delta = deltaV ();
			    gameObject.transform.position += new Vector3 (delta.x, delta.y);
			}
			break;
		case State.Move:
			if (moveDelta != null) {
				gameObject.transform.position += new Vector3 (moveDelta.x, moveDelta.y);
			}
			break;
		case State.Combat:
			if (target != null) {
				float dist = Vector3.Distance(transform.position, target.transform.position);
				if (dist < range) {
					target.GetComponent<Minion>().TakeDamage(damage);
				}else {
					gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, moveSpeed);
				}
			} else {
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

//	void OnCollisionEnter2D(Collider2D otherObj) {
//		print("Collision on minion!");
//	}


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
		Vector2 velocity = new Vector2 (Random.Range (-brownianJumpMagnitude,brownianJumpMagnitude), 
		                                Random.Range (-brownianJumpMagnitude,brownianJumpMagnitude));
		float minDist = 0f;
		Token closest = null;

//		if (owner != null) {
//
//			foreach (Token token in owner.tokenManager.tokenList) {
//				float dist = Vector3.Distance (gameObject.transform.position, token.gameObject.transform.position);
//				if (closest == null || minDist > dist) {
//					Debug.Log("We close enough?");
//					minDist = dist;
//					closest = token;
//				}
//			}
//			if (closest != null) {
//
//				gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, closest.gameObject.transform.position, tokenAtraction);
//			}
//		}

//		velocity.Normalize ();
		velocity *= (state == State.Wander ? wanderMagnitude : movePerterbation);
		return velocity;
	}
}
