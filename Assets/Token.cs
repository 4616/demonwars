using System.Collections;
using UnityEngine;

public class Token : MonoBehaviour
{
	private float distance;
	private float xDeg = 90;

	public enum State {Fresh, Dragging, Dropped, RateLimiting, Finished};

//	[HideInInspector]
	private State state = State.Dragging;

	//Don't allow the player to re-drag if they already dragged the tower.  They can still rotate though.
//	private bool alreadyDragged = false;

	public float rotateSpeed = 10;
	public float rotateFriction = 2;

	public int rateLimit = 10;

	public float minionAttractionRate = 0.1f;

	public Player owner;

	public GameObject explosion;
	public SpriteRenderer spriteRenderer;
	
	void Start() {

	}

	public void init(Player owner) {
		this.owner = owner;
	}

	public void AIToken(float deg) {
		this.state = State.Finished;
		this.xDeg = deg;
	}

	public void TakeOwnership(Player newowner){
		this.owner = newowner;
		spriteRenderer = GetComponent<SpriteRenderer>();
		//spriteRenderer.color = newowner.PlayerColor;

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

		Minion miniontest = (other.gameObject.GetComponent<Minion> ());
		if (Blocking () || Random.value > 0.1 || this.owner.PlayerNumber != miniontest.owner.PlayerNumber)
			return;

		Minion minion = (other.gameObject.GetComponent<Minion> ());
		if ( minion.state != Minion.State.Wander)
			return;
		Vector2 distanceDiff = new Vector2 (this.transform.position.x - minion.transform.position.x, 
		                                   this.transform.position.y - minion.transform.position.y);
		if(distanceDiff.sqrMagnitude < 0.1f) {
			minion.state = Minion.State.Move;
			float tokenAngle = this.transform.rotation.eulerAngles.z;
			float x = Mathf.Cos(tokenAngle*Mathf.PI/180);
			float y = Mathf.Sin(tokenAngle*Mathf.PI/180);
			minion.moveDelta = new Vector2(0.1f * x,0.1f * y);
		} else {
			Debug.Log (distanceDiff);
			Debug.Log (distanceDiff.normalized);
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
		if (state == State.Dragging) {
			state = State.Dropped;
		} else if (state == State.Dropped) {
			new WaitForSeconds(1.0f);
			state = State.Finished;
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