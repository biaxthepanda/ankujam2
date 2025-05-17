using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int mobsayısı = 5;

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {

       
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    IEnumerator SpawnEnemiesLoop()
    {
        while (true)
        {
            SpawnEnemyGroup();
            yield return new WaitForSeconds(10f);
        }
    }

    void SpawnEnemyGroup()
    {
        for (int i = 0; i < mobsayısı; i++)
        {
            Vector3 offset = new Vector3((Random.Range(0, 2) * 2 - 1) * Random.Range(1f, 2f) * i, 0f, 0f);
            Vector3 spawnPos = spawnPoint.position + offset;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);
            enemies.Add(enemy);
        }
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Day && LevelManager.Instance.DayIndex > 2)
        {
             StartCoroutine(SpawnEnemiesLoop());
        }
        else
        {
            StopAllCoroutines();
        }
        

    }
}
