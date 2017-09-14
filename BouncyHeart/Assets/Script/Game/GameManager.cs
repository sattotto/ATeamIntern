using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Text gameOverText;
	public static bool gameOver = false;

	public GameObject EnemyPrefab;
	public GameObject PlayerPrefab;

	// Use this for initialization
	void Start () {
		enemySpown (Const.ENEMYPOS);
		playerSpown ();
	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
			gameOverText.enabled = true;
		}
	}

	void enemySpown(Vector3 enemypos){
		//Vector3 enemypos = new Vector3 (0, 4, 0);
		//Instantiate (EnemyPrefab, enemtpos, Quaternion.identity);
		GameObject enemy = Instantiate (EnemyPrefab, enemypos, transform.rotation) as GameObject;
		// この時、objのlocalScaleは ( 1, 1, 1 )で、parentはrootになっている
		enemy.transform.parent = this.transform;
		// この時、objのlocalScaleはparentに応じて変更されている
		enemy.transform.localScale = Vector3.one;
	}

	void playerSpown(){
		Vector3 playerpos = new Vector3 (0, 0, 0);
		GameObject player = Instantiate (PlayerPrefab, playerpos, transform.rotation) as GameObject;
		player.transform.parent = this.transform;
		player.transform.localScale = Vector3.one;
		player.name = PlayerPrefab.name;
	}
}
