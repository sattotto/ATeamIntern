using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("クリック");
            //クリックして、オブジェクトがあったら
            GameObject obj = getClickObject();
            if (obj != null)
            {
                if (obj.tag == "ResultHome")
                {
                    SceneManager.LoadScene("Home");
                }
                if (obj.tag == "ResultOk")
                {
                    //
                    SceneManager.LoadScene("Home");
                }
                if (obj.tag == "ResultReplay")
                {
                    //
                    SceneManager.LoadScene("Home");
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

