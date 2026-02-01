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
        if(currentOxygen <= 10f)
        {
            oxygenBar.FlashRed();
        }
    }
    public void IncreaseOxygen(float amount)
    {
        oxygenBar.StopFlashing();
        currentOxygen = Math.Min(currentOxygen + amount, maxOxygen);
        oxygenBar.setMeter(currentOxygen);
        if (audioSource != null && oxygenIncreaseSound != null)
        {
            audioSource.PlayOneShot(oxygenIncreaseSound);
        }
    }
    public void DecreaseOxygen(float amount)
    {
        if (currentOxygen - amount <= 0f)
        {
            Debug.Log("Out of Oxygen!");
            StartCoroutine(OutOfOxygenRoutine());
            return;
        }
        currentOxygen = currentOxygen - amount;
        if(currentOxygen <= 10f)
        {
            oxygenBar.FlashRed();
        }
        oxygenBar.setMeter(currentOxygen);    
    }
    IEnumerator OutOfOxygenRoutine()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        //set velocity to 0 and disable movement
        playerMovement.enabled = false;
        playerRb.velocity = Vector2.zero;
        oxygenBar.setMeter(0f);
        // Play out of oxygen sound
        if (audioSource != null && oxygenDepletedSound != null)
        {
            audioSource.PlayOneShot(oxygenDepletedSound);
        }
        // Wait for sound to finish
        yield return new WaitForSeconds(1f);
        // Reload the scene
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
