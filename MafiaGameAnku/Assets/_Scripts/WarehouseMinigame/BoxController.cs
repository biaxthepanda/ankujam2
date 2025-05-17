using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public float requestInterval = 10f; // her 10 saniyede bir istek gelsin
    public float requestDuration = 5f;  // isteğin süresi

    private float intervalTimer;
    private float requestTimer;
    private bool isRequestActive = false;

    public char requiredKey;
    public string requiredItemName; // opsiyonel: malzeme türü

    public int BoxType = 0; // 0: silah, 1: mücevher

    public TextMeshPro requestText; // isteği göstermek için UI

    private void Start()
    {
        StartRequest();
        intervalTimer = Random.Range(2f, 5f); // başta rastgele başlasın
    }

    private void Update()
    {
        if (!isRequestActive)
        {
            intervalTimer -= Time.deltaTime;

            if (intervalTimer <= 0f)
            {
                StartRequest();
            }
        }
        else
        {
            requestTimer -= Time.deltaTime;

            if (requestTimer <= 0f)
            {
                isRequestActive = false;
                intervalTimer = Random.Range(5f, 10f); // tekrar için bekle
                // TODO: Trigger fail
            }
        }
    }

    private void StartRequest()
    {
        isRequestActive = true;
        requestTimer = requestDuration;
        requiredKey = (char)Random.Range(65, 91); // A-Z
        requiredItemName = GetRandomItemName(); // Malzeme türü (örnek)
        requestText.text = requiredKey.ToString();
        // TODO: Visual feedback ile oyuncuya göster
        Debug.Log($"[BOX] Request started: Item={requiredItemName}, Key={requiredKey}");
    }

    public void FulfillRequest(bool isCorrect)
    {
        if (!isRequestActive) return;

        isRequestActive = false;

        if (isCorrect)
        {
            // TODO: Trigger success
            LevelManager.Instance.EarnMoney(10); // örnek: 10 para kazan
            Debug.Log($"[BOX] Correct key '' => SUCCESS");
        }
        else
        {
            LevelManager.Instance.EarnMoney(-10); 
            Debug.Log($"[BOX] Wrong key '' => FAIL real '{requiredKey}'");
        }

        intervalTimer = Random.Range(5f, 10f); // tekrar için bekle
    }

    private string GetRandomItemName()
    {
        string[] items = { "Wood", "Stone", "Metal", "Cloth" };
        return items[Random.Range(0, items.Length)];
    }

    public bool IsRequestActive()
    {
        return isRequestActive;
    }
}
