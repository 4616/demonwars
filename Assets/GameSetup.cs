using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {
	public int nplayers = 2;
	public GameObject PlayerPrefab;
	public GameObject SpawnerPrefab;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Initialize(){
		for (int i = 0; i <= this.nplayers; i++){
			GameObject playeri = Instantiate(PlayerPrefab, new Vector3(0,0,0), Quaternion 0);
			GameObject spawneri = Instantiate(SpawnerPrefab, new Vector3(0,0,0), Quaternion 0);
			spawneri.TakeOwnership(playeri);
		}
		
	 }
}
