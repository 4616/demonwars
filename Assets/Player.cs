using UnityEngine;
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
	private float timer;

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

		public void createNewTokenAI(Vector3 objectPos) {
			//objectPos.z = -5;
			GameObject go = Instantiate(token, objectPos, Quaternion.identity) as GameObject;
			
			Token tk = go.GetComponent<Token>();
			tk.Click ();
			float rangle = Random.Range(0,360);
			tk.AIToken (rangle);
			//tk.init(owner);
			tk.TakeOwnership (owner);
			tokenList.Insert(0,tk);
			
		}

		public Token getActiveToken() {
			if (tokenList.Count > 0) {
				Token activeToken = tokenList[0];
				if (activeToken.Active()) return activeToken;
			}
			return null;
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
					tk.Destroy();
					Instantiate (tk.explosion, tk.transform.position, tk.transform.rotation);
				}
				tokenList = tokenList.GetRange (0, maxTokens);
			}
		}
	}
	void Start () {
		//tag = "Player";

		
	}


	public void AIBeast(){
//		timer += Time.deltaTime;
//		
//		if (timer >= 10f) {
//			Vector3 objectPos = new Vector3 (1, 14, 0); //error on this line
//			tokenManager.createNewTokenAI (objectPos);
//			timer = 0f;
//		}

	}


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
			AIBeast ();
		}

	}



}
