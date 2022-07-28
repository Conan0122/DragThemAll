// Copied from CharacterSpawnPractice2
// This script is full of my own experimental code
// So far this one is working well
// this script is working all good

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterSpawnCode : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] Transform[] characterSpawnLocation;

    [Header("Probability")]
    [Space(10)]
    [SerializeField] int[] table;
    [SerializeField] int total;
    [SerializeField] int randomNumberrr;

    bool spawn = true;


    IEnumerator Start()
    {
        foreach (var item in table)
        {
            total += item;
        }

        while (spawn)
        {
            // Get random character spawn location
            int getRandomSpawner = Random.Range(0, characterSpawnLocation.Length);

            // Now iterate through the table till we get index where our random number falls
            for (int currentTableIndex = 0; currentTableIndex < table.Length; currentTableIndex++)   // table length = 3
            {
                if (randomNumberrr <= table[currentTableIndex])
                {
                    yield return new WaitForSeconds(1.5f);  // Put random range for delay in spawning

                    GameObject newCharacter = Instantiate(characterPrefabs[currentTableIndex],
                                                          characterSpawnLocation[getRandomSpawner].position,
                                                          Quaternion.identity);

                    /*  
                        ---Put every new enemy gameobject under a single gameobject/ folder---
                        newCharacter.transform.parent = new GameObject().transform;
                    */

                    randomNumberrr = Random.Range(0, total);
                    break;
                }
                else
                {
                    randomNumberrr -= table[currentTableIndex];
                }
            }
        }
    }
}
