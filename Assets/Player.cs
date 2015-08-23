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
	public float baseposY;
	private bool basearrow = false;
	
	
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
			if (owner.HumanPlayer)
				return;
			//objectPos.z = -5;
			GameObject go = Instantiate(token, objectPos, Quaternion.identity) as GameObject;
			
			Token tk = go.GetComponent<Token>();
			tk.Click ();
			float rangle = Random.Range(0,360);
			tk.AIToken (rangle);
			//tk.init(owner);
			tk.TakeOwnership (owner);
			Debug.Log ("Expecting AI PlayerNumber 1, got :" + owner.PlayerNumber);
			//Debug.Log (owner.PlayerNumber);

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
		if (HumanPlayer == false) {

			Debug.Log ("A non-human just started playing!!!");
		}

		//tag = "Player";

		
	}

	public bool ifHumanPlayer() {
		return PlayerNumber == 0;
	}



	public void AIBeast(){
		if (basearrow == false) {
			Vector3 objectPos = new Vector3 (0, baseposY, 0); //error on this line
			tokenManager.createNewTokenAI (objectPos);
			timer = 0f;
			basearrow = true;

		}

		timer += Time.deltaTime;
		
		if (timer >= 100f) {
			float xpos = Random.Range(Global.left,Global.right);
			float ypos = Random.Range(Global.bottom,Global.top);

			//Debug.Log (xpos);
			Random.Range(0,360);
			Vector3 objectPos = new Vector3 (xpos, ypos, 0); //error on this line
			tokenManager.createNewTokenAI (objectPos);
			timer = 0f;
		}

	}


	//public List<Token>  getTokenList() {
	//	return tokenList;
	//}
	
	// Update is called once per frame
	void Update () {
		sorrow += sorrowgen;
		if (tokenManager == null) tokenManager = new TokenManager (this, token);
		tokenManager.trimOldTokens (maxTokensLimit);
		if (HumanPlayer) {
			//Debug.Log ("AI exists");
//			AIBeast ();
		}

//		if (this.HumanPlayer == false) {
//			Debug.Log ("AI expect player 1: " + PlayerNumber);
//
//		}

	}



}
