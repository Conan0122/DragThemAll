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
    TimeSpan time;

    [Tooltip("Timer in Seconds")]
    [SerializeField] float levelTime = 10;
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

        // Show Time in timer
        time = TimeSpan.FromSeconds(levelTime);
        levelTimerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");
    }

    void Update()
    {
        if (LevelTimerIsReached)
        {
            Debug.Log($"Timer reached");
            return;
        }
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        // Wait for sometime
        yield return new WaitForSeconds(1f);

        // Start Timer
        if (levelTime > 0 && LevelTimerIsReached == false)
        {
            levelTime -= Time.deltaTime;
        }
        else
        {
            LevelTimerIsReached = true;
        }

        time = TimeSpan.FromSeconds(levelTime);
        levelTimerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");


        
    }


}
