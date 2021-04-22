using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass_PlayerController : GrassController
{
    private bool isGenerating;
    private Vector3 moveDir;
    private Vector3 firstPos; //生成位置

    public float moveTimer = 1.0f; //飛び出し時間
    public float dist = 3.0f; //飛び出し距離
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        isWild = false; //野生ではない
        isGenerating = true; //生成中
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        this.gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f); //最初は大きさゼロ

        //dirVector取得用
        moveDir = GameObject.Find("Player").GetComponent<PlayerController>().dirVector * dist;

        //生成位置取得
        firstPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (isGenerating)
        {
            generate();
        }
    }

    //生成時関数
    public void generate()
    {
        this.transform.position += moveDir * Time.deltaTime * dist;
        this.transform.localScale = new Vector3(time / moveTimer, time / moveTimer, time / moveTimer);
        time += Time.deltaTime;
        if (time >= moveTimer)
        {
            time = 0.0f; //タイマーリセット
            isGenerating = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
