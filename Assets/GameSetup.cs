using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {
	public int nplayers = 2;

	public GameObject PlayerPrefab;
	public GameObject SpawnerPrefab;
	public GameObject HousePrefab;
	public GameObject WaterPrefab;
	public GameObject RockPrefab;

	public int numHouses = 50;
	public int numLakes = 12;
	public int numRocks = 17;
	public float boardWidth = 20f;
	public float boardHeight = 30f;
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
		float bottom = - boardHeight / 2f;
		float top = boardHeight / 2f;
		float left = - boardWidth / 2f;
		float right = boardWidth / 2f;

		float flipx = 1;
		float flipy = 1;
		for (int i = 0; i < nplayers; i++) {

			//Add spawners
			float posY = (bottom + 1f) * flipy;
			GameObject playergameobji = Instantiate (PlayerPrefab, new Vector3 (0f, posY, 0f), Quaternion.identity) as GameObject;
			GameObject spawnergameobji = Instantiate (SpawnerPrefab, new Vector3 (0f, posY, 0f), Quaternion.identity) as GameObject;
			Player playeri = playergameobji.GetComponent<Player> ();
			playeri.PlayerColor = PlayerColorList[i];
			playeri.PlayerNumber = i;
			playeri.PlayerLayer = i + 8;
			if(i == 0){
				playeri.HumanPlayer = true;
			}
			else{
				playeri.HumanPlayer = false;
				//playeri.AIBeast();
			}

			Spawner spawneri = spawnergameobji.GetComponent<Spawner> ();
			spawneri.Init ();
			//print (playeri);
			spawneri.TakeOwnership (playeri);
			flipy = flipy * -1f;
		}

		//Add houses
		for (int i = 0; i < numHouses; i+=2) {
			float xposition = Random.Range(left,right);
			float yposition = Random.Range(bottom,top);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (HousePrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			Instantiate (HousePrefab, new Vector3 (-xposition, -yposition, 0f), Quaternion.identity);

		}

		//Add water
		for (int i = 0; i < numLakes; i+=2) {
			float xposition = Random.Range(left,right);
			float yposition = Random.Range(bottom,top);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (WaterPrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			Instantiate (WaterPrefab, new Vector3 (-xposition, -yposition, 0f), Quaternion.identity);
			
		}

		//Add rocks
		for (int i = 0; i < numRocks; i+=2) {
			float xposition = Random.Range(left,right);
			float yposition = Random.Range(bottom,top);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (RockPrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			Instantiate (RockPrefab, new Vector3 (-xposition, -yposition, 0f), Quaternion.identity);
			//Debug.Log("Creating rocks");
			
		}

		for (float i = bottom; i < top; i += 0.25f) {
			Instantiate (RockPrefab, new Vector3 (left, i, 0f), Quaternion.identity);
			Instantiate (RockPrefab, new Vector3 (right, i, 0f), Quaternion.identity);
		}

		for (float i = left; i < right; i += 0.25f) {
			Instantiate (RockPrefab, new Vector3 (i, top, 0f), Quaternion.identity);
			Instantiate (RockPrefab, new Vector3 (i, bottom, 0f), Quaternion.identity);
		}
	
	}	
	 
}
