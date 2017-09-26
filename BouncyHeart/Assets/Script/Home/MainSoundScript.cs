using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSoundScript : MonoBehaviour
{
    public bool DontDestroyEnabled = true;

    private AudioSource clickSe;

    int count = 0;
    int timer = 0;
    bool inBattle = false;

    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        clickSe = audioSource[1];

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
                    clickSe.PlayOneShot(clickSe.clip);

                    //クエスト選択
                    SceneManager.LoadScene("QuestSelect");
                }
                if(obj.tag == "QuestBotton")
                {
                    clickSe.PlayOneShot(clickSe.clip);
                    SceneManager.LoadScene("Aria");
                }
                if (obj.tag == "AriaBotton")
                {
                    clickSe.PlayOneShot(clickSe.clip);
                    SceneManager.LoadScene("Friend");
                }
                if (obj.tag == "FriendBotton")
                {
                    clickSe.PlayOneShot(clickSe.clip);
                    SceneManager.LoadScene("Check");
                }
                if (obj.tag == "BattleBotton")
                {
                    inBattle = true;
                }

                if (obj.tag == "return")
                {
                    clickSe.PlayOneShot(clickSe.clip);
                    SceneManager.LoadScene(currentSceneIndex - 1);
                    if(currentSceneIndex - 1 == 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        if(inBattle)
        {
            count++;
            if (count <= 20)
            {
                if (timer == 0)
                {
                    clickSe.PlayOneShot(clickSe.clip);
                }
                timer++;
            }
        if (count > 20)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Game");
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