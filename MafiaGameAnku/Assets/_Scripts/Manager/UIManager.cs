using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI MoneyText;


    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    

    public void UpdateMoneyText(int money)
    {
         MoneyText.text = "MONEY:" + money.ToString();
    }
}
