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

    // ball発射の周期
    public float timeOut = 5f;
    private float timeElapsed;

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
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= timeOut) {
            // Do anything

            timeElapsed = 0.0f;
            Invoke("shoot", 2f);
        }
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
		Vector3 enemyPos = transform.position;

		Vector3 ballPos = new Vector3(enemyPos.x + enemyPos.x/2f, enemyPos.y + enemyPos.y/2f, enemyPos.z);;
        GameObject shot = Instantiate(enemyBallPrefab, ballPos, transform.rotation) as GameObject;
        shot.transform.localScale = new Vector3(1f, 1f, 1f);
        // Shotスクリプトオブジェクトを取得
        BallController s = shot.GetComponent<BallController>();
        s.enemyShoot(enemyPos, 90f, 2f);
		//s.ChangeSprite();
	}
}
