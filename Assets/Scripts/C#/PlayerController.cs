using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Foodを投げるとき以下のオブジェクトをインスタンシングする
    public GameObject Meat;
    private MeatController mc;
    public GameObject Grass;
    private GrassController gc;

    private bool isThrowing = false; //投げ動作フラグ

    //プレイヤーパラメータ
    private Vector3 moveVector; //移動方向ベクトル(positionに加算する)
    [HideInInspector] public Vector3 dirVector; //プレイヤーの方向ベクトル
    private float playerDir; //プレイヤーの角度(z方向前基準)

    public float speed = 0.05f; //移動スピード

    private Rigidbody rb;

    //外部から参照させる変数
    [HideInInspector] public int meatCount;
    [HideInInspector] public int grassCount;

    // Start is called before the first frame update
    void Start()
    {
        //InstanciateするFoodはプレイヤーの所有物なのでStateをPLAYERに
        Meat.GetComponent<MeatController>().State = foodState.PLAYER;
        Grass.GetComponent<GrassController>().State = foodState.PLAYER;
        
        //生成直後はBoxColliderオフに
        Meat.GetComponent<BoxCollider>().enabled = false;
        Grass.GetComponent<BoxCollider>().enabled = false;

        //パラメータリセット
        resetParam();

        //初期値
        meatCount = 10;
        grassCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        resetParam();
        updateParam();

        playerMove();
        playerRotate();
        throwFood();

    }

    //パラメータリセット
    void resetParam()
    {
        moveVector = new Vector3(0.0f, 0.0f, 0.0f);
        dirVector = new Vector3(0.0f, 0.0f, 0.0f);
        playerDir = 0.0f;
    }

    //パラメータ更新
    private void updateParam()
    {
        //MoveVectorのリセット
        moveVector = new Vector3(0.0f, 0.0f, 0.0f);
        //入力受付
        if (Input.GetKey(KeyCode.W))
        {
            moveVector.z += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x += -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector.z += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x += 1;
        }

        moveVector = moveVector.normalized * speed;

        Vector3 mouseVector = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0.0f)); //マウスベクトル(原点は画面中心)

        Vector3 baseDir = new Vector3(0.0f, 1.0f, 0.0f); //基準方向
        playerDir = mouseVector.x / Mathf.Abs(mouseVector.x) * Mathf.Acos(Vector3.Dot(mouseVector, baseDir) / (mouseVector.magnitude * baseDir.magnitude)) * 180 / Mathf.PI; //プレイヤー角度の導出
        
        baseDir = new Vector3(0.0f, 0.0f, 1.0f); //基準方向
        dirVector = (Quaternion.Euler(0.0f, playerDir, 0.0f) * baseDir).normalized;
    }

    //移動関数
    void playerMove()
    {
        this.transform.position += moveVector * Time.deltaTime;
    }

    //回転関数
    void playerRotate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, playerDir, 0.0f); //回転
    }

    //餌を投げる
    void throwFood()
    {
        if (meatCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Meat.GetComponent<MeatController>().moveDir = this.dirVector * Meat.GetComponent<MeatController>().dist;
                GameObject obj = Instantiate(Meat, this.transform.position, Quaternion.identity);
                meatCount--;
            }
        }
        if (grassCount > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Grass.GetComponent<GrassController>().moveDir = this.dirVector * Grass.GetComponent<GrassController>().dist;
                GameObject obj = Instantiate(Grass, this.transform.position, Quaternion.identity);
                grassCount--;
            }
        }
    }

    //衝突処理
    private void OnTriggerEnter(Collider other)
    {
        //衝突相手がMeatだった場合
        if (other.gameObject.tag == "Meat") 
        {
            meatCount++;

            //衝突があった瞬間にBoxCollider切らないと判定が重複する
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            other.gameObject.GetComponent<MeatController>().isGetting = true;
        }
        //衝突相手がGrassだった場合
        if (other.gameObject.tag == "Grass")
        {
            grassCount++;

            //衝突があった瞬間にBoxCollider切らないと判定が重複する
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            other.gameObject.GetComponent<GrassController>().isGetting = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
