//      Handling Game timer

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    #region Variable Initialization

    AttackerSpawner attackerSpawner;
    TaskGiver taskGiver;
    [SerializeField] TextMeshProUGUI levelTimerText;
    [SerializeField] TextMeshProUGUI initialTimerText;
    [SerializeField] GameObject startPanelButton;
    TimeSpan time;

    [Tooltip("Timer in Seconds")]
    [SerializeField] float levelTime = 10;
    [SerializeField] int initialTime = 3;
    bool levelTimerIsReached = false;

    bool startLevelTimer = false;

    #endregion

    #region Getters and setters
    public bool LevelTimerIsReached
    {
        get { return levelTimerIsReached; }
        set { levelTimerIsReached = value; }
    }
    public bool StartLevelTimer
    {
        get { return startLevelTimer; }
        set { startLevelTimer = value; }
    }
    #endregion

    void Start()
    {
        initialTimerText.gameObject.SetActive(false);
        attackerSpawner = FindObjectOfType<AttackerSpawner>();
        taskGiver = FindObjectOfType<TaskGiver>();

        // Show level Time in timer icon at start
        time = TimeSpan.FromSeconds(levelTime);
        levelTimerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");
    }

    void Update()
    {
        if (attackerSpawner.AttackerSpawn)
        {
            LevelTimer();
        }
    }
    public void StartPanelControls()
    {
        // Disable the panel
        // Start the initial countdown timer
        startPanelButton.SetActive(false);
        initialTimerText.gameObject.SetActive(true);
        StartLevelTimer = true;
        InitialTimerControl();
    }

    void InitialTimerControl()
    {
        if (attackerSpawner.AttackerSpawn == false)
        {
            StartCoroutine(InitialGameTimer());
        }
    }

    IEnumerator InitialGameTimer()
    {
        for (int i = initialTime; i >= 1; i--)
        {
            initialTimerText.alpha = 1;
            initialTimerText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        initialTimerText.alpha = 0;
        attackerSpawner.AttackerSpawn = true;
    }

    void LevelTimer()
    {
        if (levelTime > 0 && LevelTimerIsReached == false)
        {
            levelTime -= 1 * Time.deltaTime;
        }
        else
        {
            taskGiver.IncrementQuest(null, Quest.GoalType.Survive);
            LevelTimerIsReached = true;
            attackerSpawner.AttackerSpawn = false;
        }

        time = TimeSpan.FromSeconds(levelTime);
        levelTimerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");

    }


}
