using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {
	public bool paused;
	//public UIManager UI;
	// Use this for initialization
	void Start () {
		paused = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
		//if (Global.GameOver) {
			paused = !paused;
			Debug.Log("space captured");
			Debug.Log(paused);


		}

		if(paused){
			//UI.GetComponentInChildren<Canvas>().enabled = false;
			Time.timeScale = 0;
		}

		else if (!paused){
			Time.timeScale = 1;
		}
	
	}
}
