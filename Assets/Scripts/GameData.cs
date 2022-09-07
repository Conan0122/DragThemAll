/*      For holding values which needs to be saved
        like, level reached, coins, defender quantity.
*/

using UnityEngine;

[System.Serializable]
public class GameData
{
    #region Variable Initialization

    [SerializeField] int maxLevelReached = 1;
    // [SerializeField] int coins = 100;

    #endregion

    public int MaxlevelReached
    {
        get { return maxLevelReached; }
        set { maxLevelReached = value; }
    }



}
