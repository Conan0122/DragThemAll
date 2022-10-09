/*      Responsible for the shop mechanism,
        like buying defenders, coins, premium and modifying game effects like trails and death VFXs.
        Not in use right now.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Coins coins;

    int defAmount;
    int defIndex;

    void Start()
    {
        coins = FindObjectOfType<Coins>();
    }

    public void DefAmount(int amount)
    {
        //  Get defender amount to buy
        this.defAmount = amount;
    }

    public void DefIndex(int index)
    {
        //  Get Defender index
        this.defIndex = index;
    }

    public void BuyDefender(int cost)
    {
        // If coins available,
        // user can buy defenders
        // else can't.
        if (coins.IsCoinAvailable())
        {
            DataPersistenceManager.instance.gameData.DefendersInfos[defIndex].Amt += defAmount;
            coins.SpendCoins(cost);
        }
        else
        {
            Debug.Log($"No coins to buy any defender, buy some coins first");
        }
    }



}
