using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public int flg = 0;
    public Vector3 tmp;
    float knockBackSpeed = 0.5f;
    Vector3 knockBackDirection = new Vector3(0, 0, 0);

    //Animator
    Animator anim;


    //アニメーションのフラグ
    int dir = 0;

	public int ENEMY_HP_MAX = Const.ENEMY_HP;
	public int enemyHP;

    // Use this for initialization
    void Start()
    {
		ENEMY_HP_MAX = Const.ENEMY_HP;
		enemyHP = ENEMY_HP_MAX;
        //Animatorをキャッシュ
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //方向取得
        MoveAngle();
        //方向をセット
        anim.SetInteger("dir", dir);


        //衝突していなかったら
        if (flg == 0)
        {
            //プレイヤーに追従する処理
            this.transform.position=Vector3.MoveTowards(this.transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 1f * Time.deltaTime);
        } 
        //衝突したら
        if(flg == 1)
        {
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
			Debug.Log (transform.position.z);
            Debug.Log("ノックバック！");
        }
        if (flg == 2)
        {
            transform.position = tmp;
        }
    }

    //衝突したら
    private void OnTriggerEnter(Collider other)
    {
        //tagがplayerなら
        if (other.gameObject.tag == "player")
        {
            // 体当たりしてきた敵とプレイヤーの座標からノックバックする方向を取得する
            knockBackDirection = (this.transform.position - other.transform.position).normalized;

            //フラグを1にする
            flg = 1;

			Player.PlayerDamaged (1);

            //0.5秒後にフラグを2にする
            Invoke("flgChange",0.1f);
        }
		// tagがballなら
		if (other.gameObject.tag == "ball") {
			Destroy (other.gameObject);
			EnemyDamaged (1);
			Debug.Log ("test");
		}
    }


    //待機モーション
    void StopMotion()
    {
        Debug.Log("stop!");

        //2秒後にflgを0
        Invoke("flgChange2",2f);
    }

    //フラグをfalseにする
    void flgChange()
    {
        //フラグをfalse
        flg = 2;

        //現在位置を保有
        ////tmp = agent.transform.position;
        tmp = transform.position;

        //待機モーションをいれる
        StopMotion();

    }
    void flgChange2()
    {
        flg = 0;
    }

	float getField (int i){
		switch(i){
		case 0: // 画面上
			return target.GetComponent<Player> ().fieldTop;
		case 1: // 画面右
			return target.GetComponent<Player> ().fieldRight;
		case 2: // 画面左
			return target.GetComponent<Player> ().fieldLeft;
		case 3: // 画面下
			return target.GetComponent<Player> ().fieldBottom;
		default:
			return 0;
		}
	}

    //--------------------------------------------------------//
    // アニメーションのstateメモ
    // ０：UP　１：DOWN　２：LEFT　３：RIGHT
    // ４：LEFTUP　５：RIGHTUP　６：LEFTDOWN　７：RIGHTDOWN
    //--------------------------------------------------------//
    void MoveAngle()
    {
        // 自分とターゲットとなる相手との方向を求める
        Vector3 direction = (this.transform.position - target.transform.position).normalized;
        //角度を求める
        float angle = Mathf.Atan2(-direction.y, -direction.x);
        angle *= Mathf.Rad2Deg;
        angle = (angle + 360.0f) % 360.0f;

        Debug.Log(angle);

        //角度から向いている方向を判断し、アニメーションフラグを変更する
        if ((angle > 337.5f) || (angle < 22.5f))
        {
            Debug.Log("右に動く");
            dir = 3;
        }
        else
        {
            if ((angle >= 22.5f) && (angle <= 67.5f))
            {
                Debug.Log("右上に動く");
                dir = 5;
            }
            else
            {
                if ((angle > 67.5f) && (angle < 112.5f))
                {
                    Debug.Log("上に動く");
                    dir = 0;
                }
                else
                {
                    if ((angle > 112.5f) && (angle < 157.5f))
                    {
                        Debug.Log("左上に動く");
                        dir = 4;
                    }
                    else
                    {
                        if ((angle > 157.5f) && (angle < 202.5f))
                        {
                            Debug.Log("左に動く");
                            dir = 2;
                        }
                        else
                        {
                            if ((angle > 202.5f) && (angle < 247.5f))
                            {
                                Debug.Log("左下に動く");
                                dir = 6;
                            }
                            else
                            {
                                if ((angle > 247.5f) && (angle < 292.5f))
                                {
                                    Debug.Log("下に動く");
                                    dir = 1;
                                }
                                else
                                {
                                    Debug.Log("右下に動く");
                                    dir = 7;
                                }

                            }

                        }

                    }
                }
            }
        }

    }


	void EnemyDamaged(int damage){
		setEnemyHP (enemyHP - damage);
		if (enemyHP <= 0) {
			Destroy (this.gameObject);
		}
	}

	void EnemyHealed(int heal) {
		int setHP = System.Math.Min (ENEMY_HP_MAX, enemyHP + heal);
		setEnemyHP (setHP);
	}

	void setEnemyHP(int HP){
		enemyHP = HP;
		Debug.Log (enemyHP);
	}
}