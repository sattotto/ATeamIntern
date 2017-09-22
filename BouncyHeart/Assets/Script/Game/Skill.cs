using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int kingTime = 0;
    public bool kingSkill = false;
    public GameObject KingPanelPrefab;
    public GameObject ReadyPanelPrefab;

    private AudioSource skillUp;
    public bool ready = false;
    GameObject Ready;

    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        skillUp = audioSource[0];
    }

    // Update is called once per frame
    void Update()
    {
        var reload = GetComponent<Reload>();
        SkillPanel skillPanel = GetComponent<SkillPanel>();

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
                    if (!kingSkill)
                    {
                        switch (obj.layer)
                        {
                            //皇：8
                            case 8:

                                if (!ready)
                                {
                                    Ready = Instantiate(ReadyPanelPrefab, new Vector3(0, 0, 1), transform.rotation) as GameObject;
                                }
                                ready = true;
                                //GameObject KingPanel = Instantiate(KingPanelPrefab, new Vector3(50, 0, 1), transform.rotation) as GameObject;
                                //kingSkill = true;
                                //bgm++;
                                Debug.Log("王様");
                                break;
                            ////フロスト：9
                            //case 9:
                            //    //フロストの必殺技呼び出し
                            //    Debug.Log("フロスト");
                            //    break;
                            default:
                                Debug.Log("??");
                                break;
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

    public void KingTime()
    {
        var reload = GetComponent<Reload>();
        kingTime++;
       Debug.Log(kingTime);
        if(kingTime == 160)
        {
            skillUp.PlayOneShot(skillUp.clip);

        }
        if (kingTime == 275)
        {
            Destroy(Ready);
            reload.KingSkill();
            reload.RenderKing();
        }
        if (kingTime > 875)
        {
            kingSkill = false;
            //KingBgm.Stop();

            Debug.Log("TimeOver");
        }
    }
}
