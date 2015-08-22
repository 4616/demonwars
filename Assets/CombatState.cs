using UnityEngine;
using System.Collections;

public class CombatState : MinionState {
	public GameObject target;

	public float magnitude = 0.1f;
	override public Vector2 setVelocity() {
		Vector2 velocity = new Vector2 (Random.Range (-1f,1f), Random.Range (-1f,1f));
		velocity.Normalize ();
		velocity *= magnitude;
		return velocity;
	}

	public bool inCombat() {
		return true;
	}

}
