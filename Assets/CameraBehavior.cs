using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {
	public float dragSpeed = 2f;
	public float zoom = 20;
	public float zoomSpeed = 2f;
	public float minZoom = 2f;
	public float maxZoom = 100f;
	private Vector3 dragStart;

	Camera cam;

	// Use this for initialization
	void Start () {
		cam = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update(){
		//Debug.Log("scroll"+Input.GetAxis ("Mouse ScrollWheel").ToString());
		cam.orthographicSize *= 1-Input.GetAxis ("Mouse ScrollWheel");
	}

	void LateUpdate () {
		if (Input.GetMouseButtonDown(2)){
			dragStart = Input.mousePosition;
			return;
		}
		if (!Input.GetMouseButton (2))
			return;



		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragStart);
		Vector3 move = new Vector3(pos.x * dragSpeed,pos.y * dragSpeed);
		transform.Translate (move, Space.Self); 
	}
}
