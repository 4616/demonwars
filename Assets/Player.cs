using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List<Token> tokenList = new List<Token>();

	public float sorrow;
	// Use this for initialization
	void Start () {
		tag = "Player";
	
	}

	//public List<Token>  getTokenList() {
	//	return tokenList;
	//}
	
	// Update is called once per frame
	void Update () {
	
	}



}
