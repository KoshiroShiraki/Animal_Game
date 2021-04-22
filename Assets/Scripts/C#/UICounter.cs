using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICounter: MonoBehaviour
{
    GameObject Player;
    PlayerController pc;
    int MeatCount;
    int GrassCount;

    Text text;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        pc = Player.GetComponent<PlayerController>();

        this.MeatCount = pc.meatCount;
        this.GrassCount = pc.grassCount;

        text = GameObject.Find("UIManager/UI/FoodCounter_counter").GetComponent<Text>();

        text.text = this.MeatCount.ToString() + "\n" + this.GrassCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        this.MeatCount = pc.meatCount;
        this.GrassCount = pc.grassCount;

        text.text = this.MeatCount.ToString() + "\n" + this.GrassCount.ToString();
    }
}
