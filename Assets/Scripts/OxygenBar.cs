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

    private Coroutine flashRoutine;
    public void FlashRed()
    {
        if (flashRoutine == null)
        {
            flashRoutine = StartCoroutine(FlashRepeat());
        }
    }
    public void StopFlashing()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }
        // Reset color to original
        oxygenBar.color = new Color(0f, 255f, 225f, 255f);
    }
    private IEnumerator FlashRepeat()
    {
        Color originalColor = new Color(0f, 255f, 225f, 255f);
        Color flashColor = Color.red;
        float halfDuration = 0.25f;

        while (true)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / halfDuration;
                oxygenBar.color = Color.Lerp(originalColor, flashColor, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / halfDuration;
                oxygenBar.color = Color.Lerp(flashColor, originalColor, t);
                yield return null;
            }
        }
    }
}
