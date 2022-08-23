using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskGiver : MonoBehaviour
{
    #region Variable Initialization

    [SerializeField] Quest[] quests;
    GameTimer gameTimer;

    bool isIncremented = false;

    #endregion

    private void Awake()
    {
        QuestTextUpdate();
    }

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    private void Update()
    {
        QuestTextUpdate();
        if (AllQuestCompleted())
        {
            Debug.Log($"Completed");
            // Something to do after all quests are completed
        }
    }

    void QuestTextUpdate()
    {
        foreach (var quest in quests)
        {
            quest.descriptionText.text = quest.Description;
            quest.quantityText.text = quest.CurrentQuantity + "/" + quest.RequiredQuantity;
        }
    }

    bool GoalReached(Quest quest)
    {
        return (quest.CurrentQuantity >= quest.RequiredQuantity);
    }

    bool AllQuestCompleted()
    {
        foreach (var quest in quests)
        {
            if (!GoalReached(quest))
            {
                return false;
            }
        }
        return true;
    }
    
    public void IncrementQuest(string currentAttacker, Quest.GoalType goalType)
    {

        foreach (var quest in quests)
        {
            if (goalType == Quest.GoalType.Kill)
            {
                if (quest.Attacker == "All" && !GoalReached(quest))
                {
                    quest.CurrentQuantity++;
                }
                else if (currentAttacker == quest.Attacker + "(Clone)" && !GoalReached(quest))
                {
                    quest.CurrentQuantity++;
                }
            }
            else if (goalType == Quest.GoalType.Survive && currentAttacker == null)
            {
                if (!isIncremented)
                {
                    quest.CurrentQuantity++;
                    isIncremented = true;
                }

            }
        }
    }


}
