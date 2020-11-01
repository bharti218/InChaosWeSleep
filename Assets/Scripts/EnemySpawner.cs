using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform spawningPoint;
    void Start()
    {
        StartCoroutine(EnemyWave());
    }


    IEnumerator EnemyWave()
    {
        while (true)
        {
            int respawningTime = Random.Range(2, 5);
            yield return new WaitForSeconds(respawningTime);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy =  Instantiate(enemyPrefab);
        enemy.transform.position = spawningPoint.transform.position;
    }
}
