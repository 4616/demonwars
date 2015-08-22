using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickForegroundController : MonoBehaviour {

	public int delayTimer;
	private int currentDelay = 0;

	public GameObject token;
//	private Camera camera = GetComponent.<Camera>();

	private List<Token> tokenList = new List<Token>();

	void OnMouseDown()
	{
		if (currentDelay <= 0 && (tokenList.Count == 0 || !tokenList[0].Blocking())) {
			var mousePos = Input.mousePosition;
			mousePos.z = -5;
			var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
			//objectPos.z = -5;
			GameObject go = Instantiate(token, objectPos, Quaternion.identity) as GameObject;

			Token tk = go.GetComponent<Token>();
			tk.startDragging();
			tokenList.Insert(0,tk);
//			tk.state = Token.State.Dragging;
			currentDelay = delayTimer;
		}
	}

	void Update()
	{
		currentDelay--;
	}
}
