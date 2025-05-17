using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject fake;
    public GameObject real;
    public Transform position;

    void Start()
    {
        int say = 0;
        int saygercek = 0;
        int kasa = 0;

        Vector3 startPos = position.position; // başlangıç pozisyonu

        for (int i = 0; i < 40; i++)
        {
            int olasılık = Random.Range(1, 10);

            // Gerçek/Sahte para oluştur
            if (olasılık < 8)
            {
                Instantiate(real, position.position, Quaternion.identity);
                saygercek++;
                Debug.Log("Gerçek: " + saygercek);
                // kasa += 100;
            }
            else
            {
                Instantiate(fake, position.position, Quaternion.identity);
                say++;
                Debug.Log("Sahte: " + say);
            }

            // Sağa kay (X ekseni)
            position.position += new Vector3(1, 0, 0);

            // Her 8 adımda: Z'yi artır, X'i sıfırla
            if ((i + 1) % 8 == 0)
            {
                position.position = new Vector3(startPos.x, startPos.y, position.position.z + 1);
            }
        }
    }

    void Update()
    {
    }
}
