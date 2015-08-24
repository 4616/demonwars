﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public float sorrow;
	// Use this for initialization
	public GameObject token;

	public int maxTokensLimit;
	public Color PlayerColor;
	public int PlayerNumber;
	public int PlayerLayer;
	public bool HumanPlayer;
	public float sorrowgen = 0.05f;
	public TokenManager tokenManager;
//	private float timer;
	public float baseposY;
//	private bool basearrow = false;

	private AIStrategy ai;
	
	public LayerMask aiLayermask;

	public Vector3 getSpawnerPosition() {
		return new Vector3 (0f, Global.top - 1f, 0f);
	}
	
	public class TokenManager {
		private Player owner;
		private GameObject token;
		public TokenManager(Player player, GameObject tokenInstance) {
			owner = player;
			token = tokenInstance;
		}

		public List<Token> tokenList = new List<Token>();
		
		public void createNewToken(Vector3 mousePosition) {
			var objectPos = Camera.main.ScreenToWorldPoint(mousePosition);
			//objectPos.z = -5;
			GameObject go = Instantiate(token, objectPos, Quaternion.identity) as GameObject;
			
			Token tk = go.GetComponent<Token>();
			tk.Click ();
			//tk.init(owner);
			tk.TakeOwnership (owner);
			tokenList.Insert(0,tk);

		}

		public Token createNewTokenAI(Vector3 objectPos, float angle) {
			if (owner.HumanPlayer)
				return null;
			//objectPos.z = -5;
			GameObject go = Instantiate(token, objectPos, Quaternion.identity) as GameObject;
			
			Token tk = go.GetComponent<Token>();
			tk.Click ();
			tk.AIToken (angle);
			//tk.init(owner);
			tk.TakeOwnership (owner);
			Debug.Log ("Expecting AI PlayerNumber 1, got :" + owner.PlayerNumber);
			//Debug.Log (owner.PlayerNumber);

			tokenList.Insert(0,tk);
			return tk;
			
		}

		public Token getActiveToken() {
			if (tokenList.Count > 0) {
				Token activeToken = tokenList[0];
				if (activeToken.Active()) return activeToken;
			}
			return null;
		}

		public void Destroy(Token tk) {
			tk.Destroy ();
			tokenList.Remove (tk);
		}

		public bool ifBlocking() {
			return (getActiveToken() != null);
		}

		public bool Click() {
			Token activeToken = getActiveToken ();
			if (activeToken != null) {
				activeToken.Click ();
				return true;
			}
			return false;
		}

		public void trimOldTokens(int maxTokens) {
			if (tokenList.Count > maxTokens) {
				foreach (Token tk in tokenList.GetRange (maxTokens, (tokenList.Count-(maxTokens)))) {
					//TODO: Play destruction animation
					Debug.Log (maxTokens);
					Debug.Log (tokenList.Count);
					Instantiate (tk.explosion, tk.transform.position, tk.transform.rotation);
					tk.Destroy();
				}
				tokenList = tokenList.GetRange (0, maxTokens);
			}
		}
	}

	private abstract class AIStrategy {

//		private Vector3 _spawnerPosition;
		protected static Player _player;

		public AIStrategy(Player player) {
			_player = player;
//			_spawnerPosition = player.getSpawnerPosition();
		}

//		GameObject findClosestByDegrees(Vector2 position, float degrees) {
//			return findClosest (position, 
//		}

//		GameObject findClosestGameObject (Vector2 position, Vector2 direction) {
		public static GameObject findClosestGameObject (Vector2 currentLocation, Vector2 direction) {
			RaycastHit2D hit = Physics2D.Raycast(currentLocation, direction, Mathf.Infinity, _player.aiLayermask.value);
			 
			if (hit.collider != null) {
				return hit.collider.gameObject;
			}
			return null;
		}

		public static float findClosestGameObjectYDistance(Vector2 currentLocation, Vector2 direction) {
			GameObject closestObject = findClosestGameObject (currentLocation, direction);
			return (Mathf.Abs(closestObject.transform.position.y - currentLocation.y));
		}

		/**
		 * Behavior that should happen every pulse.
		 * These pulses should happen less often than game ticks as set by a parameter
		 **/
		abstract public void pulse ();
	}

	private class RotatingTower {

		private Player _player;
		private Vector2 _playerPosition;

		public RotatingTower(Player player, Vector2 playerPosition) {
			_player = player;
			_playerPosition = playerPosition;
		}

		public bool Locked = false;
		public bool finalLocked = false;

		private Vector2 furthestLocationFound;
		private bool _foundFurthestLocation = false;

		public Vector2 getLockedDirection() {
			return Vector2.ClampMagnitude(furthestLocationFound - _playerPosition,4) + _playerPosition;
		}

		private Token baseTower;

		private Vector2 _enemySpawnerLocation;
		private bool _foundEnemySpawnerLocation = false;

		public void rotateTower() {

			if (Locked)
				return;

			if (baseTower == null) {
				baseTower = _player.tokenManager.createNewTokenAI (new Vector3 (_playerPosition.x, _playerPosition.y, 0), 270f);
			}
			
			List<House> unclaimedHouses = new List<House>();
			

			if (!_foundFurthestLocation) {
				furthestLocationFound = _playerPosition - Vector2.down;
				_foundFurthestLocation = true;
			}
			
			//Determine direction base tower should face
			for (int counter = 1; counter <= 20; counter++) {
				Vector2 randomPos = new Vector2(Random.Range(Global.left * 8,Global.right * 8), -15);
				GameObject go = AIStrategy.findClosestGameObject(_playerPosition, randomPos);
				if (go.tag == "House") {
					House house = go.GetComponent<House> ();
					if (house.owner == null || house.owner != _player)
						unclaimedHouses.Add(house);
				} 
//				else if (go.tag == "Spawner") {
//					if (_playerPosition.y < -5) {
//						Spawner sp = go.GetComponent<Spawner>();
//						if (sp.owner != _player) {
//							_enemySpawnerLocation = sp.transform.position;
//							_foundEnemySpawnerLocation = true;
//						}
//					}
//				}
				if ((_playerPosition.y - go.gameObject.transform.position.y) > (_playerPosition.y - furthestLocationFound.y)) {
					furthestLocationFound = go.gameObject.transform.position;
				}
			}
			float angle = 270f;
			
			Debug.Log ("Unclaimed houses");
			Debug.Log (unclaimedHouses.Count);
			if (unclaimedHouses.Count > 0) {
				House randomHouse = unclaimedHouses [Random.Range (0, unclaimedHouses.Count)];
				Vector3 housePosition = randomHouse.transform.position;
				Vector2 HousePosition2d = new Vector2 (housePosition.x, housePosition.y);
				//				angle = Vector2.Angle (
				//					new Vector2(0, _player.baseposY),
				//					HousePosition2d
				//					);
				angle = Vector2.Angle (Vector2.right, _playerPosition - HousePosition2d) + 180;
				
				//				Debug.Log (HousePosition2d);
				//				Debug.Log (new Vector2(0, _player.baseposY));
				//				Debug.Log (angle);
				
			}
			else {
				//If we can't find any houses, 5% probability that we will lock the tower.
				if (Random.value < 0.05) {
//					if (_foundEnemySpawnerLocation) {
//						angle = Vector2.Angle (Vector2.right, _playerPosition - _enemySpawnerLocation) + 180;
//						finalLocked = true;
//					}
//					else {
						angle = Vector2.Angle (Vector2.right, _playerPosition - furthestLocationFound) + 180;
//					}
					Locked = true;
				}
				//Lock to furthest distance location
			}
			

			_player.tokenManager.Destroy(baseTower);
			baseTower = null;			
			baseTower = _player.tokenManager.createNewTokenAI (new Vector3 (_playerPosition.x, _playerPosition.y, 0), angle);
			
		}
	}

	private class BeardedDemonAIStrategy : AIStrategy {
		public BeardedDemonAIStrategy(Player player) : base(player)
		{}

//		private static Vector2 objectLocation(GameObject go)

		private List<RotatingTower> towers = new List<RotatingTower>();

		public override void pulse ()
		{
			//Only act every 20 ticks or so for sanity
			if (Random.value > 0.05)
				return;

			if (towers.Count == 0) {
				towers.Add(new RotatingTower(_player, new Vector2 (0, _player.baseposY )));
			}

			RotatingTower lastTower = towers [towers.Count - 1];
			// && lastTower.getLockedDirection().y < -11
			if (lastTower.Locked && !lastTower.finalLocked && towers.Count <= 15) {
				towers.Add (new RotatingTower(_player, towers[towers.Count - 1].getLockedDirection()));
			}

			foreach (RotatingTower t in towers) {
				t.rotateTower();
			}

//			findClosestGameObject (new Vector3 (0, _player.baseposY, 0), -Vector2.up);
		}
	}

	private class RandomArrowAIStrategy : AIStrategy {
		private bool basearrow = false;
		private float timer;

		public RandomArrowAIStrategy(Player player) : base(player)
		{}
		
		public override void pulse ()
		{
			if (basearrow == false) {
				Vector3 objectPos = new Vector3 (0, _player.baseposY, 0); //error on this line
				_player.tokenManager.createNewTokenAI (objectPos, Random.Range(0,360));
				timer = 0f;
				basearrow = true;
				
			}
			
			timer += Time.deltaTime;
			
			if (timer >= 2f) {
				float xpos = Random.Range(Global.left,Global.right);
				float ypos = Random.Range(Global.bottom,Global.top);
				
				//Debug.Log (xpos);
				Random.Range(0,360);
				Vector3 objectPos = new Vector3 (xpos, ypos, 0); //error on this line
				_player.tokenManager.createNewTokenAI (objectPos, Random.Range(0,360));
				timer = 0f;
			}
		}
	}

	void Start () {
		if (HumanPlayer == false) {

			Debug.Log ("A non-human just started playing!!!");

			ai = new BeardedDemonAIStrategy(this);
		}

		//tag = "Player";

		
	}

	public bool ifHumanPlayer() {
		return PlayerNumber == 0;
	}


