using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {
	public int nplayers = 2;
	public GameObject PlayerPrefab;
	public GameObject SpawnerPrefab;


	// Use this for initialization
	void Start () {
		StartGame();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartGame(){
		float flipx = -1;
		float flipy = -1;

		for (int i = 0; i < this.nplayers; i++){
			GameObject playergameobji = Instantiate(PlayerPrefab, new Vector3(0f,-5f * flipy,0f), Quaternion.identity) as GameObject;
			GameObject spawnergameobji = Instantiate(SpawnerPrefab, new Vector3(0f,-5f * flipy,0f), Quaternion.identity) as GameObject;
			Player playeri =  playergameobji.GetComponent<Player>();
			Spawner spawneri =  spawnergameobji.GetComponent<Spawner>();
			spawneri.Init();
			//print (playeri);
			spawneri.TakeOwnership(playeri);
			flipy = flipy * -1;
		}
		
	 }
}
