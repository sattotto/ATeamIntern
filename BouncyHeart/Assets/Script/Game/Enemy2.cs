using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour {

	public GameObject enemyBallPrefab;
    public GameObject target;

    public int flg = 0;
    Vector3 knockBackDirection = new Vector3(0, 0, 0);
    public Vector3 tmp;

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

        playerContact();
	}

    private void OnTriggerEnter(Collider other)
    {
        //tagがplayerなら
        if (other.gameObject.tag == "player") {
            // 体当たりしてきた敵とプレイヤーの座標からノックバックする方向を取得する
            knockBackDirection = (this.transform.position - other.transform.position).normalized;

            //フラグを1にする
            flg = 1;

            Player.PlayerDamaged(Const.ENEMY_ATK);

            //0.5秒後にフラグを2にする
            Invoke("flgChange", 0.1f);
        }
        // tagがballなら
        if (other.gameObject.tag == "ball") {

			// 体当たりしてきた敵とプレイヤーの座標からノックバックする方向を取得する
			knockBackDirection = (this.transform.position - other.transform.position).normalized;
			//フラグを1にする
			flg = 1;
			//0.5秒後にフラグを2にする
			Invoke("flgChange", 0.1f);

            Destroy(other.gameObject);
            EnemyDamaged(Const.BALL_ATK[0]);
            //Debug.Log ("test");
        }
    }

	void EnemyDamaged(int damage) {
        setEnemyHP(enemyHP - damage);
        if (enemyHP <= 0 && !isDead)
        {
            GameManager.EnemyNum -= 1;
            GameManager.clearCheck();
            escape = true;
            isDead = true;
            Destroy(this.gameObject);
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

    void playerContact(){
        //衝突したら
        float knockBackSpeed = 0.15f;
        if (flg == 1){
            //ノックバックさせる
            float dx = transform.position.x + (knockBackSpeed * knockBackDirection.x);
            float dy = transform.position.y + (knockBackSpeed * knockBackDirection.y);
            // Field内に移動しているかのチェック
            if (dx < getField(2)) {
                dx = getField(2);
            } else if (dx > getField(1)) {
                dx = getField(1);
            }
            if (dy > getField(0)) {
                dy = getField(0);
            } else if (dy < getField(3)) {
                dy = getField(3);
            }

            transform.position = new Vector3(dx, dy, transform.position.z);
        } else if (flg == 2) {
            transform.position = tmp;
        }
    }

    float getField(int i)
    {
        switch (i)
        {
            case 0: // 画面上
                return target.GetComponent<Player>().fieldTop;
            case 1: // 画面右
                return target.GetComponent<Player>().fieldRight;
            case 2: // 画面左
                return target.GetComponent<Player>().fieldLeft;
            case 3: // 画面下
                return target.GetComponent<Player>().fieldBottom;
            default:
                return 0;
        }
    }
    
}
