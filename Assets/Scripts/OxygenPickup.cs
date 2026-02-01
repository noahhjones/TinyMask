using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPickup : MonoBehaviour
{
    [SerializeField] float oxygenAmount = 10f;
    [SerializeField] Transform maskTransform;

    void Start()
    {
        float percent = oxygenAmount / 100f;
        
        Vector3 scale = maskTransform.localScale;
        scale.y = percent * maskTransform.localScale.y;
        maskTransform.localScale = scale;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //if player collides with cloud, decrease oxygen
        if(collider.gameObject.CompareTag("Player")) {
            OxygenManager.Instance.IncreaseOxygen(oxygenAmount);
        }
    }
}
