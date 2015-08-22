using UnityEngine;
using System.Collections;

public class ClickForegroundController : MonoBehaviour {

	public int delayTimer;
	private int currentDelay = 0;

	public Transform token;

//	private Camera camera = GetComponent.<Camera>();

	void OnMouseDown()
	{
		Debug.Log ("Potato");
		if (currentDelay <= 0) {
			var mousePos = Input.mousePosition;
			Debug.Log (mousePos.x);
			Debug.Log (mousePos.y);
			mousePos.z = 10;
			var objectPos = Camera.current.ScreenToWorldPoint(mousePos);
			Debug.Log(objectPos);
//			Instantiate(token, objectPos, Quaternion.identity);
			GameObject objectNew = GameObject.Instantiate(token, objectPos, Quaternion.identity) as GameObject;
			Rigidbody clone = objectNew.GetComponent<Rigidbody>();
			Debug.Log("instantiated");
		}
	}

	void Update()
	{
		currentDelay--;
	}
}
