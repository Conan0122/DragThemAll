/*      Handling Level/ Scene Mechanisms
        Managing Scene Loading like
        Pause, Restart, GameOver, Level Complete
        Play, Shop, Watch Ads, Settings, Share, Buy pop up

        // DON'T MAKE IT SINGLETON CLASS //
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class SceneControls : MonoBehaviour
{
    #region Variable Initialization

    Shop shop;

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
    [Header("GameProgress")]
    [SerializeField] PopUpAnimControls gameProgressPopUpAnimControls;
    [SerializeField] GameObject gameProgressPopUp;
    [Header("BuyTrail")]
    [SerializeField] PopUpAnimControls buyTrailPopUpAnimControls;
    [SerializeField] GameObject buyTrailPopUp;

    [Header("SceneTransitions")]
    [SerializeField] Animator sceneTransitionAnim;
    [SerializeField] float sceneTransitionDelay = 1f;

    int userLevel;   // Get this from LevelLoad
    int currentSceneIndex;
    bool isPaused = false;
    bool isPopUpDisplayed = false;

    const string USER_LEVEL = "userLevel";

    #endregion

    private void Start()
    {
        shop = FindObjectOfType<Shop>();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (popUpBackground)
        {
            popUpBackground.SetActive(false);

            //      Pop ups
            if (pausePopUp) pausePopUp.SetActive(false);
            if (taskPopUp) taskPopUp.SetActive(false);
            if (gameOverPopUp) gameOverPopUp.SetActive(false);
            if (gameProgressPopUp) gameProgressPopUp.SetActive(false);
            if (buyTrailPopUp) buyTrailPopUp.SetActive(false);
        }
    }

    //          POP UPS
    //              Pause
    //              Task
    //              GameOver
    //              Level Complete
    //              Game Progress
    //              Buy Trail

    public void PauseAndCancel()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
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
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
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
            AudioManager.instance.PlayAudio(Sounds.AudioName.LevelComplete, false);
            levelCompletePopUpAnimControls.OpenPopUpAnim();
            Time.timeScale = 0;
            isPopUpDisplayed = true;
        }
    }

    public void GameProgresssPopUp()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        popUpBackground.SetActive(true);
        gameProgressPopUp.SetActive(true);
        gameProgressPopUpAnimControls.OpenPopUpAnim();
    }

    public void ResetProgress()
    {
        DataPersistenceManager.instance.NewGameData();
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        CloseResetPopUp();
    }

    public void CloseResetPopUp()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        gameProgressPopUpAnimControls.ClosePopUpAnim();
    }

    public void BuyTrailPopUp()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        popUpBackground.SetActive(true);
        buyTrailPopUp.SetActive(true);
        buyTrailPopUpAnimControls.OpenPopUpAnim();
    }
    public void BuyTrailButton(int cost)
    {
        //  If bought successfully
        //      close pop up.
        //  else do something so that user gets to know that purchase failed.
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        if (shop)
        {
            CloseBuyTrailPopUp();
            shop.BuyTrail(cost);
            Debug.Log($"Buying");
        }
    }
    public void CloseBuyTrailPopUp()
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        buyTrailPopUpAnimControls.ClosePopUpAnim();
    }

    // --------------------------------------------------------

    public void Restart()
    {
        sceneTransitionAnim.SetTrigger("Start");
        // TO be used in analytics purpose
        // to analyse in which level does player restarts level most.
        // Set userLevel to whatever saved in playerprefs for current level
        userLevel = PlayerPrefs.GetInt(USER_LEVEL, 1);
        Analytics.CustomEvent("restartLevel__", new Dictionary<string, object>
        {
            { "userLevel" , userLevel}
        });
        Debug.Log($"UserLevel when restarting = " + userLevel);

        StartCoroutine(WaitAndLoadScene<int>(currentSceneIndex, Sounds.AudioName.NormalButtonClicks));
        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        // Move to Level selector scene
        // Change this to specific scene later

        Analytics.CustomEvent("playButton__");
        // Debug.Log($"Custom event PlayButton " + analyticsResult);

        StartCoroutine(WaitAndLoadScene<int>((currentSceneIndex + 1), Sounds.AudioName.BigButtonClicks));
    }

    public void PlayBigButtonSFX()
    {
        // Script just to play Big button sfx wherever needed
        AudioManager.instance.PlayAudio(Sounds.AudioName.BigButtonClicks, false);
    }

    public void GoToScene(string sceneName)
    {
        DataPersistenceManager.instance.LoadFile();
        //  Can put this on back button well.
        StartCoroutine(WaitAndLoadScene<string>(sceneName, Sounds.AudioName.NormalButtonClicks));
        Time.timeScale = 1;
    }

    public void NextLevelButton()
    {
        /*          Make one scene, named "End of Levels".
                        Keep it in last build index after level scene,
                        And we cant go to next scene after scene.
                            We will inform players that they've complete all the levels,
                            either they restart game again from level 1 or they can wait for new updates.
        */
        StartCoroutine(WaitAndLoadScene<int>(currentSceneIndex + 1, Sounds.AudioName.NormalButtonClicks));
        Time.timeScale = 1;
    }

    public void LevelLoad(string level)
    {
        StartCoroutine(WaitAndLoadScene<string>("L" + level, Sounds.AudioName.NormalButtonClicks));
        DataPersistenceManager.instance.ReturnActiveLevel(level);
        // Parse the current level(string) to integer
        // To use later to receive it in Analytics purpose
        Int32.TryParse(level, out userLevel);
        // Save userLevel in playerprefs
        // and then use it to know whats current level
        PlayerPrefs.SetInt(USER_LEVEL, userLevel);
        Debug.Log($"UserLevel on load = " + userLevel);
    }

    public void BackHomeButton()
    {
        StartCoroutine(WaitAndLoadScene<string>("HomeScene", Sounds.AudioName.NormalButtonClicks));
        Time.timeScale = 1;
    }

    public void LevelSelectorButton()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitAndLoadScene<string>("LevelSelectorScene", Sounds.AudioName.NormalButtonClicks));
    }

    // For Big button with scene transition 
    // for first 3 big button in home scene
    public void Experiment<T>(T scene)
    {
        StartCoroutine(WaitAndLoadScene<T>(scene, Sounds.AudioName.NormalButtonClicks));
        Time.timeScale = 1;
    }

    IEnumerator WaitAndLoadScene<T>(T scene, Sounds.AudioName audioName)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayAudio(audioName, false);
        }

        sceneTransitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(sceneTransitionDelay);

        if (typeof(T) == typeof(int))
        {
            SceneManager.LoadScene((int)(object)scene);
        }
        else if (typeof(T) == typeof(string))
        {
            SceneManager.LoadScene((string)(object)scene);
        }
    }

    
}


//  UNUSED CODE


// IEnumerator WaitAndLoadScene_Int(int scene, Sounds.AudioName audioName)
// {
//     if (AudioManager.instance != null)
//     {
//         // For scenes based on index
//         AudioManager.instance.PlayAudio(audioName, false);
//     }
//     sceneTransitionAnim.SetTrigger("Start");
//     yield return new WaitForSeconds(sceneTransitionDelay);
//     SceneManager.LoadScene(scene);
// }

// IEnumerator WaitAndLoadScene_String(string scene, Sounds.AudioName audioName)
// {
//     if (AudioManager.instance != null)
//     {
//         // For levels
//         AudioManager.instance.PlayAudio(audioName, false);
//     }
//     sceneTransitionAnim.SetTrigger("Start");
//     yield return new WaitForSeconds(sceneTransitionDelay);
//     SceneManager.LoadScene(scene);
// }