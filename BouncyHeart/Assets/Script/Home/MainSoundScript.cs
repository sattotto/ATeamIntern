using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSoundScript : MonoBehaviour
{
    public bool DontDestroyEnabled = true;

    // Use this for initialization
    void Start()
    {
        if (DontDestroyEnabled)
        {
            // Sceneを遷移してもオブジェクトが消えないようにする
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //今のシーン番号
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (Input.GetMouseButton(0))
        {
            Debug.Log("クリック");
            //クリックして、オブジェクトがあったら
            GameObject obj = getClickObject();
            if (obj != null)
            {
                if (obj.tag == "HomeBotton")
                {
                    //クエスト選択
                    SceneManager.LoadScene("QuestSelect");
                }
                if(obj.tag == "QuestBotton")
                {
                    //
                    SceneManager.LoadScene("Aria");
                }
                if (obj.tag == "AriaBotton")
                {
                    //
                    SceneManager.LoadScene("Friend");
                }
                if (obj.tag == "FriendBotton")
                {
                    //
                    SceneManager.LoadScene("Check");
                }
                if (obj.tag == "BattleBotton")
                {
                    //
                    Destroy(this.gameObject);
                    SceneManager.LoadScene("Game");
                }

                if (obj.tag == "return")
                {
                    //
                    SceneManager.LoadScene(currentSceneIndex - 1);
                    if(currentSceneIndex - 1 == 0)
                    {
                        Destroy(this.gameObject);
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