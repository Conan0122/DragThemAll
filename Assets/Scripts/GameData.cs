/*      For holding values which needs to be saved
        like, level reached, coins, defender quantity.
*/

using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    #region Variable Initialization

    [SerializeField] int maxLevelReached;
    [SerializeField] int currentCoins;
    [SerializeField] public List<DefendersInfo> defendersInfos;

    #endregion

    public int MaxlevelReached
    {
        get { return maxLevelReached; }
        set { maxLevelReached = value; }
    }

    public int CurrentCoins
    {
        get { return currentCoins; }
        set { currentCoins = value; }
    }

}

[Serializable]
public class DefendersInfo
{
    public DefenderName def;
    public int amt;
}


