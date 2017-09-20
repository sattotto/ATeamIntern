using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{

    public bool flug = false;
    bool position_flug = true;
    bool end_flug = false;
    int bgm = 0;
    int time = 0;
    public bool skill_flug = false;
    private AudioSource king;
    Vector2 start_pos;

    //Vector2 pos;
    // Use this for initialization
    void Start()
    {
        start_pos = GetComponent<RectTransform>().anchoredPosition;

        AudioSource[] audioSource = GetComponents<AudioSource>();
        king = audioSource[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (flug)
        {
            if (position_flug || end_flug)
            {
                Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
                pos.x -= 10;
                GetComponent<RectTransform>().anchoredPosition = pos;
                if (pos.x < 4)
                {
                    position_flug = false;
                }
                if(pos.x < -384)
                {
                    flug = false;
                    skill_flug = true;
                    GetComponent<RectTransform>().anchoredPosition = start_pos;
                }
            }
            if (!position_flug)
            {
                time++;
                bgm++;
            }
            if (time > 240)
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
