
using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour
{
    public int currentRequest=-1 ;
    public bool isWaiting = false;
    public float requestDuration = 6f;
    public float cooldownDuration = 5f;

    public GameObject pizzaIcon;

    void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState state)
    {
        if(state != GameState.Day)
        {

            isWaiting = false;
            pizzaIcon.SetActive(false);
            StopAllCoroutines();
        }
    }

    public void StartRequest(int foodType)
    {
        if (isWaiting) return;

        currentRequest = foodType;
        isWaiting = true;
         pizzaIcon.SetActive(true);
        Debug.Log(name + " sipariş verdi: " + foodType);

        StartCoroutine(RequestTimer());
    }

    public void TryDeliverFood(int foodType)
    {
        if (!isWaiting) return;

        if (foodType == currentRequest)
        {
            Debug.Log("Doğru yemek verildi!");
            // PUAN ARTIR
        }
        else
        {
            Debug.Log("YANLIŞ yemek verildi!");
            // PUAN DÜŞÜR
        }

        StopAllCoroutines();
        isWaiting = false;
         pizzaIcon.SetActive(false);
        currentRequest = -1;
        StartCoroutine(Cooldown());
    }

    IEnumerator RequestTimer()
    {
        float timer = requestDuration;

    while (timer > 0f)
    {
        timer -= Time.deltaTime;
        yield return null;
    }
        yield return new WaitForSeconds(requestDuration);
        if (isWaiting)
        {
            Debug.Log("Süre doldu, sipariş iptal!");
            // PUAN DÜŞÜR
            isWaiting = false;
             pizzaIcon.SetActive(false);
            currentRequest = -1;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Random.Range(cooldownDuration,cooldownDuration*2f));
        // tekrar sipariş alabilir
    }
}
