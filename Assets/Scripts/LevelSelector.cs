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
    [SerializeField] Image lockImage;

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
            DataPersistenceManager.instance.LoadFile();

            if (levelSelector.Level <= DataPersistenceManager.instance.gameData.MaxlevelReached)
            {
                //  Unlocked levels
                levelSelector.GetComponent<Button>().interactable = true;
                levelSelector.lockImage.enabled = false;
                levelSelector.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
            else if (levelSelector.Level > DataPersistenceManager.instance.gameData.MaxlevelReached)
            {
                //  Locked levels
                levelSelector.GetComponent<Button>().interactable = false;
                levelSelector.lockImage.enabled = true;
                levelSelector.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }

}
