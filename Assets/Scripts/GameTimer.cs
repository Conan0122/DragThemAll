//  Handling Game timer

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
    [SerializeField] TextMeshProUGUI levelTimerText;
    [SerializeField] TextMeshProUGUI initialTimerText;
    TimeSpan time;

    [Tooltip("Timer in Seconds")]
    [SerializeField] float levelTime = 10;
    [SerializeField] int initialTime = 3;
    bool levelTimerIsReached = false;

    #endregion

    #region Getters and setters
    public bool LevelTimerIsReached
    {
        get { return levelTimerIsReached; }
        set { levelTimerIsReached = value; }
    }
    #endregion

    void Start()
    {
        attackerSpawner = FindObjectOfType<AttackerSpawner>();

        // Show level Time in timer icom at start
        time = TimeSpan.FromSeconds(levelTime);
        levelTimerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");

        InitialTimerControl();
    }

    void Update()
    {
        if (attackerSpawner.AttackerSpawn)
        {
            LevelTimer();
        }
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
            LevelTimerIsReached = true;
            attackerSpawner.AttackerSpawn = false;
        }

        time = TimeSpan.FromSeconds(levelTime);
        levelTimerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");

    }


}
