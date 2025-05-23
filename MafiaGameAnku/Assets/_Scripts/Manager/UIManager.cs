using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI MoneyText;

    public TextMeshProUGUI MoneyFriendlyText;

    public GameObject BeforeNightCanvas;
    public GameObject DeathCanvas;
    public GameObject NextDayCanvas;
    public TextMeshProUGUI DialogueText;

    public String[] DialogueTexts;

    public Transform Tutorial;

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

    public void UpdateMoneyFriendlyText()
    {
        MoneyFriendlyText.text = LevelManager.Instance.EarnedMoneyInDay + "/100 = " + (LevelManager.Instance.EarnedMoneyInDay / 100).ToString();
    }

    public void OpenDeathCanvas()
    {
        DeathCanvas.SetActive(true);
    }

    public void OpenNextDayCanvas()
    {
        UpdateDialogueText();
        NextDayCanvas.SetActive(true);
    }


    public void NextDayButton()
    {
        LevelManager.Instance.StartNextDay();
        NextDayCanvas.SetActive(false);
    }

    public void RestartGameButton()
    {
        LevelManager.Instance.StartDay();
        DeathCanvas.SetActive(false);
    }

    public void UpdateDialogueText()
    {
        DialogueText.text = DialogueTexts[LevelManager.Instance.DayIndex];
    }

    public void ToggleATutorial(int index)
    {
        foreach (Transform child in Tutorial)
        {
            child.gameObject.SetActive(false);
        }
        Tutorial.GetChild(index).gameObject.SetActive(true);
    }
    
    public void HideAllTutorials()
    {
        foreach (Transform child in Tutorial)
        {
            child.gameObject.SetActive(false);
        }
    }
}
