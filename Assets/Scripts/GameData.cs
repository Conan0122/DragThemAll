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
    [SerializeField] bool isDefaultTrailParticlesActive;
    [SerializeField] bool boughtTrailsAlready;
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
    
    //  For getting and setting trail particles status
    public bool IsDefaultTrailParticlesActive
    {
        get { return isDefaultTrailParticlesActive; }
        set { isDefaultTrailParticlesActive = value; }
    }

    //  For getting and setting "if user ever bought trail particles before" 
    public bool BoughtTrailsAlready
    {
        get { return boughtTrailsAlready; }
        set { boughtTrailsAlready = value; }
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


