﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    //--------------------------------------//
    // ココダマ
    //--------------------------------------//
    //配列
    public int[] reloadBall = new int[5];
    int shootNum = 0;

    //描画用
    GameObject[] Ball;
    GameObject[] Next;
    GameObject[] Third;
    GameObject[] Four;
    GameObject[] Five;
    GameObject[] Six;

    SpriteRenderer BallSpriteRenderer;
    SpriteRenderer NextSpriteRenderer;
    SpriteRenderer ThirdSpriteRenderer;
    SpriteRenderer FourSpriteRenderer;
    SpriteRenderer FiveSpriteRenderer;
    SpriteRenderer SixSpriteRenderer;


    //ココダマのイラスト一覧
    public Sprite Kokonoha;
    public Sprite Fulost;
    public Sprite Milky;
    public Sprite King;
    public Sprite Ai;
    public Sprite None;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //弾の配列をリセット
    //打ったら、配列をずらす
    public void BallLoad()
    {
        shootNum += 1;
        if (shootNum > 4)
        {
            BallReset();
        }
        KokodamaRender();
        //NextRender();

    }
    public void BallReset()
    {
        for (int i = 0; i < 5; i++)
        {
            reloadBall[i] = Random.Range(0, 4);

            Debug.Log(reloadBall[i]);
        }
        //取り出し用変数を初期化
        shootNum = 0;
    }


    //打つ球のIDをreturnする
    public int ShootBall()
    {
        Debug.Log("打つ");
        Debug.Log(reloadBall[shootNum]);
        return reloadBall[shootNum];
    }

    public void KokodamaSpriteInitialize()
    {
        //------------------------------------------------------------------------//
        // 取得した際の画像変更のために、それぞれのオブジェクトを取得しておく     //
        //------------------------------------------------------------------------//

        Next = GameObject.FindGameObjectsWithTag("NextKokodama");
        Third = GameObject.FindGameObjectsWithTag("ThirdKokodama");
        Four = GameObject.FindGameObjectsWithTag("FourKokodama");
        Five = GameObject.FindGameObjectsWithTag("FiveKokodama");
        Six = GameObject.FindGameObjectsWithTag("SixKokodama");

        // このobjectのSpriteRendererを取得
        NextSpriteRenderer = Next[0].GetComponent<SpriteRenderer>();
        ThirdSpriteRenderer = Third[0].GetComponent<SpriteRenderer>();
        FourSpriteRenderer = Four[0].GetComponent<SpriteRenderer>();
        FiveSpriteRenderer = Five[0].GetComponent<SpriteRenderer>();
        SixSpriteRenderer = Six[0].GetComponent<SpriteRenderer>();

    }


    public void KokodamaRender()
    {
        NextRender();
        ThirdRender();
        FourRender();
        FiveRender();
        SixRender();
    }
    public void NextRender()
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

    public void ThirdRender()
    {
        if (shootNum <= 3)
        {
            switch (reloadBall[shootNum + 1])
            {
                case 0:
                    ThirdSpriteRenderer.sprite = Kokonoha;
                    break;
                case 1:
                    ThirdSpriteRenderer.sprite = Fulost;
                    break;
                case 2:
                    ThirdSpriteRenderer.sprite = Milky;
                    break;
                case 3:
                    ThirdSpriteRenderer.sprite = King;
                    break;
                case 4:
                    ThirdSpriteRenderer.sprite = Ai;
                    break;
            }
        }
        else
        {
            ThirdSpriteRenderer.sprite = None;
        }
    }
    public void FourRender()
    {
        if (shootNum <= 2)
        {

            switch (reloadBall[shootNum + 2])
            {
                case 0:
                    FourSpriteRenderer.sprite = Kokonoha;
                    break;
                case 1:
                    FourSpriteRenderer.sprite = Fulost;
                    break;
                case 2:
                    FourSpriteRenderer.sprite = Milky;
                    break;
                case 3:
                    FourSpriteRenderer.sprite = King;
                    break;
                case 4:
                    FourSpriteRenderer.sprite = Ai;
                    break;
            }
        }
        else
        {
            FourSpriteRenderer.sprite = None;
        }

    }
    public void FiveRender()
    {
        if (shootNum <= 1)
        {

            switch (reloadBall[shootNum + 3])
            {
                case 0:
                    FiveSpriteRenderer.sprite = Kokonoha;
                    break;
                case 1:
                    FiveSpriteRenderer.sprite = Fulost;
                    break;
                case 2:
                    FiveSpriteRenderer.sprite = Milky;
                    break;
                case 3:
                    FiveSpriteRenderer.sprite = King;
                    break;
                case 4:
                    FiveSpriteRenderer.sprite = Ai;
                    break;
            }
        }
        else
        {
            FiveSpriteRenderer.sprite = None;
        }

    }

    public void SixRender()
    {
        if (shootNum < 1)
        {

            switch (reloadBall[shootNum + 4])
            {
                case 0:
                    SixSpriteRenderer.sprite = Kokonoha;
                    break;
                case 1:
                    SixSpriteRenderer.sprite = Fulost;
                    break;
                case 2:
                    SixSpriteRenderer.sprite = Milky;
                    break;
                case 3:
                    SixSpriteRenderer.sprite = King;
                    break;
                case 4:
                    SixSpriteRenderer.sprite = Ai;
                    break;
            }
        }
        else
        {
            SixSpriteRenderer.sprite = None;
        }

    }



    //Kokonoha;
    //Fulost;
    //Milky;
    //King;
    //Ai;
}