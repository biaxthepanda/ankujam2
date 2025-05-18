
using UnityEngine;
using System.Collections;

public class RequestManager : MonoBehaviour
{
    
    public Transform[] foodSpawnPoints;

    public GameObject FoodPrefab;
    public Table[] tables;

    void Start()
    {
        StartCoroutine(SiparisDagitici());
    }

    IEnumerator SiparisDagitici()
    {
        
        while (true && GameManager.Instance.CurrentState == GameState.Day && LevelManager.Instance.DayIndex > 2)
        {
            Table t = tables[Random.Range(0, tables.Length)];
            if (!t.isWaiting)
            {
                int yemek = 0;     //Random.Range(0, 3);
                t.StartRequest(yemek);
            }
            foreach (var foodPoint in foodSpawnPoints)
            {
                if (foodPoint.childCount < 1)
                {
                    Instantiate(FoodPrefab, foodPoint.transform);

                }
            }
            yield return new WaitForSeconds(Random.Range(4f, 8f));
        }
    }
}
