using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public Enemy[] prefabs;
    public Score scoreboard;
    public Text waveIndicator;
    public int enemyPoolSize = 15;

    private int currentWave = 0;

    public SpawnPoint[] spawnPoints;

    private List<Enemy> enemyPool;

    void Awake()
    {
        PopulatePool();
        spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
        InvokeRepeating("CheckIfEnemiesAreActive", 1f, 1f);
    }
    
    void CheckIfEnemiesAreActive()
    {
        foreach(Enemy e in enemyPool)
        {
            if(e.IsActive())
            {
                return;
            }
        }

        SpawnEnemies();
    }

    void PopulatePool()
    {
        enemyPool = new List<Enemy>();

        for (int i = 0; i < enemyPoolSize; i++)
        {
            int rand = Random.Range(0, prefabs.Length);

            Enemy clone = Instantiate<Enemy>(prefabs[rand]);

            clone.gameObject.SetActive(false);
            clone.transform.SetParent(this.transform);

            enemyPool.Add(clone);
        }
    }

    void ShufflePool()
    {
        List<Enemy> newEnemyPool = new List<Enemy>();

        int randomIndex = 0;

        while(enemyPool.Count > 0)
        {
            randomIndex = Random.Range(0, enemyPool.Count);
            newEnemyPool.Add(enemyPool[randomIndex]);
            enemyPool.RemoveAt(randomIndex);
        }

        enemyPool = newEnemyPool;
    }

    void SpawnEnemies()
    {
        int rand;

        currentWave++;
        waveIndicator.text = "WAVE: " + currentWave.ToString();

        foreach(SpawnPoint spawnPoint in spawnPoints) 
        {
            do
            {
                rand = Random.Range(0, enemyPool.Count - 1);
            }
            while (enemyPool[rand].IsActive());

            enemyPool[rand].transform.position = spawnPoint.transform.position; 
            enemyPool[rand].scoreboard = scoreboard;
            enemyPool[rand].gameObject.SetActive(true);
        }
    }
}
