using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCloud : MonoBehaviour
{
    private Coroutine gasCoroutine;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")) {
            //gasCoroutine = StartCoroutine("DamageOverTime", collider.gameObject));
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")) {
            //if(gasCoroutine != null)
            //    StopCoroutine(gasCoroutine);
        }
    }
}
