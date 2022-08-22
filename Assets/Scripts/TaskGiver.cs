using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskGiver : MonoBehaviour
{
    #region Variable Initialization

    [SerializeField] Quest[] quests;

    #endregion

    private void Awake()
    {
        QuestMechanism();
    }

    private void Update()
    {
        QuestMechanism();
        if (AllQuestCompleted())
        {
            Debug.Log($"Completed");
        }
    }

    void QuestMechanism()
    {
        foreach (var quest in quests)
        {
            quest.descriptionText.text = quest.Description;
            quest.quantityText.text = quest.CurrentQuantity + "/" + quest.RequiredQuantity;
        }
    }

    public bool GoalReached(Quest quest)
    {
        return (quest.CurrentQuantity >= quest.RequiredQuantity);
    }

    public bool AllQuestCompleted()
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

    public void GetAttacker(string currentAttacker)
    {
        foreach (var quest in quests)
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
    }


}
