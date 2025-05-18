using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI MoneyText;

    public TextMeshProUGUI MoneyFriendlyText;

    public GameObject BeforeNightCanvas;


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
        MoneyText.text = "PARA: " + money.ToString();
    }

    public void OpenBeforeNightCanvas()
    {
        BeforeNightCanvas.SetActive(true);
        UpdateMoneyFriendlyText();
    }

    public void StartShoot()
    {
        LevelManager.Instance.StartShooting();
        BeforeNightCanvas.SetActive(false);

    }

    public void UpdateMoneyFriendlyText( )
    {
        MoneyFriendlyText.text = LevelManager.Instance.EarnedMoneyInDay+"/100 = " + (LevelManager.Instance.EarnedMoneyInDay/100).ToString();
    }
}
