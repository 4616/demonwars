using UnityEngine;
using System.Collections;

public class House : Obstacle {
	public float sorrow = 1f;
	public float sorrowgen = 0f;
	public float sorrowgenratechange = .00001f;

	public Sprite sprite1; // Drag your first sprite here
	public Sprite sprite2; // Drag your second sprite here

	// Use this for initialization
	void Start () {
		ownable = true;
		///
				spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
		if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
			spriteRenderer.sprite = sprite1; // set the sprite to sprite1
		///
	}
	
	// Update is called once per frame
	void Update () {

		sorrowgen += sorrowgenratechange;
		sorrow += sorrowgen;

		if (owner != null){
			owner.sorrow += sorrow;
			sorrow = 0f;
	
		}
	}


	
	private SpriteRenderer spriteRenderer; 
	


	void ChangeTheDamnSprite ()
	{
		if (spriteRenderer.sprite == sprite1) // if the spriteRenderer sprite = sprite1 then change to sprite2
		{
			spriteRenderer.sprite = sprite2;
		}
		else
		{
			spriteRenderer.sprite = sprite1; // otherwise change it back to sprite1
		}
	}

	void OnTriggerEnter2D(Collider2D otherObj) {
		//Debug.Log (otherObj);
		if (otherObj.gameObject.tag == "Minion") {
			Minion minion = otherObj.gameObject.GetComponent<Minion>();
			this.TakeOwnership(minion.owner);
			float duration = 1.0f;
			ChangeTheDamnSprite (); // call method to change sprite

			//float lerp = Mathf.PingPong(Time.time, duration) / duration;
			//spr.color = 
			//this.material.color = Color.Lerp(otherObj.GetComponent<Renderer>().material.color, Color.red, lerp);
			/Debug.Log("owner of house is " + owner);
		}
	}



//	void OnCollisionEnter(Collision otherObj) {
//		print ("Collision with a house!");
//		if (otherObj.gameObject.tag == "Minion") {
//			Minion minion = otherObj.gameObject.GetComponent<Minion>();
//			this.TakeOwnership(minion.owner);
//		}
//	}
}
