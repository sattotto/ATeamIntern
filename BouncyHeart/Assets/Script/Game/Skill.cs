﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var reload = GetComponent<Reload>();
        if (Input.GetMouseButton(0))
        {
            //クリックして、オブジェクトがあったら
            GameObject obj = getClickObject();
            if(obj != null)
            {
                if (obj.tag == "Skill")
                {
                    Debug.Log("skill");

                    switch (obj.layer)
                    {
                        //皇：8
                        case 8:
                            //皇の必殺技呼び出し
                            reload.KingSkill();
                            reload.RenderKing();
                            Debug.Log("王様");
                            break;
                        //フロスト：9
                        case 9:
                            //フロストの必殺技呼び出し
                            Debug.Log("フロスト");
                            break;
                        default:
                            Debug.Log("??");
                            break;
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
}