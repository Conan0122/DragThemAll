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
    [SerializeField] List<DefendersInfo> defendersInfos;

    #endregion

    #region Getters and setters
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

    public List<DefendersInfo> DefendersInfos
    {
        get { return defendersInfos; }
        set { defendersInfos = value; }
    }
    #endregion

}

[Serializable]
public class DefendersInfo
{
    [SerializeField] DefenderName def;
    [SerializeField] int amt;

    #region Getters and Setters

    public DefenderName Def
    {
        get { return def; }
        set { def = value; }
    }

    public int Amt
    {
        get { return amt; }
        set { amt = value; }
    }
    #endregion

}


