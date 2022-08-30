using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Quest
{
    #region Enums
    public enum GoalType
    {
        Kill, Survive
    }
    #endregion

    #region Variable Initilization

    [SerializeField] GameObject goalPanel;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] string attacker;
    [SerializeField] int requiredQuantity;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] Image checkBox;
    [SerializeField] GoalType typeOFGoal;
    [SerializeField] int currentQuantity = 0;

    #endregion


    #region Getters and Setters

    public GoalType TypeOFGoal { get { return typeOFGoal; } set { typeOFGoal = value; } }
    public Image CheckBox { get { return checkBox; } set { checkBox = value; } }
    public string Attacker { get { return attacker; } set { attacker = value; } }
    public int RequiredQuantity { get { return requiredQuantity; } set { requiredQuantity = value; } }
    public int CurrentQuantity { get { return currentQuantity; } set { currentQuantity = value; } }
    public TextMeshProUGUI DescriptionText { get { return descriptionText; } set { descriptionText = value; } }
    public TextMeshProUGUI QuantityText { get { return quantityText; } set { quantityText = value; } }

    #endregion

}



