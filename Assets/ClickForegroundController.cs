using UnityEngine;
using System.Collections;

public class ClickForegroundController : MonoBehaviour {

	public int delayTimer;
	private int currentDelay;

	public Transform token;

	void OnMouseDown()
	{
		if (currentDelay <= 0) {
			var mousePos = Input.mousePosition;
			var objectPos = Camera.current.ScreenToWorldPoint(mousePos);
			Instantiate(token, objectPos, Quaternion.identity);
		}
	}

	void Update()
	{
		currentDelay--;
	}
}
