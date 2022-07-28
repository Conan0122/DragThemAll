using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnPractice : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] GameObject[] characterPrefabs;
    [Header("Practice")]
    [SerializeField] int[] table; // Where we count weights of characters
    [SerializeField] int total;
    [SerializeField] int randomNumber;
    // [SerializeField] GameObject[] bossCharacterPrefabs;
    [SerializeField] Transform characterSpawnLocation;
    [SerializeField] float minSpawnDuration = 1f, maxSpawnDuration = 5f;



    bool spawn = true;


    IEnumerator Start()
    {
        float spawnDurationGap = Random.Range(minSpawnDuration, maxSpawnDuration);

        // to be put inside Start() 
        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        Debug.Log("randomNumberrr before foreach = " + randomNumber);

        for (int i = 0; i < table.Length; i++)   // table length = 3
        {
            if (randomNumber <= table[i])
            {
                Debug.Log(" Table " + table[i]);

                // spawning character
                while (spawn)
                {
                    yield return new WaitForSeconds(spawnDurationGap);
                    RandomEnemySpawn();
                }
                break;
            }
            else
            {
                randomNumber -= table[i];
            }
        }





    }

    // Spawn random enemy in random spawner
    void RandomEnemySpawn()
    {
        int randomCharacter = Random.Range(0, characterPrefabs.Length);
        GameObject newCharacter = Instantiate(characterPrefabs[randomCharacter],
                                          characterSpawnLocation.position,
                                          Quaternion.identity);
        
        Debug.Log("Character spawned" + newCharacter.name);
    }

}
