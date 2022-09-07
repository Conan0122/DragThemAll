/*      Handling Level Controller mechanism
        When to show GameOver pop up
        When to show Level complete pop up
        What to do when you failed the level
        What to do when you complete level
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region Variable Initialization

    TaskGiver taskGiver;
    Player playerHealth;
    SceneControls sceneControls;
    bool isIncremented = false;

    public bool IsIncremented
    {
        get { return isIncremented; }
        set { isIncremented = value; }
    }

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

            if (!isIncremented)
            {
                Debug.Log($"Active Level after incr : " + DataPersistenceManager.instance.ActiveLevel);
                Debug.Log($"Max Level : " + DataPersistenceManager.instance.gameData.MaxlevelReached);

                if (DataPersistenceManager.instance.ActiveLevel == DataPersistenceManager.instance.gameData.MaxlevelReached)
                {
                    DataPersistenceManager.instance.ActiveLevel++;
                    DataPersistenceManager.instance.gameData.MaxlevelReached++;
                    DataPersistenceManager.instance.WriteFile();
                }

                isIncremented = true;
            }
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
