using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    //配列
    public int[] reloadBall = new int[5];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BallReset()
    {
        for(int i = 0; i < 5; i++)
        {
            reloadBall[i] = Random.Range(0, 4);
        }
    }
}
