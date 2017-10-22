using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour {

	public GameObject enemyBallPrefab;

	public int ENEMY_HP_MAX = Const.ENEMY_HP;
    public int enemyHP;

	bool isDead = false;
	bool escape = false;

	private Slider _HPBar;

	// Use this for initialization
	void Start () {
		// 敵のHPを設定
		ENEMY_HP_MAX = Const.ENEMY_HP;
        enemyHP = ENEMY_HP_MAX;

		_HPBar = transform.Find("EnemyHPBarSlider").GetComponent<Slider>();
        _HPBar.maxValue = enemyHP;
        _HPBar.value = enemyHP;
        Debug.Log("Child is: " + _HPBar.name);
	}
	
	// Update is called once per frame
	void Update () {
		Invoke("shoot", 2f);
	}

	void EnemyDamaged(int damage)
    {
        setEnemyHP(enemyHP - damage);
        if (enemyHP <= 0 && !isDead)
        {
            GameManager.EnemyNum -= 1;
            GameManager.clearCheck();
            escape = true;
            isDead = true;
        }
    }
    
    void setEnemyHP(int HP)
    {
        enemyHP = HP;
        _HPBar.value = enemyHP;
        //Debug.Log (enemyHP);
    }

	void shoot(){
		Vector3 PlayerPos = transform.position;
		Vector3 enemyPos = transform.position;

		Vector3 ballPos = new Vector3(PlayerPos.x + enemyPos.x/2f, PlayerPos.y + enemyPos.y/2f, PlayerPos.z);;
        GameObject shot = Instantiate(enemyBallPrefab, ballPos, transform.rotation) as GameObject;
        // Shotスクリプトオブジェクトを取得
        BallController s = shot.GetComponent<BallController>();
		//s.ChangeSprite();
	}
}
