﻿using UnityEngine;
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
		for (int i = 0; i <= this.nplayers; i++){
			GameObject playergameobji = Instantiate(PlayerPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
			GameObject spawnergameobji = Instantiate(SpawnerPrefab, new Vector3(.5f,.5f,0f), Quaternion.identity) as GameObject;
			Player playeri =  playergameobji.GetComponent<Player>();
			Spawner spawneri =  spawnergameobji.GetComponent<Spawner>();
			spawneri.TakeOwnership(playeri);
		}
		
	 }
}
