using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycolllision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Çarpışma Algılandı: " + other.gameObject.name);
        Destroy(other.gameObject);
        LevelManager.Instance.EarnMoney(-10);
        
            // Burada çarpışma sonrası yapılacak işlemleri ekleyebilirsiniz
        // Örneğin, oyuncunun canını azaltmak veya düşmanı yok etmek gibi

    }
}
