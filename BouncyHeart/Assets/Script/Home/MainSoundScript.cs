using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSoundScript : MonoBehaviour
{
    public bool DontDestroyEnabled = true;

    private AudioSource clickSe;

    public GameObject AlertPanel;
    GameObject panel;

    int count = 0;
    int timer = 0;
    bool inBattle = false;
    public static bool showAlert = false;
    bool isDisplayDelay = false;

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
            if (obj != null && !showAlert)
            {
                if (obj.tag == "HomeBotton")
                {
                    clickSe.PlayOneShot(clickSe.clip);

                    //クエスト選択
                    SceneManager.LoadScene("QuestSelect");
                }
                if (obj.tag == "QuestBotton")
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
                    if (currentSceneIndex - 1 == 1)
                    {
                        Destroy(this.gameObject);
                    }
                }

                if (obj.tag == "returnHome")
                {
                    clickSe.PlayOneShot(clickSe.clip);

                    //Homeへ
                    SceneManager.LoadScene(1);
                    Destroy(this.gameObject);
                }
                if (obj.tag == "none")
                {
                    clickSe.PlayOneShot(clickSe.clip);
                    panel = Instantiate(AlertPanel, new Vector3(0, 0, 0), transform.rotation) as GameObject;
                    // カメラオブジェクトを取得します
                    GameObject _mainCamera = GameObject.Find("Main Camera");
                    GameObject.Find("Alert(Clone)").GetComponent<Canvas>().worldCamera = _mainCamera.GetComponent<Camera>();
                    Invoke("flgChange", 0.3f);
                    showAlert = true;
                }
            } else {
                if (showAlert && isDisplayDelay){
					clickSe.PlayOneShot(clickSe.clip);
					Destroy(panel.gameObject);
					//showAlert = false;
                    Invoke("alertFlg", 0.3f);
                    isDisplayDelay = false;
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
    void flgChange(){
        isDisplayDelay = !isDisplayDelay;
    }
    void alertFlg(){
        showAlert = false;
    }
}