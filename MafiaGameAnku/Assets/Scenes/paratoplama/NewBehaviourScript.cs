using System.Collections;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    public GameObject fake;
    public GameObject real;
    public Transform spawnStartPosition;
    public int moneyAmount = 5;

    private int spawnedFake = 0;
    private int spawnedReal = 0;

    private Vector3 nextSpawnPos;

    public float[] SpawnDelayPerDay;

    void Start()
    {
        StartCoroutine(RepeatSpawning());
    }

    IEnumerator RepeatSpawning()
    {
        while (GameManager.Instance.CurrentState == GameState.Day)
        {
            nextSpawnPos = spawnStartPosition.position;
            yield return StartCoroutine(SpawnMoneyRoutine());

            yield return new WaitForSeconds(SpawnDelayPerDay[LevelManager.Instance.DayIndex]); // 10 saniye sonra yeni para seti başlasın
        }
    }

    IEnumerator SpawnMoneyRoutine()
    {
        for (int i = 0; i < moneyAmount; i++)
        {
            int chance = Random.Range(1, 10); // 1-9

            GameObject money;
            if (chance < 7)
            {
                Instantiate(real, nextSpawnPos, Quaternion.identity);
                spawnedReal++;
                // kasa += 100;
            }
            else
            {
                Instantiate(fake, nextSpawnPos, Quaternion.identity);
                spawnedFake++;
            }

            // Pozisyon güncelle
            nextSpawnPos += new Vector3(0.2f, 0f, 0f);
            if ((i + 1) % 8 == 0)
            {
                nextSpawnPos = new Vector3(spawnStartPosition.position.x, spawnStartPosition.position.y, nextSpawnPos.z + 0.4f);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
