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
    public float timeOut = 2f;
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
            shoot();
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

        for (int i = 0;i < 8;i++){
            Vector3 ballPos = new Vector3(enemyPos.x+Mathf.Cos(Mathf.PI/4*i)*2/3, enemyPos.y+Mathf.Sin(Mathf.PI/4*i)*2/3, enemyPos.z);;
            GameObject shot = Instantiate(enemyBallPrefab, ballPos, transform.rotation) as GameObject;
            shot.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            // Shotスクリプトオブジェクトを取得
            BallController s = shot.GetComponent<BallController>();
            s.enemyShoot(enemyPos, 45f*i, 2f);
		    //s.ChangeSprite();
            Debug.Log(i + ": Mathf.Cos(Mathf.PI/4*i) = " + Mathf.Cos(Mathf.PI/4*i) + " : Mathf.Sin(Mathf.PI/4*i) = " + Mathf.Sin(Mathf.PI/4*i));
        }
	}
}
