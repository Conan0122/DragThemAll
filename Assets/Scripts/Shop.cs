/*      Responsible for the shop mechanism,
        like buying defenders, coins, premium and modifying game effects like trails and death VFXs.
        Not in use right now.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Coins coins;
    ShortMsgPopUpManager shortMsgPopUpManager;

    int defAmount;
    int defIndex;

    const string NO_COIN_LEFT_ALERT = "Insufficient Coins, buy some coins first";
    const string BOUGHT_SUCCESSFULLY_ALERT = "Bought successfully";

    void Start()
    {
        coins = FindObjectOfType<Coins>();
        shortMsgPopUpManager = FindObjectOfType<ShortMsgPopUpManager>();
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
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        if (coins.IsCoinAvailable(cost))
        {
            DataPersistenceManager.instance.gameData.DefendersInfos[defIndex].Amt += defAmount;
            coins.SpendCoins(cost);
            shortMsgPopUpManager.ShowPopUpMessage(BOUGHT_SUCCESSFULLY_ALERT);
        }
        else
        {
            shortMsgPopUpManager.ShowPopUpMessage(NO_COIN_LEFT_ALERT);
            Debug.Log($"No coins to buy any defender, buy some coins first");
        }
    }

    public void BuyTrail(int cost)
    {
        AudioManager.instance.PlayAudio(Sounds.AudioName.NormalButtonClicks, false);
        if (coins.IsCoinAvailable(cost))
        {
            DataPersistenceManager.instance.gameData.BoughtTrailsAlready = true;
            coins.SpendCoins(cost);
            shortMsgPopUpManager.ShowPopUpMessage(NO_COIN_LEFT_ALERT);
            Debug.Log($"Bought");
            shortMsgPopUpManager.ShowPopUpMessage(BOUGHT_SUCCESSFULLY_ALERT);
        }
        else
        {
            AudioManager.instance.PlayAudio(Sounds.AudioName.EmptyDefenderSlot, false);
            shortMsgPopUpManager.ShowPopUpMessage(NO_COIN_LEFT_ALERT);
            Debug.Log($"No coins to buy any defender, buy some coins first");
        }
    }


}
