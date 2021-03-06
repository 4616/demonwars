﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {
	public int nplayers = 2;
	public bool testing = false;

	public GameObject PlayerPrefab;
	public GameObject SpawnerPrefab;
	public GameObject HousePrefab;
	public GameObject WaterPrefab;
	public GameObject RockPrefab;
	public GameObject ClickForegroundPrefab;

	public int numHouses = 50;
	public int numLakes = 12;
	public int numRocks = 17;
	List<Color> PlayerColorList = new List<Color>(){
		Color.red,
		Color.green,
		Color.magenta,
		Color.white,
	};



	// Use this for initialization
	void Start () {
		StartGame();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartGame(){

		float flipx = 1;
		float flipy = 1;
		for (int i = 0; i < nplayers; i++) {

			//Add spawners
			float posY = (Global.bottom + 1f) * flipy;
			GameObject playergameobji = Instantiate (PlayerPrefab, new Vector3 (0f, posY, 0f), Quaternion.identity) as GameObject;
			GameObject spawnergameobji = Instantiate (SpawnerPrefab, new Vector3 (0f, posY, 0f), Quaternion.identity) as GameObject;
			string layername = i == 0 ? "side1" : "side2";
			spawnergameobji.layer = LayerMask.NameToLayer(layername);
			Player playeri = playergameobji.GetComponent<Player> ();

			playeri.baseposY = posY;
			playeri.PlayerColor = PlayerColorList[i];
			playeri.PlayerColor.a = 1f;
			playeri.PlayerNumber = i;
			playeri.PlayerLayer = LayerMask.NameToLayer(layername);
			Debug.Log(playeri.PlayerColor.ToString());

			if(i == 0){
				playeri.HumanPlayer = true;
				playeri.maxTokensLimit = 23 - (3 * Global.difficulty);
				playeri.sorrow = 55 - (Global.difficulty * 10);
				ClickForegroundPrefab.GetComponent<ClickForegroundController>().owner = playeri;
			}
			else{


				if(testing == false){
					playeri.HumanPlayer = false;
					playeri.sorrow = Global.difficulty * 100;
					playeri.sorrowgen = 0.05f * Global.difficulty;
					playeri.maxTokensLimit = Global.difficulty * 20;
					Debug.Log ("AI player created");
					//playeri.AIBeast();
				}
			}
			Debug.Log(playeri.maxTokensLimit);
			Spawner spawneri = spawnergameobji.GetComponent<Spawner> ();
			spawneri.Init ();
			//print (playeri);
			spawneri.TakeOwnership (playeri);
			flipy = flipy * -1f;
		}

		//Add houses
		for (int i = 0; i < numHouses; i+=2) {
			float xposition = Random.Range(Global.left,Global.right);
			float yposition = Random.Range(Global.bottom,Global.top);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (HousePrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			Instantiate (HousePrefab, new Vector3 (-xposition, -yposition, 0f), Quaternion.identity);

		}

		//Add water
		for (int i = 0; i < numLakes; i+=2) {
			float xposition = Random.Range(Global.left,Global.right);
			float yposition = Random.Range(Global.bottom,Global.top);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (WaterPrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			Instantiate (WaterPrefab, new Vector3 (-xposition, -yposition, 0f), Quaternion.identity);
			
		}

		//Add rocks
		for (int i = 0; i < numRocks; i+=2) {
			float xposition = Random.Range(Global.left,Global.right);
			float yposition = Random.Range(Global.bottom,Global.top);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (RockPrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			Instantiate (RockPrefab, new Vector3 (-xposition, -yposition, 0f), Quaternion.identity);
			//Debug.Log("Creating rocks");
			
		}

		for (float i = Global.bottom; i < Global.top; i += 0.25f) {
			Instantiate (RockPrefab, new Vector3 (Global.left, i, 0f), Quaternion.identity);
			Instantiate (RockPrefab, new Vector3 (Global.right, i, 0f), Quaternion.identity);
		}

		for (float i = Global.left; i < Global.right; i += 0.25f) {
			Instantiate (RockPrefab, new Vector3 (i, Global.top, 0f), Quaternion.identity);
			Instantiate (RockPrefab, new Vector3 (i, Global.bottom, 0f), Quaternion.identity);
		}
	
	}	
	 
}
