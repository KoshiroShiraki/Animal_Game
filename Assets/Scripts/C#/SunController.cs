using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    float angle = 0.0f;
    public float daySpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(daySpeed, 0.0f, 0.0f);
    }
}
