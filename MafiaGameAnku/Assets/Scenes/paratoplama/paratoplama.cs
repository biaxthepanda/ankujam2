using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class paratoplama : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject fake;
    GameObject real;    
    public TextMeshProUGUI scoreText;
    public int score;

    void Start()
    {
        
    }

    // Update is called once per frame
     public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Skor: " + score;
    }
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
                    real = obj;
                    Debug.Log("Gerçek paraya tıkladın!");
                    Destroy(real);
                    AddScore(100);
                }
                else if (obj.CompareTag("fake"))
                {
                    fake = obj;
                    Debug.Log("Sahte paraya tıkladın!");
                    Destroy(fake);
                }
        }
    }
    }
}
