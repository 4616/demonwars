using UnityEngine;
using System.Collections;

public class Spawner : Obstacle {
	public GameObject MinonPrefab;
	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	public void Init () {
		ownable = true;
		spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject

		//this.TakeOwnership (owner);
	}
	
	// Update is called once per frame
	void Update () {
		//print (owner);
		if(owner != null && owner.sorrow >= 1f){
			this.Spawn();
			owner.sorrow -= 1f;
			//print (owner.sorrow);

		}
	}

	public void TakeOwnership(Player newowner) {
		//print (newowner);
		print (ownable);
		if (ownable) {
			this.owner = newowner;
			spriteRenderer.color = newowner.PlayerColor;
			print (this.tag);
			
		}
	}

	void Spawn () {
		GameObject obj = Instantiate (MinonPrefab, this.transform.position, this.transform.rotation) as GameObject;
		//Debug.Log (LayerMask.LayerToName(gameObject.layer));
		//Debug.Log (gameObject.layer);
		//obj.layer = gameObject.layer;
		Minion mini = obj.GetComponent<Minion> ();
		mini.TakeOwnership(owner);
	}
}
