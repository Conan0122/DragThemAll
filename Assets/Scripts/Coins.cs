/*      Handling game coins,
        Increment/decrement coins.
        Needs to be put wherever coins needed.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    #region Variable Initialization
    [SerializeField] TextMeshProUGUI coinsText;
    #endregion

    void Update()
    {
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        if (coinsText)
        {
            coinsText.text = DataPersistenceManager.instance.gameData.CurrentCoins.ToString();
        }
    }

    public bool IsCoinAvailable(int amount)
    {
        if (DataPersistenceManager.instance.gameData.CurrentCoins > amount) { return true; }
        else { return false; }
    }

    public void AddCoins(int amount)
    {
        Debug.Log(amount + " coins added");
        DataPersistenceManager.instance.gameData.CurrentCoins += amount;
        DataPersistenceManager.instance.SaveFile();
    }

    public void SpendCoins(int amount)
    {
        Debug.Log($"Coins decremented by " + amount);

        if (IsCoinAvailable(amount))
        {
            DataPersistenceManager.instance.gameData.CurrentCoins -= amount;
            DataPersistenceManager.instance.SaveFile();
        }
        else
        {
            Debug.Log($"Coins can't be decremented because you have " +
                    DataPersistenceManager.instance.gameData.CurrentCoins + " coins left.");
        }

    }


}
