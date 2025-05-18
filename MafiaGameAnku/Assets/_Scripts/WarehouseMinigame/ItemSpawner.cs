using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform spawnPoint;
    public List<BoxController> boxes;
    private int maxItems = 4;
    private float nextSpawnTime = 0f;

    public float[] SpawnRatesByDay;

    void Update()
    {
        if (LevelManager.Instance.DayIndex < 1) return;
        if (GameManager.Instance.CurrentState != GameState.Day)
        {
            nextSpawnTime = Time.time + SpawnRatesByDay[LevelManager.Instance.DayIndex];
            return;
        }
        if (GameObject.FindGameObjectsWithTag("FallingItem").Length >= maxItems) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnItem();
            nextSpawnTime = Time.time + SpawnRatesByDay[LevelManager.Instance.DayIndex];
        }
    }

    void SpawnItem()
    {
        int rand = Random.Range(0, boxes.Count);
        var box = boxes[rand];
        Debug.Log(rand.ToString());
        if (!box) return;
        
        SoundManager.Instance.PlaySFX(SoundEffects.Horn);
        GameObject item = Instantiate(itemPrefabs[box.BoxType], spawnPoint.position, this.transform.rotation);
        var falling = item.GetComponent<FallingItem>();
        falling.targetBox = box;
    }
}
