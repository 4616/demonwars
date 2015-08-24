using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {
	public enum State
	{
		Wander,
		Move,
		Combat,
		AttackSpawner
	};

	public Sprite spriteminion1; // Drag your first sprite here
	public Sprite spriteminion2; // Drag your second sprite here
	private SpriteRenderer spriteRenderer; 
	public GameObject explosion;
	
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
	public GameObject spawnerTarget;
	public float damage = 1f;
	public float brownianJumpMagnitude = 1f;
	public float probabilityToWander = 0.1f;
	public float range = 1.5f;
	public float deathChance;
	public int newCommandTimeout = 10;


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
				if (Random.Range(0f,1f)<.5f){
					gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, combatSpeed);
				}
				else {target.transform.position = Vector3.MoveTowards(target.transform.position, gameObject.transform.position, combatSpeed);
				}
			} else {
				state = State.Wander;
			}
			break;
		case State.AttackSpawner:
			if (spawnerTarget != null) {
				gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, spawnerTarget.transform.position, moveSpeed);
				float dist = Vector3.Distance (transform.position, spawnerTarget.transform.position);
				if (dist < range) {
					spawnerTarget.GetComponent<Spawner> ().TakeDamage (damage);
				}
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
		if (gameObject.transform.position.x < Global.left) {
			transform.position = new Vector3(Global.left, transform.position.y, transform.position.z);
		} else if (gameObject.transform.position.x > Global.right) {
			transform.position = new Vector3(Global.right, transform.position.y, transform.position.z);
		} else if (gameObject.transform.position.y < Global.bottom) {
			transform.position = new Vector3(transform.position.x, Global.bottom, transform.position.z);
		} else if (gameObject.transform.position.y > Global.top) {
			transform.position = new Vector3(transform.position.x, Global.top, transform.position.z);
		}
	}

	void LateUpdate(){
		if (state == State.Combat && target != null) {
			float dist = Vector3.Distance (transform.position, target.transform.position);
			if (dist < range) {
				target.GetComponent<Minion> ().TakeDamage (damage);
			}
			//CheckDeath ();
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


	void RandomSprite (){
		int spritenumber = Random.Range (1, 3);
		if (spritenumber == 1) // if the spriteRenderer sprite = sprite1 then change to sprite2
		{
			spriteRenderer.sprite = spriteminion1;
		}

		if (spritenumber == 2) // if the spriteRenderer sprite = sprite1 then change to sprite2
		{
			spriteRenderer.sprite = spriteminion2;
		}
		else
		{
			spriteRenderer.sprite = spriteminion1; // otherwise change it back to sprite1
		}
	}

	public void TakeOwnership(Player newowner){
		this.owner = newowner;
		spriteRenderer = GetComponent<SpriteRenderer>();
		RandomSprite ();
		//spriteRenderer.sprite = spriteminion2;
		//Debug.Log ("taking ownership of minion");
		//Debug.Log (spriteminion2);
		spriteRenderer.color = newowner.PlayerColor;
		this.gameObject.layer = newowner.PlayerLayer;

		foreach(Transform child in transform)
		{
			child.gameObject.layer = newowner.PlayerLayer;
		}
	}
	
	private void findNewTarget() {

	}
	
	public void TakeDamage(float damage) {
		health -= damage;
		CheckDeath ();
	}

	public void CheckDeath(){
		if(health < 0){
			Instantiate (explosion, this.transform.position, this.transform.rotation);
			Debug.Log ("minion death");
			Destroy(gameObject, 0.05f);

		}
	}

//	void OnCollisionEnter2D(Collider2D otherObj) {
//		print("Collision on minion!");
//	}


//	void OnTriggerEnter2D(Collider2D other) {
//		if (state != null && state != State.Combat && other.gameObject.tag == "Minion") {
//			Minion minon = other.gameObject.GetComponent<Minion>();
//			if (minon.owner != this.owner) {
//				// Come at me bro!
//				state = State.Combat;
//				target = other.gameObject;
//			}
//		}
//	}

	void OnTriggerStay2D(Collider2D other) {
		if (state != null && state != State.Combat) {
			if (other.gameObject.tag == "Minion") {
				Minion minon = other.gameObject.GetComponent<Minion>();
				if (minon.owner != this.owner) {
					// Come at me bro!
					state = State.Combat;
					target = other.gameObject;
				} else {
					Debug.Log("Same team, man");
				}
			} else if (state != State.AttackSpawner && other.gameObject.tag == "Spawner") {
				Spawner spawn = other.gameObject.GetComponent<Spawner>();
				if (spawn.owner != this.owner) {
					state = State.AttackSpawner;
					spawnerTarget = other.gameObject;
					Debug.Log("Attack the spawner");
				} else {
					Debug.Log("No, the other base");
				}
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
