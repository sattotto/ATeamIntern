using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public GameObject target;
    NavMeshAgent agent;
    public int flg = 0;
    public Vector3 tmp;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //衝突していなかったら
        if (flg == 0)
        {
            //プレイヤーに追従する処理
            this.transform.position=Vector3.MoveTowards(this.transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z), 1f * Time.deltaTime);
            //agent.destination = target.transform.position;
        } 
        //衝突したら
        if(flg == 1)
        {
            //現在位置を保有
            //Vector3 tmp2 = agent.transform.position;
            //ノックバック
            transform.position = transform.forward * 0.1f;
            //agent.transform.position = new Vector3(tmp2.x, tmp2.y, tmp2.z);
        }
        if(flg == 2)
        {
            transform.position = tmp;
        }
    }

    //衝突したら
    private void OnCollisionEnter(Collision other)
    {
        //tagがplayerなら
        if (other.gameObject.tag == "player")
        {
            //フラグをtrue
            flg = 1;
            //0.5秒後にフラグをfalse
            Invoke("flgChange",0.5f);

            //Invoke("StopMotion", 1f);
        }
    }
    //ここで少し移動しないモーションを入れる
    void StopMotion()
    {
        //現在位置を保有
        //Vector3 tmp = agent.transform.position;
        //
     //   agent.transform.position = tmp;
        Debug.Log("stop!");
        Invoke("flgChange2",1f);
    }

    //フラグをfalseにする
    void flgChange()
    {
        //フラグをfalse
        flg = 2;
        //現在位置を保有
        ////tmp = agent.transform.position;
        tmp = transform.position;
        StopMotion();

    }
    void flgChange2()
    {
        flg = 0;
    }
}