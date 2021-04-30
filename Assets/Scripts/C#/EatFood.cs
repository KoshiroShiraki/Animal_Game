using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if(other.gameObject.tag == "Meat")
        {
            if(this.tag == "Animal_carn")
            {
                this.GetComponent<CarnController>().Hungry += 10;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<MeatController>().isGetting = true;
            }
        }
        if(other.gameObject.tag == "Grass")
        {
            if(this.tag == "Animal_herb")
            {
                this.GetComponent<HerbController>().Hungry += 10;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<GrassController>().isGetting = true;
            }
        }
    }
}
