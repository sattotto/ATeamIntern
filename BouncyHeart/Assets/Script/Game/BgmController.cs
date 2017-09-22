using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{

    private static int id;

    private AudioSource KingBgm;
    private AudioSource BattleBgm;

    int bgm = 0;
    int battle = 0;

    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        BattleBgm = audioSource[0];
        KingBgm = audioSource[1];

    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                KingBgm.Stop();
                bgm = 0;
                battle++;
                break;

            case 1:
                BattleBgm.Stop();
                battle = 0;
                bgm++;
                break;

        }

        if (bgm == 1)
        {
            KingBgm.PlayOneShot(KingBgm.clip);
            Debug.Log("music!!");
        }
        if (battle == 1)
        {
            BattleBgm.PlayOneShot(BattleBgm.clip);
            Debug.Log("music!!");
        }

    }

    public static void ChangeBgm(int bgmId)
    {
        id = bgmId;
    }
}
