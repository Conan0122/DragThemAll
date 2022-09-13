/*      For holding values which needs to be saved
        like, level reached, coins, defender quantity.
*/

using UnityEngine;

[System.Serializable]
public class GameData
{
    #region Variable Initialization

    [SerializeField] int maxLevelReached;
    [SerializeField] int currentCoins;

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
