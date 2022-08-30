using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskGiver : MonoBehaviour
{
    #region Variable Initialization

    [SerializeField] Quest[] quests;

    bool isIncremented = false;

    #endregion

    private void Awake()
    {
        QuestTextUpdate();
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
            if (quest.TypeOFGoal == Quest.GoalType.Kill && quest.Attacker == "All")
            {
                quest.DescriptionText.text = "Destroy any Attacker " + quest.RequiredQuantity + " times.";
            }
            else if (quest.TypeOFGoal == Quest.GoalType.Kill)
            {
                quest.DescriptionText.text = "Destroy " + quest.Attacker + " " + quest.RequiredQuantity + " times.";
            }
            else if (quest.TypeOFGoal == Quest.GoalType.Survive)
            {
                //  Make sure RequiredQuantity is always 1 in case of Survive Quest
                quest.RequiredQuantity = 1;
                quest.DescriptionText.text = "Survive till the end.";
            }
            quest.QuantityText.text = quest.CurrentQuantity + "/" + quest.RequiredQuantity;
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

            if (GoalReached(quest))
            {
                quest.CheckBox.gameObject.SetActive(true);
                quest.QuantityText.enabled = false;
            }
        }
    }


}
