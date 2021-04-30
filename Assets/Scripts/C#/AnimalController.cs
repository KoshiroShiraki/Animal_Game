using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*----基底クラス-----*/
public enum AnimalState
{
    NORMAL, //通常状態
    ATTACK, //攻撃状態(草食動物の場合→DetectDistの範囲内にいた場合(特定の種に限る)、肉食動物の場合→DetectDistの範囲内にいた場合かつ空腹状態の場合)
    FOLLOW, //追従状態
    HUNGRY, //空腹状態
    FOODSEARCH, //餌探し状態(腹が減っているとき)
    EAT, //食事状態
    DEAD, //死亡状態
};

public abstract class AnimalController : MonoBehaviour
{
    [HideInInspector] public AnimalState State; //状態
    public float NormalActionTimer; //汎用タイマー
    public float NormalActionTime; //ノーマルアクション継続時間
    public float RotateTimer; //タイマー
    public float RotateTime; //回転時間
    GameObject Player;

    /*-----動物共通パラメータ-----*/
    public int HP; //体力値
    public int Hungry; //空腹値
    public int DetectDist; //感知距離
    public float speed = 1.0f; //移動スピード
    [HideInInspector] public Vector3 moveVector; //移動方向
    [HideInInspector] public float animalDir; //向き
    [HideInInspector] public float panimalDir; //更新前向き
    [HideInInspector] public float deltaDir; //向き更新量

    /*-----各種ループ用フラグ-----*/
    [HideInInspector] public bool isActionStart = false;

    // Start is called before the first frame update
    public void Start()
    {
        State = AnimalState.NORMAL; //初期状態はNORMAL  
        NormalActionTimer = 0.0f; //タイマーリセット
        RotateTimer = 0.0f;

        animalDir = panimalDir = deltaDir = 0.0f; //初期向き

        Player = GameObject.Find("Player"); //プレイヤーオブジェクトは見つけておく
    }

    // Update is called once per frame
    public void Update()
    {
        //それぞれの状態の時にそれぞれの関数を実行
        switch (State)
        {
            case AnimalState.NORMAL:
                NormalAction();
                break;
            case AnimalState.ATTACK:
                AttackAction();
                break;
            case AnimalState.FOLLOW:
                FollowAction();
                break;
            case AnimalState.DEAD:
                Dead();
                break;
        }

    }

    //自由行動
    public abstract void NormalAction();

    //攻撃行動
    public abstract void AttackAction();

    //追従行動
    public abstract void FollowAction();

    //死亡
    public abstract void Dead();

    //移動
    public void move()
    {
        this.transform.position += moveVector * speed * Time.deltaTime;
    }

    //回転
    public void rotate()
    {

        this.transform.rotation = Quaternion.Euler(0.0f, panimalDir + (deltaDir * (RotateTimer / RotateTime)), 0.0f); //回転
    }
}
