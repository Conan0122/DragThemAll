using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Quest
{
    #region Enums
    public enum GoalType
    {
        Kill,Survive
    }
    #endregion

    #region Variable Initilization

    [SerializeField] GameObject goalPanel;
    [SerializeField] string description;
    public TextMeshProUGUI descriptionText;
    [SerializeField] string attacker;
    [SerializeField] int requiredQuantity;
    public TextMeshProUGUI quantityText;
    [SerializeField] GoalType typeOFGoal;
    [SerializeField] int currentQuantity = 0;

    #endregion


    #region Getters and Setters

    public GoalType TypeOFGoal { get { return typeOFGoal; } set { typeOFGoal = value; } }
    public string Attacker { get { return attacker; } set { attacker = value; } }
    public int RequiredQuantity { get { return requiredQuantity; } set { requiredQuantity = value; } }
    public int CurrentQuantity { get { return currentQuantity; } set { currentQuantity = value; } }
    public string Description { get { return description; } set { description = value; } }

    #endregion

}



