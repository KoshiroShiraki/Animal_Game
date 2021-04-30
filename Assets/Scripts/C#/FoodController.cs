using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-----Food基底クラス-----*/

public enum foodState
{
    WILD, //野生
    PLAYER, //プレイヤーの所有
}

public abstract class FoodController : MonoBehaviour
{
    /*-----全継承クラス共通-----*/
    public bool isGetting; //オブジェクト獲得中フラグ
    public foodState State = foodState.WILD;
    /*--------------------------*/

    /*-----xxx_PlayerController用-----*/
    public bool isGenerating = false; //生成中アニメーション管理フラグ
    [HideInInspector] public Vector3 moveDir; //生成方向
    [HideInInspector] public Vector3 firstPos; //生成位置
    
    public float moveTimer = 0.3f; //飛び出し時間(秒)
    public float dist = 3.0f; //飛び出し距離(1秒間)
    private float time = 0.0f;
    /*--------------------------------*/

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //初期状態指定
        isGetting = false;

        if(State == foodState.WILD)
        {
            isGenerating = false;
        }

        if(State == foodState.PLAYER)
        {
            this.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            isGenerating = true;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        normalFood();

        //取得中なら
        if (isGetting) gotFood();
        //生成中なら
        if (isGenerating) generate();
    }

    //平常時のふるまい
    public abstract void normalFood();

    //Foodが入手された時のふるまい(共通)
    public void gotFood()
    {
        //大きさを徐々に小さくする
        Vector3 scale = this.transform.localScale;
        scale -= new Vector3(1.0f, 1.0f, 1.0f) * Time.deltaTime * 4;
        this.transform.localScale = scale;

        if (scale.x <= 0)
        {
            isGetting = false;
            Destroy(this.gameObject);
        }
    }

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
