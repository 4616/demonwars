using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {
	public int nplayers = 2;
	public int nhouse = 50;
	public int nrock = 20;
	public int nwater = 5;

	public GameObject PlayerPrefab;
	public GameObject SpawnerPrefab;
	public GameObject HousePrefab;
	public GameObject WaterPrefab;
	public GameObject RockPrefab;
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

			GameObject playergameobji = Instantiate (PlayerPrefab, new Vector3 (0f, -5f * flipy, 0f), Quaternion.identity) as GameObject;
			GameObject spawnergameobji = Instantiate (SpawnerPrefab, new Vector3 (0f, -5f * flipy, 0f), Quaternion.identity) as GameObject;
			Player playeri = playergameobji.GetComponent<Player> ();
			Spawner spawneri = spawnergameobji.GetComponent<Spawner> ();
			spawneri.Init ();
			//print (playeri);
			spawneri.TakeOwnership (playeri);
			flipy = flipy * -1f;
		}

		//Add houses
		for (int i = 0; i < nhouse; i++) {
			float xposition = Random.Range(-5f,5f);
			float yposition = Random.Range(-5f,5f);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (HousePrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);

		}

		//Add water
		for (int i = 0; i < nwater; i++) {
			float xposition = Random.Range(-5f,5f);
			float yposition = Random.Range(-5f,5f);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (WaterPrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			
		}

		//Add rocks
		for (int i = 0; i < nrock; i++) {
			float xposition = Random.Range(-5f,5f);
			float yposition = Random.Range(-5f,5f);
			//print ("(x,y)");
			//print (xposition);
			//print (yposition);
			Instantiate (RockPrefab, new Vector3 (xposition, yposition, 0f), Quaternion.identity);
			//Debug.Log("Creating rocks");
			
		}
	
	}	
	 
}
