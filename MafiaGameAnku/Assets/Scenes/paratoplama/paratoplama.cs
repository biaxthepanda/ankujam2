using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paratoplama : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log("Tıklanan obje: " + obj.name);
            Debug.Log("Tagi: " + obj.tag);

            // Örnek: Eğer tag "Apple" ise
            if (obj.CompareTag("real"))
            {
                Debug.Log("Gerçek paraya tıkladın!");
            }
            else if (obj.CompareTag("fake"))
            {
                Debug.Log("Sahte paraya tıkladın!");
            }
        }
    }
    }
}
