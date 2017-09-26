using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    int charaId;

    //描画用
    GameObject[] Chara;

    SpriteRenderer CharaSpriteRenderer;

    private AudioSource kokonohaVoice;
    private AudioSource fulostVoice;
    private AudioSource milkyVoice;
    private AudioSource kingVoice;
    private AudioSource aiVoice;
//    private AudioSource readySe;


    //きゃらのイラスト一覧
    public Sprite Kokonoha;
    public Sprite Fulost;
    public Sprite Milky;
    public Sprite King;
    public Sprite Ai;

    // Use this for initialization
    void Start () {

        Chara = GameObject.FindGameObjectsWithTag("Charactor");
        // このobjectのSpriteRendererを取得
        CharaSpriteRenderer = Chara[0].GetComponent<SpriteRenderer>();

        charaId = Random.Range(0, 5);
        Debug.Log(charaId);
        switch (charaId)
        {
            case 0:
                CharaSpriteRenderer.sprite = Kokonoha;
                break;
            case 1:
                CharaSpriteRenderer.sprite = Fulost;
                break;
            case 2:
                CharaSpriteRenderer.sprite = Milky;
                break;
            case 3:
                CharaSpriteRenderer.sprite = King;
                break;
            case 4:
                CharaSpriteRenderer.sprite = Ai;
                break;
            default:
                CharaSpriteRenderer.sprite = Ai;
                break;

        }

        AudioSource[] audioSource = GetComponents<AudioSource>();
        kokonohaVoice = audioSource[0];
        fulostVoice = audioSource[1];
        milkyVoice = audioSource[2];
        kingVoice = audioSource[3];
        aiVoice = audioSource[4];
        //readySe = audioSource[3];


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //クリックして、オブジェクトがあったら
            GameObject obj = getClickObject();
            if (obj != null)
            {
                if (obj.tag == "Charactor")
                {
                    switch (charaId)
                    {
                        case 0:
                            kokonohaVoice.PlayOneShot(kokonohaVoice.clip);
                            break;
                        case 1:
                            fulostVoice.PlayOneShot(fulostVoice.clip);
                            break;
                        case 2:
                            milkyVoice.PlayOneShot(milkyVoice.clip);
                            break;
                        case 3:
                            kingVoice.PlayOneShot(kingVoice.clip);
                            break;
                        case 4:
                            aiVoice.PlayOneShot(aiVoice.clip);
                            break;
                        default:
                            aiVoice.PlayOneShot(aiVoice.clip);
                            break;

                    }

                }
            }
        }
    }
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
