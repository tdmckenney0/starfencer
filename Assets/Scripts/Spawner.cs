using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour { 

    public Enemy[] prefabs;
    public Score scoreboard;
    public Text waveIndicator;
    public int enemyPoolSize = 20;

    public int currentWave = 0;

    public SpawnPoint[] spawnPoints;

    public List<Enemy> enemyPool;

    // When Object is created in the Hierarchy //

    void Awake()
    {
        PopulatePool();
        spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
        InvokeRepeating("CheckIfEnemiesAreActive", 0.5f, 0.5f);
    }

    // Runs in background //
    
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

    // Randomly Populate the pool. //

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

    // Foreach spawnPoint, grab a random pool entry, move to spawnPoint, activate //

    void SpawnEnemies()
    {
        int rand;

        currentWave++;
        waveIndicator.text = "WAVE: " + currentWave.ToString();

        if(currentWave % 5 == 0)
        {
            PopulatePool(); // Refresh pool after five waves. //
        }

        foreach (SpawnPoint spawnPoint in spawnPoints) 
        {
            do
            {
                rand = Random.Range(0, enemyPool.Count - 1);
            }
            while (enemyPool[rand].IsActive()); // Roll the dice until we find an index *not* active //

            enemyPool[rand].transform.position = spawnPoint.transform.position; 
            enemyPool[rand].scoreboard = scoreboard;
            enemyPool[rand].gameObject.SetActive(true);
        }
    }
}
