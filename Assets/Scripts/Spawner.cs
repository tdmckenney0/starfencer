using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public Enemy[] prefabs;
    public Score scoreboard;
    public Text waveIndicator; 
    public int poolSizeX = 5;
    public int poolSizeY = 2;
    public int enemyPoolSize = 15;
    public int spacing = 1;

    private int currentWave = 0;

    private List<Enemy> enemyPool;

	void FixedUpdate()
    {
        if (transform.childCount <= 0)
        {
            /* PopulatePool();
            ShufflePool(); */

            SpawnEnemies();
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

        currentWave++;
        waveIndicator.text = "WAVE: " + currentWave.ToString();

        for (int y = 0; y < poolSizeY; y++)
        {
            for (int x = 0; x < poolSizeX; x++)
            {
                int rand = Random.Range(0, prefabs.Length);

                Enemy clone = Instantiate<Enemy>(prefabs[rand]);

                clone.transform.position = new Vector3(this.transform.position.x - (float) poolSizeX - 1, this.transform.position.y) + (new Vector3(x, y) * spacing);
                clone.scoreboard = scoreboard;
                clone.transform.SetParent(this.transform);
            }
        }
    }
}
