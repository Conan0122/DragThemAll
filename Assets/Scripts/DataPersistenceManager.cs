/*      For handling Persistence data,
        across scenes.
        Singleton class
*/

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DataPersistenceManager : MonoBehaviour
{
    #region Variable Initialization

    public static DataPersistenceManager instance;

    public GameData gameData = new GameData();

    string filePath;
    [SerializeField] int activeLevel;

    #endregion

    #region Getters and setters

    public int ActiveLevel
    {
        get { return activeLevel; }
        set { activeLevel = value; }
    }

    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        filePath = Application.persistentDataPath + "/game_data.dta";
    }

    public void WriteFile()
    {
        // Serialize object into JSON and save string to filepath
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, jsonData);
    }

    public void ReadFile()
    {
        // Check if file exist or not
        if (File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);

            // Deserialize object into JSON and save string to filepath
            gameData = JsonUtility.FromJson<GameData>(fileContents);
        }
        else
            Debug.LogError($"*********error**********");
    }

    public string ReturnActiveLevel(string activeLevel)
    {
        Int32.TryParse(activeLevel, out this.activeLevel);
        return activeLevel;
    }



}
