using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCloud : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        //if player collides with cloud, decrease oxygen
        if(collider.gameObject.CompareTag("Player")) {
            OxygenManager.Instance.DecreaseOxygen(10f);
        }
    }
}
