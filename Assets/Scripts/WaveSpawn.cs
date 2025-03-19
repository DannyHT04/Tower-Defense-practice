using System.Collections;
using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public Wave[] waves;
    public Transform spawnPoint;
    private int currentWaveIndex = 0;
    private int activeEnemies = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            Wave wave = waves[currentWaveIndex];
            yield return StartCoroutine(SpawnEnemies(wave));

            // Wait until all enemies are defeated
            yield return new WaitUntil(() => activeEnemies == 0);

            currentWaveIndex++;
            yield return new WaitForSeconds(5f); // Time between waves
        }
    }

    IEnumerator SpawnEnemies(Wave wave)
    {
        // need to know what enemy to spawn
        // need to know how much of those enemy to spawn

        foreach (var enemyInfo in wave.enemies)
        {
            for (int i = 0; i < enemyInfo.enemyCount; i++)
            {
                GameObject enemy = Instantiate(enemyInfo.enemyPrefab, spawnPoint.position, Quaternion.identity);
                activeEnemies++;
                enemy.GetComponent<EnemyHealth>().OnDeath += EnemyDefeated;
                yield return new WaitForSeconds(wave.spawnInterval);
            }
        }
    }

    void EnemyDefeated()
    {
        activeEnemies--;
    }
}
