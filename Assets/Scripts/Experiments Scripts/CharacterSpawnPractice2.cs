// Improvised version of CharacterSpawnPractice script
// Script with random generation of character with weighted probabilities
// finalized script for random character spawning

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnPractice2 : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] GameObject[] characterPrefabs;
    [SerializeField] Transform characterSpawnLocation;
    // [SerializeField] float minSpawnDuration = 1f, maxSpawnDuration = 5f;

    [Header("Probability")]
    [SerializeField] int[] table;
    [SerializeField] int total;
    [SerializeField] int randomNumberrr;



    IEnumerator Start()
    {
        foreach (var item in table)
        {
            total += item;
        }


        randomNumberrr = Random.Range(0, total);

        Debug.Log("randomNumberrr before foreach = " + randomNumberrr);

        for (int i = 0; i < table.Length; i++)   // table length = 3
        {
            if (randomNumberrr <= table[i])
            {
                    // SpawnCharacter();
                    // int randomCharacter = Random.Range(0, characterPrefabs.Length);
                    while (true)
                    {
                        yield return new WaitForSeconds(1f);
                        GameObject newCharacter = Instantiate(characterPrefabs[i],
                                                      characterSpawnLocation.position,
                                                      Quaternion.identity);

                        Debug.Log("Character name = " + newCharacter);
                        break;

                    }
                
            }
            else
            {
                randomNumberrr -= table[i];
            }
        }


        // void SpawnCharacter()
        // {
        //     int randomCharacter = Random.Range(0, characterPrefabs.Length);
        //     GameObject newCharacter = Instantiate(characterPrefabs[randomCharacter],
        //                                       characterSpawnLocation.position,
        //                                       Quaternion.identity);

        //     Debug.Log("Character spawned" + newCharacter.name);
        // }
    }


}
