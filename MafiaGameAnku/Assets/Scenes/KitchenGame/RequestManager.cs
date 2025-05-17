// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RequestManager : MonoBehaviour
// {
//     public Table[] tables;
//     public GameObject foodPrefabs; // yemek prefabları

//     void Start()
//     {
//         StartCoroutine(SiparisAt());
//     }

//     IEnumerator SiparisAt()
//     {
//         while (true)
//         {
//             Table t = tables[Random.Range(0, tables.Length)];
//             int yemek = Random.Range(0, 3); // 0,1,2 gibi yemek ID'leri
//             t.TryDeliverFood(foodPrefabs); // yemek gönder

//             yield return new WaitForSeconds(Random.Range(5f, 10f));
//         }
//     }
// }
using UnityEngine;
using System.Collections;

public class RequestManager : MonoBehaviour
{
    
    public Table[] tables;

    void Start()
    {
        StartCoroutine(SiparisDagitici());
    }

    IEnumerator SiparisDagitici()
    {
        
        while (true)
        {
            Table t = tables[Random.Range(0, tables.Length)];
            if (!t.isWaiting)
            {
                int yemek = Random.Range(0, 3);
                t.StartRequest(yemek);
            }
            yield return new WaitForSeconds(Random.Range(4f, 8f));
        }
    }
}
