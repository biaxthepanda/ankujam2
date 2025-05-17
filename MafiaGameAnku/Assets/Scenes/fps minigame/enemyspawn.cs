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
            Vector3 spawnPos = spawnPoint.position + new Vector3(0f, 0f, i * 2f); // aralıklı spawn
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
        }
    }

    void MoveEnemies()
    {
        // foreach (GameObject enemy in enemies)
        // {
        //     if (enemy != null) // kontrol: öldü mü vs.
        //         enemy.transform.position += new Vector3(0f, 0f, 1f); // ileri hareket
        // }
    }
    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Çarpışma: " + collision.gameObject.name);
    // }
}
