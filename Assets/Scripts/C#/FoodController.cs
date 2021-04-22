using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-----Food基底クラス-----*/

public abstract class FoodController : MonoBehaviour
{
    [HideInInspector] public bool isGetting = false; //オブジェクト獲得中フラグ
    public bool isWild = true; //野生のFoodか、プレイヤー所有のFoodか

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        normalFood();

        if (isGetting) gotFood();
    }

    private void OnTriggerEnter(Collider other)
    {
        isGetting = true; //取得中
        isWild = false; //野生から所有物へ
    }

    //平常時のふるまい
    public abstract void normalFood();

    //Foodが入手された時のふるまい(共通)
    public void gotFood()
    {
        //大きさを徐々に小さくする
        Vector3 scale = this.transform.localScale;
        scale -= new Vector3(0.05f, 0.05f, 0.05f);
        this.transform.localScale = scale;

        if (scale.x <= 0)
        {
            isGetting = false;
            Destroy(this.gameObject);
        }
    }
}
