using System.Collections;
using UnityEngine;

public class Token : MonoBehaviour
{
	private float distance;
	private float xDeg = 90;

	public enum State {Fresh, Dragging, Dropped, RateLimiting, Finished};

//	[HideInInspector]
	public State state = State.Dragging;

	//Don't allow the player to re-drag if they already dragged the tower.  They can still rotate though.
//	private bool alreadyDragged = false;

	public float rotateSpeed = 10;
	public float rotateFriction = 2;

	public int rateLimit = 10;

	public float minionAttractionRate = 0.1f;

	public float towerStrength = 0.1f;

	public Player owner;

	public GameObject explosion;
	private SpriteRenderer spriteRenderer;
	
	void Start() {

	}

	public void init(Player owner) {
		this.owner = owner;
	}

	public void AIToken(float deg) {
		this.state = State.Finished;
		this.xDeg = deg;
		//Debug.Log ("degree " + xDeg);


		transform.rotation = Quaternion.Euler (0, 0, xDeg);
		//var target = Quaternion.Euler (0, 0, xDeg);
		// Dampen towards the target rotation
		//transform.rotation = Quaternion.Euler (transform.rotation, target, 0);

	}
	
	public void TakeOwnership(Player newowner){
		this.owner = newowner;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = newowner.PlayerColor;

	}

	public bool Blocking() {
		return !(state == State.Finished);
	}

	public bool Active() {
		return (state == State.Dragging || state == State.Dropped || state == State.Fresh);
	}

	public void Click() {
		if( Active()) IncrementState();
	}

	public void IncrementState() {
		if (state == State.Fresh) {
			distance = Vector3.Distance (transform.position, Camera.main.transform.position);
			state = State.Dragging;
		} else if (state == State.Dragging) 
			state = State.Dropped;
		else if (state == State.Dropped) 
			state = State.RateLimiting;
		else if (state == State.RateLimiting)
			state = State.Finished;
	}

	public void Destroy() {
		Destroy (this.gameObject);
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag != "Minion")
			return;

		Minion minion = (other.gameObject.GetComponent<Minion> ());

//		Debug.Log ("We got here");

		if (Blocking () || 
		    (Random.value > 0.1) || 
		    (this.owner.PlayerNumber != minion.owner.PlayerNumber) ||
		    minion.Blocking())
			return;
//		Debug.Log ("We got here2");
		
		Vector2 distanceDiff = new Vector2 (this.transform.position.x - minion.transform.position.x, 
		                                   this.transform.position.y - minion.transform.position.y);
//		Debug.Log ("We got here3");
		if(distanceDiff.sqrMagnitude < 0.1f) {
			minion.setState(Minion.State.Move);
			float tokenAngle = this.transform.rotation.eulerAngles.z;
			float x = Mathf.Cos(tokenAngle*Mathf.PI/180);
			float y = Mathf.Sin(tokenAngle*Mathf.PI/180);
			minion.moveDelta = new Vector2(towerStrength * x,towerStrength * y);
		} else {
			minion.setState(Minion.State.Wander);
		    Vector2 normalizedDistance = distanceDiff.normalized * minionAttractionRate;

			minion.transform.position += new Vector3 (normalizedDistance.x, normalizedDistance.y);
		}

	}

//	public void Drag() {
//
//	}
	
//	void OnMouseDown()
//	{
//		if (state == State.Fresh)
//			startDragging ();
//		if (state == State.Dropped) {
//			state = State.RateLimiting;
//		}
//	}

//	public void startDragging() {
//		distance = Vector3.Distance (transform.position, Camera.main.transform.position);
//		state = State.Dragging;
//	}
	
	void OnMouseUp()
	{
		//These increments may be unnecessary at this point
		if (state == State.Dragging) {
			IncrementState();
		} else if (state == State.Dropped) {
			new WaitForSeconds(1.0f);
			IncrementState();
		}
	}

	void Update()
	{
		if (state == State.Dropped) {
//			if (Input.GetButton("Fire2")) {
			xDeg -= Input.GetAxis ("Mouse X") * rotateSpeed / rotateFriction;
			var target = Quaternion.Euler (0, 0, xDeg);
			// Dampen towards the target rotation
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * rotateSpeed);
		} else if (state == State.Dragging) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint (distance);
			transform.position = rayPoint;
		} else if (state == State.RateLimiting) {
			if (rateLimit == 0) IncrementState();
			else rateLimit --;
		}

	}
}