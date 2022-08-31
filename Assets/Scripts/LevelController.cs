//          Handling Level Controller mechanism
//          When to show GameOver pop up
//          When to show Level complete pop up
//          What to do when you failed the level
//          What to do when you complete level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Variable Initialization

    TaskGiver taskGiver;
    Player playerHealth;
    SceneControls sceneControls;

    #endregion

    void Start()
    {
        taskGiver = FindObjectOfType<TaskGiver>();
        playerHealth = FindObjectOfType<Player>();
        sceneControls = FindObjectOfType<SceneControls>();
    }

    private void Update()
    {
        OpenLevelCompletePopUp();
        OpenGameOverPopUp();
    }

    void OpenLevelCompletePopUp()
    {
        if (taskGiver.AllQuestCompleted() && playerHealth.CurrentHealth >= 0)
        {
            sceneControls.LevelCompletePopUp();
        }
    }

    void OpenGameOverPopUp()
    {
        if ((!taskGiver.AllQuestCompleted() && playerHealth.CurrentHealth <= 0) || playerHealth.CurrentHealth <= 0)
        {
            sceneControls.GameOverPopUp();
        }
    }

}
