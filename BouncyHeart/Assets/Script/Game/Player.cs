using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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

    Vector3 prevPos = new Vector3(0, 0, 0);
    float prevRot;

    public static Vector2 vector;

    //Animator
    Animator anim;

    //アニメーション変更用変数
    int dir;

    //--------------------------------------//
    // ココダマ
    //--------------------------------------//
    //配列
    public int[] reloadBall = new int[5];
    int shootNum = 0;

    //描画用
    GameObject[] Next;
    SpriteRenderer NextSpriteRenderer;

    //ココダマのイラスト一覧
    public Sprite Kokonoha;
    public Sprite Fulost;
    public Sprite Milky;
    public Sprite King;
    public Sprite Ai;






    // Use this for initialization
    void Start()
    {
        // カメラオブジェクトを取得します
        GameObject obj = GameObject.Find("Main Camera");
        _mainCamera = obj.GetComponent<Camera>();

        //Animatorをキャッシュ
        anim = GetComponent<Animator>();


        // 座標値を出力
        ////Debug.Log (getScreenTopLeft ().x + ", " + getScreenTopLeft ().y);
        ////Debug.Log (getScreenBottomRight ().x + ", " + getScreenBottomRight ().y);

        PLAYER_HP_MAX = Const.PLAYER_HP;
        playerHP = PLAYER_HP_MAX;

        shootMax = Const.SHOOT_NUM;

        //弾のイラスト設定
        KokodamaSpriteInitialize();
        //弾の配列を入れる
        BallReset();
        NextRender();



    }

    // Update is called once per frame
    void Update()
    {
        if (!isReload)
        {
            moveKeyboard();
        }
        else
        {
            Invoke("flgChange", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isReload)
        {
            shoot();
        }
        checkPlayerRotation();
        //方向をセット
        anim.SetInteger("dir", dir);


    }

    void moveKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && fieldLeft < transform.position.x)
        {
            transform.Translate(-Const.SPEED[GameSpeedButton.speedCount], 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) && fieldRight > transform.position.x)
        {
            transform.Translate(Const.SPEED[GameSpeedButton.speedCount], 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) && fieldTop > transform.position.y)
        {
            transform.Translate(0, Const.SPEED[GameSpeedButton.speedCount], 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) && fieldBottom < transform.position.y)
        {
            transform.Translate(0, -Const.SPEED[GameSpeedButton.speedCount], 0);
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
        if (shootMax == 0)
        {
            isReload = true;
        }
        //Instantiate (ballPrefab, transform.position, Quaternion.identity);
        // 弾を生成
        Vector3 PlayerPos = transform.position;
        Vector3 ballPos = new Vector3(PlayerPos.x + vector.x / 3, PlayerPos.y + vector.y / 3, PlayerPos.z);
        GameObject shot = Instantiate(ballPrefab, ballPos, transform.rotation) as GameObject;
        // Shotスクリプトオブジェクトを取得
        BallController s = shot.GetComponent<BallController>();
        // 移動速度を設定
        s.Create(prevRot, 3f);
        //打つボールを取得（配列から１つ取り出す）
        ShootBall();
        //次に打つボールのセット
        BallLoad();
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
        ////Debug.Log (playerHP);
    }

    void checkPlayerRotation()
    {
        float x = this.transform.position.x - prevPos.x;
        float y = this.transform.position.y - prevPos.y;

        Vector2 vec = new Vector2(x, y).normalized;
        vector = vec;

        float xx = prevPos.x - this.transform.position.x;
        float yy = prevPos.y - this.transform.position.y;
        Vector2 move_vec = new Vector2(xx, yy).normalized;

        //アニメーション用
        MoveAngle(move_vec);

        ////Debug.Log (vector);

        float rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;

        if (rot > 180) rot -= 360;
        if (rot < -180) rot += 360;

        if (prevRot != rot)
        {
            //			////Debug.Log ("Angle = " + rot);
            prevRot = rot;
            prevPos = this.transform.position;
        }
    }

    void flgChange()
    {
        isReload = false;
        shootMax = 5;
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

        ////Debug.Log(angle);

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



    //弾の配列をリセット
    //打ったら、配列をずらす
    void BallLoad()
    {
        shootNum += 1;
        if (shootNum > 4)
        {
            BallReset();
        }
        NextRender();

    }
    void BallReset()
    {
        for (int i = 0; i < 5; i++)
        {
            reloadBall[i] = Random.Range(0, 4);

            Debug.Log(reloadBall[i]);
        }
        //取り出し用変数を初期化
        shootNum = 0;
    }


    int ShootBall()
    {
        Debug.Log("打つ");
        Debug.Log(reloadBall[shootNum]);
        return reloadBall[shootNum];
    }

    void KokodamaSpriteInitialize()
    {
        //------------------------------------------------------------------------//
        // 取得した際の画像変更のために、それぞれのオブジェクトを取得しておく     //
        //------------------------------------------------------------------------//

        Next = GameObject.FindGameObjectsWithTag("NextKokodama");

        // このobjectのSpriteRendererを取得
        NextSpriteRenderer = Next[0].GetComponent<SpriteRenderer>();
    }

    void NextRender()
    {

        switch (reloadBall[shootNum])
        {
            case 0:
                NextSpriteRenderer.sprite = Kokonoha;
                break;
            case 1:
                NextSpriteRenderer.sprite = Fulost;
                break;
            case 2:
                NextSpriteRenderer.sprite = Milky;
                break;
            case 3:
                NextSpriteRenderer.sprite = King;
                break;
            case 4:
                NextSpriteRenderer.sprite = Ai;
                break;
        }


    }

    //Kokonoha;
    //Fulost;
    //Milky;
    //King;
    //Ai;
}
