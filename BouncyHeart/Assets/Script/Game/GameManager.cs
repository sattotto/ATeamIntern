﻿using System.Collections;
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
		for (int i = 0; i < Const.ENEMY_NUM [0]; i++) {
			enemySpown (Const.ENEMY_POS, i);
		}
		playerSpown ();
	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
			gameOverText.enabled = true;
		}
	}

	void enemySpown(Vector3 enemypos, int i){

		//GameObject enemy = Instantiate (EnemyPrefab, enemypos, transform.rotation) as GameObject;
		GameObject enemy = Instantiate (EnemyPrefab, new Vector3(enemypos.x + Const.ENEMY_POS_X[i], enemypos.y + Const.ENEMY_POS_Y[i], enemypos.z), transform.rotation) as GameObject;
		enemy.transform.parent = this.transform;
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
