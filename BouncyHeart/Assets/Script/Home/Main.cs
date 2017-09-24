using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    int charaId;

    //描画用
    GameObject[] Chara;

    SpriteRenderer CharaSpriteRenderer;


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

        charaId = Random.Range(0, 4);
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
        }


    }

    // Update is called once per frame
    void Update () {
		
	}
}
