using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    #region Variable Initialization
    TextMeshProUGUI levelText;

    [SerializeField] int level;

    #endregion

    #region Getters and setters
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    #endregion

    void Start()
    {
        levelText = GetComponentInChildren<TextMeshProUGUI>();

        LevelText();
    }

    void LevelText()
    {
        if (level <= 0)
        {
            levelText.fontSize = 32;
            levelText.text = "Coming soon";
        }
        else
        levelText.text = level.ToString();
    }
}
