using UnityEngine;
using System.Collections;

public abstract class MinionState {

	public abstract Vector2 setVelocity();

	public bool inCombat() {
		return false;
	}
}
