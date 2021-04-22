using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player;
    private Vector3 offset; //プレイヤー - カメラ距離
    public float cameraHeight = 10.0f;
    public float cameraChase = -2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        offset = new Vector3(0.0f, cameraHeight, cameraChase);
        this.transform.position = Player.transform.position + offset;
        this.transform.LookAt(Player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(0.0f, cameraHeight, cameraChase);
        this.transform.position = Player.transform.position + offset;
        this.transform.LookAt(Player.transform.position);

        //マウス位置によって少し画面をずらす
        Vector3 slide = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height)) / 250.0f;
        Vector3 cameraPos = this.transform.position;
        cameraPos.x += slide.x;
        cameraPos.z += slide.y; //軸の違いを吸収
        this.transform.position = cameraPos;
    }
}
