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

    private List<Enemy> enemyPool;
    private List<Enemy> activeEnemies = new List<Enemy>();

    void Awake()
    {
        PopulatePool();
        InvokeRepeating("CheckIfEnemiesAreActive", 1f, 1f);

        //Invoke("CheckIfEnemiesAreActive", 1f)
    }

	void FixedUpdate()
    {
        if(activeEnemies.Count <= 0)
        {
            SpawnEnemies();
        }
	}

    void CheckIfEnemiesAreActive()
    {
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            if(!activeEnemies[i].IsActive())
            {    
                activeEnemies.RemoveAt(i);
            }
        }
    }

    void PopulatePool()
    {
        enemyPool = new List<Enemy>();

        for(int i = 0; i < enemyPoolSize; i++)
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

        activeEnemies.Clear();

        foreach(GameObject spawnPoint in GameObject.FindGameObjectsWithTag("SpawnPoint")) 
        {

            do
            {
                rand = Random.Range(0, enemyPool.Count - 1);

            } while(activeEnemies.Contains(enemyPool[rand])); 
            

            enemyPool[rand].transform.position = spawnPoint.transform.position; 
            enemyPool[rand].scoreboard = scoreboard;
            enemyPool[rand].gameObject.SetActive(true);

            activeEnemies.Add(enemyPool[rand]);
        }
    }
}
