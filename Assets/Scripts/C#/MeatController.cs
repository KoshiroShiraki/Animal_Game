using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : FoodController
{
    public float rotateSpeed = 0.1f;

    //肉は回転
    public override void normalFood()
    {
        this.transform.Rotate(0.0f, rotateSpeed, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
