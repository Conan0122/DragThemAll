/*      Handling Level/ Scene Mechanisms
        Managing Scene Loading like
        Pause, Restart, GameOver, Level Complete
        Play, Shop, Watch Ads, Settings, Share
*/

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
    [Header("GameOver")]
    [SerializeField] PopUpAnimControls gameOverPopUpAnimControls;
    [SerializeField] GameObject gameOverPopUp;
    [Header("LevelComplete")]
    [SerializeField] PopUpAnimControls levelCompletePopUpAnimControls;
    [SerializeField] GameObject levelCompletePopUp;
    [Header("SceneTransitions")]
    [SerializeField] Animator sceneTransitionAnim;
    [SerializeField] float sceneTransitionDelay = 1f;

    int currentSceneIndex;
    bool isPaused = false;
    bool isPopUpDisplayed = false;
    

    #endregion

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (popUpBackground)
        {
            popUpBackground.SetActive(false);

            //      Pop ups
            if (pausePopUp) pausePopUp.SetActive(false);
            if (taskPopUp) taskPopUp.SetActive(false);
            if (gameOverPopUp) gameOverPopUp.SetActive(false);
        }
    }

    //          POP UPS
    //              Pause
    //              Task
    //              GameOver
    //              Level Complete

    public void PauseAndCancel()
    {
        if (!isPaused)
        {
            //      Pause Game
            popUpBackground.SetActive(true);
            pausePopUp.SetActive(true);
            pausePopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
            //      Resume Game
            pausePopUpAnimControls.ClosePopUpAnim();
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void TaskButton()
    {
        if (!isPaused)
        {
            //      Pause Game
            popUpBackground.SetActive(true);
            taskPopUp.SetActive(true);
            taskPopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
            //      Resume Game
            taskPopUpAnimControls.ClosePopUpAnim();
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void GameOverPopUp()
    {
        if (!isPopUpDisplayed)
        {
            //  TODO
            //      Destroy all the attacker remaining in scene
            popUpBackground.SetActive(true);
            gameOverPopUp.SetActive(true);
            gameOverPopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPopUpDisplayed = true;
        }
    }

    public void LevelCompletePopUp()
    {
        if (!isPopUpDisplayed)
        {
            //  TODO
            //      Destroy all the attacker remaining in scene
            popUpBackground.SetActive(true);
            levelCompletePopUp.SetActive(true);
            levelCompletePopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPopUpDisplayed = true;
        }
    }

    public void Restart()
    {
        sceneTransitionAnim.SetTrigger("Start");
        StartCoroutine(SceneWaitAndLoad(currentSceneIndex));
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

    public void NextLevelButton()
    {
        /*          Make one scene, named "End of Levels".
                        Keep it in last build index after level scene,
                        And we cant go to next scene after scene.
                            We will inform players that they've complete all the levels,
                            either they restart game again from level 1 or they can wait for new updates.
        */
        StartCoroutine(SceneWaitAndLoad(currentSceneIndex + 1));
        Time.timeScale = 1;
    }

    public void LevelLoad(string level)
    {
        StartCoroutine(SceneWaitAndLoadLevel("L" + level));
        DataPersistenceManager.instance.ReturnActiveLevel(level);
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
        // For scenes based on index
        sceneTransitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(sceneTransitionDelay);
        SceneManager.LoadScene(scene);
    }

    IEnumerator SceneWaitAndLoadLevel(string scene)
    {
        // For levels
        sceneTransitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(sceneTransitionDelay);
        SceneManager.LoadScene(scene);
    }

}
