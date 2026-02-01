using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    [SerializeField] float startingOxygen = 100f;
    public OxygenBar oxygenBar;
    private float maxOxygen = 100f;
    private float currentOxygen;
    private AudioSource audioSource;
    [SerializeField] AudioClip oxygenDepletedSound;
    [SerializeField] AudioClip oxygenIncreaseSound;
    [SerializeField] AudioClip oxygenDecreaseSound;


    public static OxygenManager Instance;
    void Awake()
    {
        //one copy of this
        if (Instance != null)
        {
            Debug.LogWarning("Multiple instances of OxygenManager detected! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (oxygenBar == null)
        {
            Debug.LogError("OxygenBar missing!");
        }
        //set current oxygen to starting oxygen
        currentOxygen = startingOxygen;
        oxygenBar.setMeter(startingOxygen);
    }
    public void IncreaseOxygen(float amount)
    {
        currentOxygen = Math.Min(currentOxygen + amount, maxOxygen);
        oxygenBar.setMeter(currentOxygen);
        if (audioSource != null && oxygenIncreaseSound != null)
        {
            audioSource.PlayOneShot(oxygenIncreaseSound);
        }
    }
    public void DecreaseOxygen(float amount)
    {
        if (currentOxygen <= 0f)
        {
            Debug.Log("Out of Oxygen!");
            if (audioSource != null && oxygenDepletedSound != null)
            {
                audioSource.PlayOneShot(oxygenDepletedSound);
            }
            // Kill player and reset
            // For now, just reload the level
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            return;
        }
        currentOxygen = Math.Max(currentOxygen - amount, 0f);
        oxygenBar.setMeter(currentOxygen);    
    }
}
