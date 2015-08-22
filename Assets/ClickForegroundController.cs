using UnityEngine;
using System.Collections;

public class ClickForegroundController : MonoBehaviour {

	public int delayTimer;
	private int currentDelay = 0;

	public Transform token;

	private Camera camera = GetComponent.<Camera>();

	void OnMouseDown()
	{
		Debug.Log ("Potato");
		if (currentDelay <= 0) {
			var mousePos = Input.mousePosition;
			Debug.Log (mousePos.x);
			Debug.Log (mousePos.y);
//			var objectPos = Camera.current.ScreenToWorldPoint(mousePos);
//			Instantiate(token, objectPos, Quaternion.identity);
		}
	}

	void Update()
	{
		currentDelay--;
	}
}
