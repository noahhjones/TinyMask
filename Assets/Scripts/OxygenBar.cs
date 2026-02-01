using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] Image oxygenBar;

    private Coroutine FillRoutine;

    public void setMeter(float amt)
    {
        StartCoroutine(AnimateSlider(amt));
    }
    
    private IEnumerator AnimateSlider(float targetValue)
    {
        if (oxygenBar == null) {
            Debug.LogError("Oxygen Bar Image is not assigned!");
            yield break;
        }

        if (FillRoutine != null) //one instance
        {
            StopCoroutine(FillRoutine);
        }

        FillRoutine = StartCoroutine(FillSlider(targetValue));
    }
    private IEnumerator FillSlider(float targetValue)
    {
        float duration = 0.5f; // half a second fill
        float elapsed = 0f;
        float startValue = oxygenBar.fillAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            oxygenBar.fillAmount = Mathf.Lerp(startValue, targetValue / 100f, t);
            yield return null;
        }

        oxygenBar.fillAmount = targetValue / 100f; // ensure it's exact
    }
}
