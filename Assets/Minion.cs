﻿using UnityEngine;
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
	public float combatSpeed = 0.2f;
	public float tokenAtraction = 2f;
	public Vector2 moveDelta;
	public GameObject target;
	public float damage = 1f;
	public float brownianJumpMagnitude = 1f;
	public float probabilityToWander = 0.1f;
	public float range = 1.5f;
	public float deathChance;
	public int newCommandTimeout = 10;
	private SpriteRenderer spriteRenderer; 	

	private int _newCommandTimer;
	
	// Use this for initialization
	void Start () {
		state = State.Wander;
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject

		_newCommandTimer = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (_newCommandTimer > 0) _newCommandTimer--;

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
				}
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, combatSpeed);
			} else {
				state = State.Wander;
			}
			break;
		default:
			break;
		}
		if (Random.Range (0f, 1f) <= deathChance) {
			Destroy(gameObject, 0.05f);
		}
		if (gameObject.transform.position.x < Global.left || gameObject.transform.position.x > Global.right
			|| gameObject.transform.position.y < Global.bottom || gameObject.transform.position.y > Global.top) {
			Destroy(gameObject, 0.05f);
		}
	}

	public bool Blocking() {
		return (_newCommandTimer > 0);
	}

	public void setState(State s) {
		if (Blocking())
			return;
		state = s;
		_newCommandTimer = newCommandTimeout;
	}

	public void TakeOwnership(Player newowner){
		this.owner = newowner;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = newowner.PlayerColor;
		this.gameObject.layer = newowner.PlayerLayer;
	}
	
	private void findNewTarget() {

	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		if(health < 0){
			Destroy(gameObject, 0.05f);
		}
		
	}

//	void OnCollisionEnter2D(Collider2D otherObj) {
//		print("Collision on minion!");
//	}


	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("OnTriggerEnter");
		if (state != null && state != State.Combat && other.gameObject.tag == "Minion") {
			Minion minon = other.gameObject.GetComponent<Minion>();
			if (minon.owner != this.owner) {
				Debug.Log ("actually start combat");
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
