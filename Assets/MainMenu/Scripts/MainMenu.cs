/// <summary>
/// Main menu.
/// Attached to Main Camera (or so says YouTube)
/// https://www.youtube.com/watch?v=xoaYzfCix3c
/// </summary>

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Texture backgroudTexture;

	public float guiPlacementX1 = .25f;
	public float guiPlacementX2 = .25f;
	public float guiPlacementY1 = .5f;
	public float guiPlacementY2 = .7f;
	void OnGUI(){
		// Displays background texture

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroudTexture);


		// Displays our Buttons (with GUI Outline)
					//button location x, button location y, buttion size x, button size y
		if(GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Play Game")){
			print ("Clicked Play Game");
		}

		if(GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .5f, Screen.height * .1f), "Options")){
			print ("Clicked Options");
		}

		// Displays our Buttons (with GUI Outline)
		//button location x, button location y, buttion size x, button size y
		if(GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "", Random1)){
			print ("Clicked Play Game");
		}
		
		if(GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .5f, Screen.height * .1f),"", Random1)){
			print ("Clicked Options");
		}

	}


}
