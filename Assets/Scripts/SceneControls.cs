//  Handling Level/ Scene Mechanisms
//  Managing Scene Loading like
//  Pause, Restart,
//  Play, Shop, Watch Ads, Settings, Share

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControls : MonoBehaviour
{
    #region Variable Initialization

    [Header("Pop Ups:-")]
    [SerializeField] GameObject popUpBackground;    
    [Header("Pause")]
    [SerializeField] PopUpAnimControls pausePopUpAnimControls;
    [SerializeField] GameObject pausePopUp;
    [Header("Task")]
    [SerializeField] PopUpAnimControls taskPopUpAnimControls;
    [SerializeField] GameObject taskPopUp;

    int currentSceneIndex;
    bool isPaused = false;
    [Space(10)]
    [SerializeField] float sceneTransitionDelay = 1f;

    #endregion

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (pausePopUp && popUpBackground)
        {
            popUpBackground.SetActive(false);
            // Pop Ups
            pausePopUp.SetActive(false);
            taskPopUp.SetActive(false);
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            // Pause Game
            popUpBackground.SetActive(true);
            pausePopUp.SetActive(true);
            pausePopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
            // Resume Game
            pausePopUpAnimControls.ClosePopUpAnim();
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void TaskButton()
    {
        if (!isPaused)
        {
            // Pause Game
            popUpBackground.SetActive(true);
            taskPopUp.SetActive(true);
            taskPopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
            // Resume Game
            taskPopUpAnimControls.ClosePopUpAnim();
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
        StartCoroutine(SceneWaitAndLoad(currentSceneIndex + 1));
    }

    public void BackButton()
    {
        StartCoroutine(SceneWaitAndLoad(currentSceneIndex - 1));
    }

    public void LevelLoad(string level)
    {
        StartCoroutine(SceneWaitAndLoadLevel(level));
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        StartCoroutine(SceneWaitAndLoadLevel("HomeScene"));
    }

    public void LevelSelectorButton()
    {
        Time.timeScale = 1;
        StartCoroutine(SceneWaitAndLoadLevel("LevelSelectorScene"));
    }

    IEnumerator SceneWaitAndLoad(int scene)
    {
        yield return new WaitForSeconds(sceneTransitionDelay);
        SceneManager.LoadScene(scene);
    }

    IEnumerator SceneWaitAndLoadLevel(string scene)
    {
        yield return new WaitForSeconds(sceneTransitionDelay);
        SceneManager.LoadScene(scene);
    }

}
