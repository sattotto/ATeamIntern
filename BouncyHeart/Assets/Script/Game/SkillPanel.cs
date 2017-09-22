using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{

    public bool flug = true;
    bool position_flug = true;
    bool end_flug = false;
    int bgm = 0;
    int time = 0;
    public bool skill_flug = false;
    private AudioSource king;
    Vector2 start_pos;

    public GameObject player;

    //Vector2 pos;
    // Use this for initialization
    void Start()
    {
        start_pos = GetComponent<RectTransform>().anchoredPosition;

        AudioSource[] audioSource = GetComponents<AudioSource>();
        king = audioSource[0];
        GameManager.kingNotEffect = true;

    }

    // Update is called once per frame
    void Update()
    {
        var skill = player.GetComponent<Skill>();
        if (flug)
        {
            if (position_flug || end_flug)
            {
                Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
                pos.x -= 0.5f;
                GetComponent<RectTransform>().anchoredPosition = pos;
                if (pos.x <= 0)
                {
                    position_flug = false;
                }
                if(pos.x < -10)
                {
                    GameManager.kingNotEffect = false;
                    Destroy(this.gameObject);
                }
            }
            if (!position_flug)
            {
                time++;
                bgm++;
            }
            if (time > 125)
            {
                end_flug = true;
            }
            //重なるのを防止するため
            if(bgm == 1)
            {
                king.PlayOneShot(king.clip);
            }
            Debug.Log(time);
        }
    }

}
