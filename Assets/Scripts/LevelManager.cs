//  Handling Level/ Scene Mechanisms
//  Managing Scene Loading like
//  Pause, Restart,
//  Play, Shop, Watch Ads, Settings, Share

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Variable Initialization

    int currentSceneIndex;
    bool isPaused = false;

    #endregion

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Pause()
    {
        if (!isPaused)
        {
            // Pause Game
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
            // Resume Game
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
    }
    
    public void PlayButton()
    {
        // Move to Level selector scene
        // Change this to specific scene later
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
