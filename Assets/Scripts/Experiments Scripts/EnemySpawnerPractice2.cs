using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPractice2 : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform[] enemySpawnerPoints;
    

    void Start()
    {

        for(int i = 0; i<=60; i++)
        {
            SpawnRandomEnemy(new Vector2(Random.Range(0, enemySpawnerPoints.Length),
            Random.Range(0, enemySpawnerPoints.Length)));
        }

        
    }

    void Update()
    {
        
    }

    void SpawnRandomEnemy(Vector2 position)
    {
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(prefab, position, Quaternion.identity);
        Debug.Log(prefab.name);
    }
}
