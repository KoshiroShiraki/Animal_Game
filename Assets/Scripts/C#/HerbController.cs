using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//草食    動物スクリプト

public class HerbController : AnimalController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        moveVector = new Vector3(1.0f, 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //自由行動
    public override void NormalAction()
    {
        //もしアクションがスタートしていない場合
        if (!isActionStart)
        {
            /*-----NormalAction時初期パラメータ設定-----*/
            //移動方向
            moveVector = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;

            //前回向きを保存
            panimalDir = animalDir;
            //更新
            Vector3 baseDir = new Vector3(0.0f, 0.0f, 1.0f);
            animalDir = (moveVector.x / Mathf.Abs(moveVector.x)) * (Mathf.Acos(Vector3.Dot(moveVector, baseDir)) / (moveVector.magnitude * baseDir.magnitude)) * 180 / Mathf.PI; //プレイヤー角度の導出
            deltaDir = animalDir - panimalDir;

            /*------------------------------------------*/

            NormalActionTimer = 0.0f; //タイマーリセット
            RotateTimer = 0.0f;
            isActionStart = true; //アクションをスタート
        }
        //アクションがスタートしている場合
        else if (isActionStart)
        {
            //アクション継続時間内であるとき
            if(NormalActionTimer < NormalActionTime)
            {
                /*-----NormalAction-----*/
                move();
                rotate();
                /*----------------------*/

                NormalActionTimer += Time.deltaTime;
                if (RotateTimer <= RotateTime) RotateTimer += Time.deltaTime;
                if (RotateTimer > RotateTime) RotateTimer = RotateTime;
            }
            //アクション継続時間を過ぎたとき
            else if (NormalActionTimer >= NormalActionTime)
            {

                isActionStart = false; //アクションを止める
            }
        }
    }

    //攻撃行動
    public override void AttackAction()
    {

    }

    //追従行動
    public override void FollowAction()
    {

    }

    //死亡
    public override void Dead()
    {

    }
}
