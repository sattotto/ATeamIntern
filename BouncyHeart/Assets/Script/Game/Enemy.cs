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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    private void OnCollisionEnter(Collision other)
    {
        //tagがplayerなら
        if (other.gameObject.tag == "player")
        {
            // 体当たりしてきた敵とプレイヤーの座標からノックバックする方向を取得する
            knockBackDirection = (this.transform.position - other.transform.position).normalized;

            //フラグを1にする
            flg = 1;

            //0.5秒後にフラグを2にする
            Invoke("flgChange",0.1f);
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
}