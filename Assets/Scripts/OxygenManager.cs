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
    }
    public void DecreaseOxygen(float amount)
    {
        if(currentOxygen - amount <= 0f) 
        {
            // Handle out of oxygen scenario
            Debug.Log("Out of Oxygen!");
        }
        currentOxygen = Math.Max(currentOxygen - amount, 0f);
        oxygenBar.setMeter(currentOxygen);    
    }
}
