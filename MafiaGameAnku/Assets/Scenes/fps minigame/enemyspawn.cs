using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int mobsayısı;

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemy();
        // InvokeRepeating(nameof(MoveEnemies), 1f, 1f);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < mobsayısı; i++)
        {
            Vector3 spawnPos = spawnPoint.position + new Vector3((Random.Range(0,1)*2-1) * Random.Range(1f,2f)*i, 0f, 0f); // aralıklı spawn
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);
            enemies.Add(enemy);
        }
    }

  
}
