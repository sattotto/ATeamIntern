using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTouchPanel : MonoBehaviour
{
    // 再生したいEffectのプレハブを指定する
    public GameObject Effect01 = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // タッチエフェクトを仮実装
        if (Input.GetMouseButtonDown(0))
        {
            // タッチした画面座標からワールド座標へ変換
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.0f));
            // 指定したエフェクトを作成
            GameObject go = (GameObject)Instantiate(Effect01, pos, Quaternion.identity);

            // エフェクトを消す
            Destroy(go, 0.4f);
        }
    }
}
