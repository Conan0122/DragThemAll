/*      For handling Persistence data,
        across scenes.
        Singleton class
*/

using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;

public class DataPersistenceManager : MonoBehaviour
{
    #region Variable Initialization

    public static DataPersistenceManager instance;
    public GameData gameData = new GameData();

    string filePath;
    [SerializeField] int activeLevel;

    [Header("Encyption and Decryption")]
    [SerializeField] bool encryptEnabled = false;       // For Debugging purpose
    byte[] ivBytes = new byte[16];
    byte[] keyBytes = new byte[16];

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

        filePath = Application.persistentDataPath + "/game_data.json";
    }

    public void SaveFile()
    {
        // Serialize object into JSON
        string jsonData = JsonUtility.ToJson(gameData);

        string dataToSave = jsonData;

        // Encrypt Game Data
        if (encryptEnabled)
        {
            dataToSave = EncryptData(jsonData);
        }

        // Save Gamedata to filepath
        File.WriteAllText(filePath, dataToSave);
    }

    public void LoadFile()
    {
        // Check if file exist or not
        // If Not, then save New game data to file
        if (File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);
            Debug.Log($"file contents= " + fileContents);

            // Try Loading data
            // Else Load New game data to file
            try
            {
                string dataToLoad = fileContents;

                // Decrypt file contents / Game Data
                if (encryptEnabled)
                {
                    dataToLoad = DecryptData(fileContents);
                    Debug.Log($"Decrypted data " + dataToLoad);
                }

                // Deserialize object into JSON and save string to filepath
                gameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception)
            {
                // Create new game data file
                NewGameData();
                Debug.Log($"Error in file");
            }

        }
        else
        {
            NewGameData();
            Debug.LogError($"*********error//No filepath detected**********");
        }

    }

    public void NewGameData()
    {
        gameData.MaxlevelReached = 1;
        gameData.CurrentCoins = 100;
        gameData.IsDefaultTrailParticlesActive = false;
        gameData.BoughtTrailsAlready = false;
        activeLevel = 1;

        for (int i = 0; i < DataPersistenceManager.instance.gameData.DefendersInfos.Count; i++)
        {
            // Reset defenders amount
            gameData.DefendersInfos[i].Amt = 5;
        }

        SaveFile();
    }


    //          Encrpt and Decrypt
    string EncryptData(string dataToEncrypt)
    {
        byte[] encryptedArray;

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(keyBytes, ivBytes);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(dataToEncrypt);
                    }
                    encryptedArray = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(encryptedArray);
    }

    string DecryptData(string cipherText)
    {
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, ivBytes);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    public string ReturnActiveLevel(string activeLevel)
    {
        // Return the active Level when loading current level scene
        Int32.TryParse(activeLevel, out this.activeLevel);
        return activeLevel;
    }



}
