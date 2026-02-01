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
    private float maxOxygen = 100f;
    private float currentOxygen;
    private Coroutine FillRoutine;

    void Start()
    {
        currentOxygen = maxOxygen;
        oxygenBar.fillAmount = maxOxygen / 100f;
    }
    public void increaseMeter(float amt)
    {
        if(currentOxygen >= 100f)
        {
            return;
        }
        if (currentOxygen + amt >= maxOxygen)
        {
            AnimateSlider(maxOxygen);
        }
        else
        {
            AnimateSlider(oxygenBar.fillAmount + amt);
        }
    }
    
    private IEnumerator AnimateSlider(float targetValue)
    {
        if (oxygenBar == null) yield break;

        if (FillRoutine != null) //one instance
            StopCoroutine(FillRoutine);

        FillRoutine = StartCoroutine(FillSlider(targetValue));
    }
    private IEnumerator FillSlider(float targetValue)
    {
        float duration = 0.5f; // half a second fill
        float elapsed = 0f;
        float startValue = oxygenBar.fillAmount * 100f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            oxygenBar.fillAmount = Mathf.Lerp(startValue / 100f, targetValue / 100f, t);
            yield return null;
        }

        oxygenBar.fillAmount = targetValue / 100f; // ensure it's exact
        currentOxygen = targetValue;
    }
}