//
//	public void AIBeast(){
//		if (basearrow == false) {
//			Vector3 objectPos = new Vector3 (0, baseposY, 0); //error on this line
//			tokenManager.createNewTokenAI (objectPos);
//			timer = 0f;
//			basearrow = true;
//
//		}
//
//		timer += Time.deltaTime;
//		
//		if (timer >= 2f) {
//			float xpos = Random.Range(Global.left,Global.right);
//			float ypos = Random.Range(Global.bottom,Global.top);
//
//			//Debug.Log (xpos);
//			Random.Range(0,360);
//			Vector3 objectPos = new Vector3 (xpos, ypos, 0); //error on this line
//			tokenManager.createNewTokenAI (objectPos);
//			timer = 0f;
//		}
//
//	}


	//public List<Token>  getTokenList() {
	//	return tokenList;
	//}
	
	// Update is called once per frame
	void Update () {
		sorrow += sorrowgen;
		if (tokenManager == null) tokenManager = new TokenManager (this, token);
		tokenManager.trimOldTokens (maxTokensLimit);
		if (HumanPlayer == false) {
			//Debug.Log ("AI exists");
//			AIBeast ();
			ai.pulse();
		}

//		if (this.HumanPlayer == false) {
//			Debug.Log ("AI expect player 1: " + PlayerNumber);
//
//		}

	}



}
