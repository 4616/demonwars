using System.Collections;
using UnityEngine;

class Token : MonoBehaviour
{
	private bool dragging = false;
	private float distance;
	private float xDeg = 90;

	//Don't allow the player to re-drag if they already dragged the tower.  They can still rotate though.
	private bool alreadyDragged = false;

	public float rotateSpeed = 10;
	public float rotateFriction = 2;
	
	void Start() {

	}
	
	void OnMouseDown()
	{
		if (!alreadyDragged) {
			distance = Vector3.Distance (transform.position, Camera.main.transform.position);
			dragging = true;
		}
	}
	
	void OnMouseUp()
	{
		dragging = false;
		alreadyDragged = true;
	}
	
	void Update()
	{
		if (dragging)
		{
			if (Input.GetButton("Fire2")) {
				xDeg -= Input.GetAxis("Mouse X") * rotateSpeed / rotateFriction;
				var target = Quaternion.Euler (0, 0, xDeg);
				// Dampen towards the target rotation
				transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * rotateSpeed);
			}
			else {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 rayPoint = ray.GetPoint(distance);
				transform.position = rayPoint;
			}
		}
	}
}