using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int kingTime = 0;
    public bool kingSkill = false;
    public bool kingNotEffect;
    public GameObject KingPanelPrefab;
    public Canvas canvas;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var reload = GetComponent<Reload>();
        SkillPanel skillPanel = GetComponent<SkillPanel>();

        //if (kingNotEffect)
        //{
        //    //皇の必殺技呼び出し
        //    reload.KingSkill();
        //    reload.RenderKing();
        //    kingSkill = true;
        //    Debug.Log("王様");
        //}

        if (kingSkill)
        {
            KingTime();
            //kingSkill = true;
        }

        if (Input.GetMouseButton(0))
        {
            //クリックして、オブジェクトがあったら
            GameObject obj = getClickObject();
            if (obj != null)
            {
                if (obj.tag == "Skill")
                {
                    Debug.Log("skill");

                    switch (obj.layer)
                    {
                        //皇：8
                        case 8:
                            GameObject KingPanel = Instantiate(KingPanelPrefab, new Vector3(50, 0, 1), transform.rotation) as GameObject;
                            //if (kingNotEffect)
                            {
                                //皇の必殺技呼び出し
                                //reload.KingSkill();
                                //reload.RenderKing();
                                kingSkill = true;
                                Debug.Log("王様");
                            }
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

    public void KingTime()
    {
        var reload = GetComponent<Reload>();

        kingTime++;
        Debug.Log(kingTime);
        if(kingTime == 240)
        {
            reload.KingSkill();
            reload.RenderKing();
        }
        if (kingTime > 900)
        {
            kingSkill = false;
            Debug.Log("TimeOver");
        }
    }
}
