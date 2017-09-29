using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
	
    public GameObject gameOverText;
	public static bool gameOver = false;

	public GameObject EnemyPrefab;
	public GameObject PlayerPrefab;

    private GameObject player;
    private GameObject[] enemy = new GameObject[16];

	public static int EnemyNum;
	public static bool isClear = false;

    public static bool kingNotEffect = false;

    // Use this for initialization
    void Start () {
        setupStage();
		Invoke("gameStart",2f);
	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
            gameOverText.SetActive(true);
            Destroy(player.gameObject);
            for (int i = 0; i < Const.ENEMY_NUM[0];i++){
                Destroy(enemy[i]);
            }
		}
		if (isClear) {
			Debug.Log ("game clear!");
            Destroy(player);

            SceneManager.LoadScene("Result");
        }
	}

	void gameStart(){
		for (int i = 0; i < Const.ENEMY_NUM [0]; i++) {
			enemySpawn (Const.ENEMY_POS, i);
		}
        playerSpawn();
	}

    void setupStage(){
        isClear = false;
        EnemyNum = Const.ENEMY_NUM[0];
        gameOver = false;
        kingNotEffect = false;
    }

	void enemySpawn(Vector3 enemypos, int i){

		//GameObject enemy = Instantiate (EnemyPrefab, enemypos, transform.rotation) as GameObject;
        enemy[i] = Instantiate (EnemyPrefab, new Vector3(enemypos.x + Const.ENEMY_POS_X[i], enemypos.y + Const.ENEMY_POS_Y[i], enemypos.z), transform.rotation) as GameObject;
        enemy[i].transform.parent = this.transform;
        enemy[i].transform.localScale = new Vector3(1f, 1f, 1f);
	}

	void playerSpawn(){
		Vector3 playerpos = new Vector3 (0, 0, 0);
        player = Instantiate (PlayerPrefab, playerpos, transform.rotation) as GameObject;
		player.transform.parent = this.transform;
		player.transform.localScale = new Vector3(1f, 1f, 1f);
		player.name = PlayerPrefab.name;
	}

	public static void clearCheck(){
		if (EnemyNum == 0) {
			isClear = true;
		}
	}

}
