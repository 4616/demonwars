/// <summary>
/// Main menu.
/// Attached to Main Camera (or so says YouTube)
/// https://www.youtube.com/watch?v=xoaYzfCix3c
/// </summary>

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Texture backgroudTexture;
	public Texture backgroudTextureWin;
	public Texture backgroudTextureLose;
	public float guiPlacementX1 = .25f;
	public float guiPlacementY1 = .45f;
	public float guiPlacementX2 = .25f;
	public float guiPlacementY2 = .6f;
	public float guiPlacementX3 = .25f;
	public float guiPlacementY3 = .75f;
	public string CurrentMenu = "Main";
	public GUIStyle Random1;
	public string diffstring = "I'm too young to die";

	void OnGUI(){

		if (CurrentMenu == "Main")
			Main_Menu();


		if (CurrentMenu == "Options")
			Options_Menu();


	
		}


	public void NavGate(string nextmenu){
		CurrentMenu = nextmenu;
	
	}
	public void SetBackGround(){
		// Displays background texture
		if(Global.GameOver == false){
			if (Global.Winner == true) {
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroudTextureWin);
			}
			if (Global.Winner == false) {
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroudTextureLose);
			}
		}

		else {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroudTexture);
		}
	}

	public void Main_Menu(){
		SetBackGround ();
			
			
		// Displays our Buttons (with GUI Outline)
		//button location x, button location y, buttion size x, button size y
		if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Play Game")) {
			print ("Clicked Play Game");
			Global.GameOver = false;
			Global.Winner = false;
			Application.LoadLevel ("Main");
		}
			
		if (GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .5f, Screen.height * .1f), "Difficulty" + " (" + diffstring + ")")) {
			NavGate ("Options");

		}

	}
			


	public void Options_Menu(){
		// Displays background texture
		SetBackGround ();

	
		if(GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * .3f, Screen.width * .5f, Screen.height * .1f), "I'm too young to die")){
			diffstring = "I'm too young to die";
			Global.difficulty = 1;
			NavGate("Main");

		}

		if(GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * .45f, Screen.width * .5f, Screen.height * .1f), "Hey, not too rough")){
			diffstring = "Hey, not too rough";
			Global.difficulty = 2;
			NavGate("Main");

		}

		if(GUI.Button (new Rect (Screen.width * guiPlacementX3, Screen.height * .6f, Screen.width * .5f, Screen.height * .1f), "Hurt me plenty")){
			diffstring = "Hurt me plenty";
			Global.difficulty = 3;
			NavGate("Main");

		}

		if(GUI.Button (new Rect (Screen.width * guiPlacementX3, Screen.height * .75f, Screen.width * .5f, Screen.height * .1f), "Ultra-Violence")){
			diffstring = "Ultra-Violence";
			Global.difficulty = 4;
			NavGate("Main");

		}

		if(GUI.Button (new Rect (Screen.width * guiPlacementX3, Screen.height * .9f, Screen.width * .5f, Screen.height * .1f), "Nightmare!")){
			diffstring = "Nightmare!";
			Global.difficulty = 5;
			NavGate("Main");
			
		}
	
	}
	}

