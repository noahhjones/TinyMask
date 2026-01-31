using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField]GameObject menuPanel;
    [SerializeField] GameObject levelsPanel;
    [SerializeField] GameObject settingsPanel;

    void Start()
    {
        ShowMenu();
    }
    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        levelsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void ShowLevels()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void ShowSettings()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void LoadScene(int level)
    {
        if(level < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);
        else Debug.LogError("Level " + level + " not found in build settings.");
    }
}