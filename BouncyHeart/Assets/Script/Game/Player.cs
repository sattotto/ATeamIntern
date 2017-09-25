using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    private Camera _mainCamera;

    public static int PLAYER_HP_MAX = Const.PLAYER_HP;
    public static int playerHP;

    public GameObject ballPrefab;

    public float fieldTop;
    public float fieldBottom;
    public float fieldLeft;
    public float fieldRight;

    int shootMax;
    bool isReload = false;
    private bool isCall = false;

    Vector3 prevPos = new Vector3(0, 0, 0);
    Vector2 movePos;
    float rot;
    float prevRot;

    public static float charge = 1f;

    public static Vector2 vector;
    public static Vector3 playerPos;

    //Animator
    Animator anim;

    //アニメーション変更用変数
    int dir;

    bool push = false;
    bool push_tmp = false;

    private float nextTime;
    public float interval = 0.0001f;	// 点滅周期
    // Use this for initialization
    void Start()
    {
        // カメラオブジェクトを取得します
        GameObject obj = GameObject.Find("Main Camera");
        _mainCamera = obj.GetComponent<Camera>();

        //Animatorをキャッシュ
        anim = GetComponent<Animator>();

        nextTime = Time.time;

        // 座標値を出力
        //Debug.Log (getScreenTopLeft ().x + ", " + getScreenTopLeft ().y);
        //Debug.Log (getScreenBottomRight ().x + ", " + getScreenBottomRight ().y);

        PLAYER_HP_MAX = Const.PLAYER_HP;
        playerHP = PLAYER_HP_MAX;

        shootMax = Const.SHOOT_NUM;

        Reload reload = FindObjectOfType<Reload>();

        //弾のイラスト設定
        reload.KokodamaSpriteInitialize();
        //弾の配列を入れる
        reload.BallReset();
        reload.KokodamaRender();
        //NextRender();

        isCall = false;
        isReload = false;

    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerRotation();
        //方向をセット
        anim.SetInteger("dir", dir);
        anim.SetBool("reload", isReload);

        Skill skill = FindObjectOfType<Skill>();

        //BGM操作
        if (skill.kingSkill)
        {
            BgmController.ChangeBgm(1);
        }
        else
        {
            BgmController.ChangeBgm(0);
        }

        if (skill.ready)
        {
            if (Time.time > nextTime)
            {
                GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

                nextTime += interval;
            }
        }
        if(!skill.ready)
        {
            GetComponent<Renderer>().enabled = true;
        }

        if (!GameManager.kingNotEffect)
        {
            if (!isReload)
            {
                //moveKeyboard();
                moveTouch();
            }
            else
            {
                if (!isCall)
                {
                    Invoke("flgChange", 1f);
                }
                isCall = true;
            }
            push_tmp = false;
			if (Input.GetMouseButton(0) && !isReload)
			{
				//クリックして、オブジェクトがあったら
				GameObject obj = getClickObject();
                push_tmp = true;
				if (obj != null)
				{
					if (obj.name == "TapZone")
					{
                        PushDown();
					}
				}
			}
            if (push_tmp){
                chargeGauge();
            }
            if (!push_tmp && push){
                PushUp();
            }
            playerPos = transform.position;
        }

    }

    void moveKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && fieldLeft < transform.position.x)
        {
            transform.Translate(-Const.SPEED[GameSpeedButton.speedCount], 0, 0);
            movePos += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) && fieldRight > transform.position.x)
        {
            transform.Translate(Const.SPEED[GameSpeedButton.speedCount], 0, 0);
            movePos += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) && fieldTop > transform.position.y)
        {
            transform.Translate(0, Const.SPEED[GameSpeedButton.speedCount], 0);
            movePos += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow) && fieldBottom < transform.position.y)
        {
            transform.Translate(0, -Const.SPEED[GameSpeedButton.speedCount], 0);
            movePos += new Vector2(0, -1);
        }
    }

    void moveTouch(){
        
        var x = CrossPlatformInputManager.GetAxis("Horizontal");
        var y = CrossPlatformInputManager.GetAxis("Vertical");
		if (fieldLeft < transform.position.x && x < 0){
            transform.position += (Vector3.right * x) * Time.deltaTime;
		}
        if (transform.position.x < fieldRight && x > 0){
            transform.position += (Vector3.right * x) * Time.deltaTime;
        }
        if (fieldBottom < transform.position.y && y < 0){
            transform.position += (Vector3.up * y) * Time.deltaTime;
        }
        if(transform.position.y < fieldTop && y > 0){
            transform.position += (Vector3.up * y) * Time.deltaTime;
        }
        if(x != 0 && y != 0){
            movePos = new Vector2(x, y);
        }


    }

    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = _mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 getScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }

    void shoot()
    {
        shootMax -= 1;
        Debug.Log("shoot");
        isCall = false;
        if (shootMax < 1)
        {
            isReload = true;
        }
        Reload reload = FindObjectOfType<Reload>();
        Skill skill = FindObjectOfType<Skill>();

        if (skill.ready)
        {
            GameObject KingPanel = Instantiate(skill.KingPanelPrefab, new Vector3(50, 0, 1), transform.rotation) as GameObject;
            skill.kingSkill = true;
            skill.ready = false;
        }
        else
        {
            if (shootMax <= 0)
            {
                if (skill.kingSkill)
                {
                    isReload = false;
                    shootMax = Const.SHOOT_NUM;
                }
                else
                {
                    isReload = true;
                }
            }
            //Instantiate (ballPrefab, transform.position, Quaternion.identity);
            // 弾を生成
            Vector3 PlayerPos = transform.position;
            Vector3 ballPos = new Vector3(PlayerPos.x + vector.x / 3, PlayerPos.y + vector.y / 3, PlayerPos.z);
            GameObject shot = Instantiate(ballPrefab, ballPos, transform.rotation) as GameObject;
            // Shotスクリプトオブジェクトを取得
            BallController s = shot.GetComponent<BallController>();

            //打つボールを取得（配列から１つ取り出す）
            int BallId = reload.ShootBall();

            s.ballType(BallId, charge, rot);

            //ボールのテクスチャを変更
            s.ChangeSprite(reload.BallSptite(BallId));
            //次に打つボールのセット
            reload.BallLoad();
        }
    }

    public static void PlayerDamaged(int damage)
    {
        setPlayerHP(playerHP - damage);
        if (playerHP <= 0)
        {
            // がめおべら
            //Debug.Log ("you died!");
            GameManager.gameOver = true;
        }
    }

    public static void PlayerHealed(int heal)
    {
        int setHP = System.Math.Min(PLAYER_HP_MAX, playerHP + heal);
        setPlayerHP(setHP);
    }

    public static void setPlayerHP(int HP)
    {
        playerHP = HP;
        //Debug.Log (playerHP);
    }

    void checkPlayerRotation()
    {
        float x = this.transform.position.x - prevPos.x;
        float y = this.transform.position.y - prevPos.y;

        Vector2 vec = new Vector2(x, y).normalized;

        float xx = prevPos.x - this.transform.position.x;
        float yy = prevPos.y - this.transform.position.y;
        Vector2 move_vec = new Vector2(xx, yy).normalized;

        Debug.Log(move_vec);

        //アニメーション用
        MoveAngle(movePos);
        //Debug.Log("move_vec = " + move_vec + ": prevMoveVec = " + movePos);
        rot = Mathf.Atan2(movePos.y, movePos.x) * 180 / Mathf.PI;

        if (rot > 180) rot -= 360;
        if (rot < -180) rot += 360;

        if (prevRot != rot)
        {
            prevRot = rot;
            prevPos = this.transform.position;
        }
    }

    void flgChange()
    {
        isReload = false;
        shootMax = Const.SHOOT_NUM;
        //配列をリセット
        //BallReset();
        //Debug.Log ("now reloarding!");
    }

    //--------------------------------------------------------//
    // アニメーションのstateメモ
    // ０：UP　１：DOWN　２：LEFT　３：RIGHT
    // ４：LEFTUP　５：RIGHTUP　６：LEFTDOWN　７：RIGHTDOWN
    //--------------------------------------------------------//
    void MoveAngle(Vector2 player_dir)
    {
        // 自分とターゲットとなる相手との方向を求める
        Vector2 direction = player_dir;
        //角度を求める
        float angle = Mathf.Atan2(-direction.y, -direction.x);
        angle *= Mathf.Rad2Deg;
        angle = (angle + 360.0f) % 360.0f;

        Debug.Log(angle);

        //角度から向いている方向を判断し、アニメーションフラグを変更する
        if ((angle > 337.5f) || (angle < 22.5f))
        {
            //Debug.Log("右に動く");
            dir = 3;
        }
        else
        {
            if ((angle >= 22.5f) && (angle <= 67.5f))
            {
                //Debug.Log("右上に動く");
                dir = 5;
            }
            else
            {
                if ((angle > 67.5f) && (angle < 112.5f))
                {
                    //Debug.Log("上に動く");
                    dir = 0;
                }
                else
                {
                    if ((angle > 112.5f) && (angle < 157.5f))
                    {
                        //Debug.Log("左上に動く");
                        dir = 4;
                    }
                    else
                    {
                        if ((angle > 157.5f) && (angle < 202.5f))
                        {
                            //Debug.Log("左に動く");
                            dir = 2;
                        }
                        else
                        {
                            if ((angle > 202.5f) && (angle < 247.5f))
                            {
                                //Debug.Log("左下に動く");
                                dir = 6;
                            }
                            else
                            {
                                if ((angle > 247.5f) && (angle < 292.5f))
                                {
                                    //Debug.Log("下に動く");
                                    dir = 1;
                                }
                                else
                                {
                                    //Debug.Log("右下に動く");
                                    dir = 7;
                                }

                            }

                        }

                    }
                }
            }
        }

    }
	// 左クリックしたオブジェクトを取得する関数(2D)
	private GameObject getClickObject()
	{
		GameObject result = null;
		// 左クリックされた場所のオブジェクトを取得
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
			if (collition2d)
			{
				result = collition2d.transform.gameObject;
			}
		}
		return result;
	}

	public void PushDown()
	{
        Debug.Log("push down!");
		push = true;
	}

	public void PushUp()
	{
        
        push = false;
        anim.SetTrigger("throw");
        Debug.Log("push up! " + charge);
        shoot();
        charge = 1;
	}

    public void chargeGauge(){
        charge += 0.01f;
    }
}
