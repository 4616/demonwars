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

	public Player owner;

	public GameObject explosion;
	
	void Start() {

	}

	public void init(Player owner) {
		this.owner = owner;
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