using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, IInteractable
{
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if(!CanInteract()) return;

        ExitLevel();
    }
    void ExitLevel()
    {
        Debug.Log("Level Complete!");
        int levelNumber = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        // Exit the level
        if(levelNumber + 1 >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings) {
            Debug.Log("Last Level Reached!"); 
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelNumber + 1);
    }
}
