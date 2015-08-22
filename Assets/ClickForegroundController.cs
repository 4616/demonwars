using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickForegroundController : MonoBehaviour {

	public int delayTimer;
	private int currentDelay = 0;

	public GameObject token;
	public Player owner;
//	private Camera camera = GetComponent.<Camera>();

	//private List<Token> tokenList = new List<Token>();

	void OnMouseDown()
	{
		if (owner != null && currentDelay <= 0 && (owner.tokenList.Count == 0 || !owner.tokenList[0].Blocking())) {
			var mousePos = Input.mousePosition;
			mousePos.z = -5;
			var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
			//objectPos.z = -5;
			GameObject go = Instantiate(token, objectPos, Quaternion.identity) as GameObject;

			Token tk = go.GetComponent<Token>();
			tk.startDragging();
			tk.init(owner);
			owner.tokenList.Insert(0,tk);
//			tk.state = Token.State.Dragging;
			currentDelay = delayTimer;
		}
	}

	void Update()
	{
		currentDelay--;
	}
}
