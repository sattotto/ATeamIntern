using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Text gameOverText;
	public static bool gameOver = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
			gameOverText.enabled = true;
		}
	}


}
