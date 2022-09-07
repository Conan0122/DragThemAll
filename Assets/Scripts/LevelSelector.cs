using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    #region Variable Initialization
    TextMeshProUGUI levelText;
    LevelSelector[] levelSelectors;

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
        levelSelectors = FindObjectsOfType<LevelSelector>();

        LevelText();
    }

    private void Update()
    {
        LevelUnlock();
    }

    void LevelText()
    {
        if (Level <= 0)
        {
            levelText.fontSize = 32;
            levelText.text = "Coming soon";
        }
        else
            levelText.text = Level.ToString();
    }

    void LevelUnlock()
    {
        foreach (var levelSelector in levelSelectors)
        {
            DataPersistenceManager.instance.ReadFile();

            if (levelSelector.Level <= DataPersistenceManager.instance.gameData.MaxlevelReached)
            {
                levelSelector.GetComponent<Button>().interactable = true;
            }
            else if (levelSelector.Level > DataPersistenceManager.instance.gameData.MaxlevelReached)
            {
                levelSelector.GetComponent<Button>().interactable = false;
            }
        }
    }

}
