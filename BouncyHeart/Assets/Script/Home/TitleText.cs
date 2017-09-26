using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour {
    private float nextTime;
    public float interval = 0.0001f;    // 点滅周期
    int time = 0;
    // Use this for initialization
    void Start () {
        nextTime = Time.time;

    }

    // Update is called once per frame
    void Update () {
        time++;
        if (time > 45)
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            time = 0;

        }

    }
}
