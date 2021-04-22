using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*----基底クラス-----*/

public abstract class AnimalController : MonoBehaviour
{
    public int hungry = 100; //空腹度
    public int thirsty = 100; //のどの渇き
    public int hp = 100; //体力
    public bool isWild = true; //野生かプレイヤーの仲間か
    public bool isAlive = true; //生きているか死んでいるか

    private bool isDying = false; //死亡アニメーションフラグ
    public float deathTime = 60.0f; //死亡時アニメーションフレーム数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //生存時処理
        if (isAlive)
        {

        }
        //死亡時処理
        else if (!isAlive)
        {
            dead();
        }
    }

    //自由行動
    public abstract void freeAction();
    //追従行動
    public abstract void followAction();
    
    //死亡
    public void dead()
    {
        if (isDying)
        {

        }
    }
}
