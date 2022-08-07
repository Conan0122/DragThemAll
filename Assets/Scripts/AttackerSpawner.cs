// Handling functionality of attacker spawners
// Probability based spawning

using System.Collections;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    #region Variable Initialization
    GameTimer gameTimer;

    [Header("Character Data")]
    [SerializeField] GameObject[] attackerPrefabs;
    [SerializeField] int[] weightsTable;    //  table of weights for each characters

    [Header("Spawning Data")]
    [Space(10)]
    [SerializeField] Transform[] attackerSpawnLocation;
    [SerializeField] float minDurationToSpawn = 0.5f, maxDurationToSpawn = 1.5f;

    [Space(15)]
    int totalOfWeights;
    int randomNumber;
    int randomSpawner;
    float randomSpawnDelay;
    bool attackerSpawn = false;

    #endregion

    #region Getters and setters
    public bool AttackerSpawn
    {
        get { return attackerSpawn; }
        set { attackerSpawn = value; }
    }
    #endregion


    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();

        StartCoroutine(CalculateSpawn());
    }

    IEnumerator CalculateSpawn()
    {
        // Get the total of weights
        foreach (var item in weightsTable)
        {
            totalOfWeights += item;
        }
        
        while (!gameTimer.LevelTimerIsReached)
        {
            // Random Weighted based Spawning Algorithm
            // Iterate through the table till we get index where our random number falls
            for (int currentTableIndex = 0; currentTableIndex <= weightsTable.Length; currentTableIndex++)
            {
                //  Instantiate attacker if random number is less than equal to current index in table
                if (randomNumber <= weightsTable[currentTableIndex])
                {
                    randomSpawnDelay = Random.Range(minDurationToSpawn, maxDurationToSpawn);
                    SpawnAttackers(currentTableIndex);
                    yield return new WaitForSeconds(randomSpawnDelay);
                    randomNumber = Random.Range(0, totalOfWeights);
                    break;
                }
                else
                {
                    randomNumber -= weightsTable[currentTableIndex];
                }
            }

        }
    }

    void SpawnAttackers(int currentTableIndex)
    {
        randomSpawner = Random.Range(0, attackerSpawnLocation.Length);

        if (AttackerSpawn == true)
        {
            GameObject newAttacker = Instantiate(attackerPrefabs[currentTableIndex],
                                             attackerSpawnLocation[randomSpawner].position,
                                             Quaternion.identity);
        }

    }
}
