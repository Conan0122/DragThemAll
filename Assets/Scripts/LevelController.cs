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
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
    #region Variable Initialization

    TaskGiver taskGiver;
    Player playerHealth;
    SceneControls sceneControls;
    Coins coins;

    [Header("Coins")]
    [SerializeField] TextMeshProUGUI coinRewardText;
    [SerializeField] int minCoinsToBeRewarded;
    [SerializeField] int maxCoinsToBeRewarded;
    [Header("Defenders")]
    [SerializeField] TextMeshProUGUI defenderRewardText;
    [SerializeField] SpriteRenderer defenderRewardSprite;
    [SerializeField] Sprite[] defenderSprites;
    [SerializeField] int minDefendersToBeRewarded;
    [SerializeField] int maxDefendersToBeRewarded;

    int randomCoinsForReward;
    int randomDefenderIndex;
    int randomDefendersAmtForReward;

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
        coins = FindObjectOfType<Coins>();

        if ((defenderRewardSprite && defenderRewardText))
        {
            defenderRewardSprite.gameObject.SetActive(false);
            defenderRewardText.gameObject.SetActive(false);
        }

        randomCoinsForReward = Random.Range(minCoinsToBeRewarded, maxCoinsToBeRewarded);
        Debug.Log($"coins to be rewarded" + randomCoinsForReward);

        randomDefendersAmtForReward = Random.Range(minDefendersToBeRewarded, maxDefendersToBeRewarded);
        Debug.Log($"Defenders to be rewarded " + randomDefendersAmtForReward);

        randomDefenderIndex = Random.Range(0, defenderSprites.Length);
        Debug.Log($"Random defender index to be rewarded = " + randomDefenderIndex);

    }

    private void Update()
    {
        OpenLevelCompletePopUp();
        OpenGameOverPopUp();
    }

    public void AddDefenderReward(int amt, int defIndex)
    {
        // Call this to reward defender to players
        // when they watch ads or complete level.
        DataPersistenceManager.instance.gameData.DefendersInfos[defIndex].Amt += amt;
        defenderRewardSprite.sprite = defenderSprites[defIndex];

        Debug.Log(
            DataPersistenceManager.instance.gameData.DefendersInfos[defIndex].Def +
            " Defender added " + amt);
        Debug.Log($"Defender sprite that is being rewarded " + defenderSprites[defIndex]);

        DataPersistenceManager.instance.SaveFile();
    }

    void OpenLevelCompletePopUp()
    {
        if (taskGiver.AllQuestCompleted() && playerHealth.CurrentHealth >= 0)
        {
            sceneControls.LevelCompletePopUp();

            // Destroy all the leftover attackers in scene
            var attackersLeftInScene = GameObject.Find("Attacker Parent");
            if (attackersLeftInScene)
            {
                Destroy(attackersLeftInScene);
            }

            if (!isIncremented && DataPersistenceManager.instance != null)
            {
                // Debug.Log($"Active Level before incr : " + DataPersistenceManager.instance.ActiveLevel);
                // Debug.Log($"Max Level before incr: " + DataPersistenceManager.instance.gameData.MaxlevelReached);

                if (DataPersistenceManager.instance.ActiveLevel == DataPersistenceManager.instance.gameData.MaxlevelReached)
                {
                    defenderRewardSprite.gameObject.SetActive(true);
                    defenderRewardText.gameObject.SetActive(true);

                    DataPersistenceManager.instance.ActiveLevel++;
                    DataPersistenceManager.instance.gameData.MaxlevelReached++;
                    coins.AddCoins(randomCoinsForReward);       // Show rewarded coins in pop up UI
                    AddDefenderReward(randomDefendersAmtForReward, randomDefenderIndex);

                    Debug.Log($"Active Level after incr : " + DataPersistenceManager.instance.ActiveLevel);
                    Debug.Log($"Max Level : " + DataPersistenceManager.instance.gameData.MaxlevelReached);

                    if (coinRewardText != null)
                    {
                        if (randomCoinsForReward >= 0)
                        {
                            coinRewardText.text = "x " + randomCoinsForReward.ToString();
                        }
                        else
                        {
                            coinRewardText.text = "x 0";
                        }
                    }

                    if (defenderRewardText != null)
                    {
                        if (randomDefendersAmtForReward >= 0)
                        {
                            defenderRewardText.text = "x " + randomDefendersAmtForReward.ToString();
                        }
                        else
                        {
                            defenderRewardText.text = "x 0";
                        }
                    }
                }

                // DataPersistenceManager.instance.SaveFile();
                DataPersistenceManager.instance.LoadFile();

                isIncremented = true;
            }
        }
    }

    void OpenGameOverPopUp()
    {
        if ((!taskGiver.AllQuestCompleted() && playerHealth.CurrentHealth <= 0) || playerHealth.CurrentHealth <= 0)
        {
            sceneControls.GameOverPopUp();

            // Destroy all the leftover attackers in scene
            var attackersLeftInScene = GameObject.Find("Attacker Parent");
            if (attackersLeftInScene)
            {
                Destroy(attackersLeftInScene);
            }
        }
    }

}
