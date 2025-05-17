// using System.Collections;
// using UnityEngine;

// public class Table : MonoBehaviour
// {
//     public int currentRequest = -1; // -1 = istemiyor
//     public float requestCooldown = 5f;
//     public float requestDuration = 7f;
//     public bool isWaiting = false;

//     public void TryDeliverFood(GameObject foodObj)
//     {
//         if (!isWaiting) return;

//         int deliveredType = foodObj.GetComponent<Food>().foodType;

//         if (deliveredType == currentRequest)
//         {
//             Debug.Log("Doğru yemek!"); 
//             // Puanı arttır
//         }
//         else
//         {
//             Debug.Log("Yanlış yemek!");
//             // Puanı düşür
//         }

//         StopAllCoroutines();
//         StartCoroutine(Cooldown());
//     }

//     public void StartRequest(int foodType)
//     {
//         if (isWaiting) return;

//         currentRequest = foodType;
//         isWaiting = true;
//         Debug.Log("Masa yemek istedi: " + foodType);

//         StartCoroutine(RequestTimer());
//     }

//     IEnumerator RequestTimer()
//     {
//         yield return new WaitForSeconds(requestDuration);

//         if (isWaiting)
//         {
//             Debug.Log("Sipariş iptal oldu!");
//             // Puanı düşür
//             StartCoroutine(Cooldown());
//         }
//     }

//     IEnumerator Cooldown()
//     {
//         isWaiting = false;
//         currentRequest = -1;
//         yield return new WaitForSeconds(requestCooldown);
//         // tekrar isteyebilecek
//     }
// }
using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour
{
    public int currentRequest=-1 ;
    public bool isWaiting = false;
    public float requestDuration = 6f;
    public float cooldownDuration = 5f;

    public void StartRequest(int foodType)
    {
        if (isWaiting) return;

        currentRequest = foodType;
        isWaiting = true;
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
        currentRequest = -1;
        StartCoroutine(Cooldown());
    }

    IEnumerator RequestTimer()
    {
        float timer = requestDuration;

    while (timer > 0f)
    {
        Debug.Log("Kalan süre: " + Mathf.Ceil(timer));
        timer -= Time.deltaTime;
        yield return null;
    }
        yield return new WaitForSeconds(requestDuration);
        if (isWaiting)
        {
            Debug.Log("Süre doldu, sipariş iptal!");
            // PUAN DÜŞÜR
            isWaiting = false;
            currentRequest = -1;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        // tekrar sipariş alabilir
    }
}
