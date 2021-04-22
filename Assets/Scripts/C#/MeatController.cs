using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : FoodController
{
    public float rotateSpeed = 0.1f;

    public override void normalFood()
    {
        this.transform.Rotate(0.0f, rotateSpeed, 0.0f);
        
    }
}
