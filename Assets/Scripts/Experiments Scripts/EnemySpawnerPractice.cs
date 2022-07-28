using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPractice : MonoBehaviour
{
    [SerializeField]
    Transform[] enemySpawnerPoints;
    
    [SerializeField]
    GameObject[] enemyPrefab;

    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(Spawn());
    }


    void spawnEnemy()
    {
        var randomEnemy = Random.Range(0, enemyPrefab.Length);
        int randomEnemySpawnerPoints = Random.Range(0, enemySpawnerPoints.Length);

        // Instantiate(randomEnemy, new Vector3(Random.Range(0, enemySpawnerPoints.Length), 0, 0), Quaternion.identity);
        Instantiate (enemyPrefab[randomEnemy],enemySpawnerPoints[randomEnemySpawnerPoints].position, transform.rotation );
        
        // GameObject newCharacter = Instantiate(enemyPrefab[0], new Vector3(Random.Range(0, 5), Random.Range(0, 5)), Quaternion.identity);

    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        spawnEnemy();
        StartCoroutine(Spawn());
    }


}
