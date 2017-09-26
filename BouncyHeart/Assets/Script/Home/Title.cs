using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AudioSource clickSe;

    int count = 0;
    int timer = 0;
    bool inBattle = false;


    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSource = GetComponents<AudioSource>();
        clickSe = audioSource[0];
        inBattle = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            inBattle = true;
        }

        if (inBattle)
        {
            count++;
            if (count <= 15)
            {
                if (timer == 0)
                {
                    clickSe.PlayOneShot(clickSe.clip);
                }
                timer++;
            }
            if (count > 15)
            {
                SceneManager.LoadScene("Home");

            }
        }
    }
}