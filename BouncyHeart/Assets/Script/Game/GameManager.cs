using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Text gameOverText;
	public static bool gameOver = false;

	public GameObject EnemyPrefab;
	public GameObject PlayerPrefab;

	public static int EnemyNum;
	public static bool isClear = false;

    public static bool kingNotEffect = false;

    // Use this for initialization
    void Start () {
		Invoke("gameStart",2f);
	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
			gameOverText.enabled = true;
		}
		if (isClear) {
			Debug.Log ("game clear!");
		}
	}

	void gameStart(){
		for (int i = 0; i < Const.ENEMY_NUM [0]; i++) {
			enemySpawn (Const.ENEMY_POS, i);
		}
		playerSpawn ();
		EnemyNum = Const.ENEMY_NUM[0];
	}

	void enemySpawn(Vector3 enemypos, int i){

		//GameObject enemy = Instantiate (EnemyPrefab, enemypos, transform.rotation) as GameObject;
		GameObject enemy = Instantiate (EnemyPrefab, new Vector3(enemypos.x + Const.ENEMY_POS_X[i], enemypos.y + Const.ENEMY_POS_Y[i], enemypos.z), transform.rotation) as GameObject;
		enemy.transform.parent = this.transform;
		enemy.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
	}

	void playerSpawn(){
		Vector3 playerpos = new Vector3 (0, 0, 0);
		GameObject player = Instantiate (PlayerPrefab, playerpos, transform.rotation) as GameObject;
		player.transform.parent = this.transform;
		player.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		player.name = PlayerPrefab.name;
	}

	public static void clearCheck(){
		if (EnemyNum == 0) {
			isClear = true;
		}
	}
}
