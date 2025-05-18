using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Day,
    Night,
    Cinematic,
    BuyingBeforeNight,
    Start,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public static Action<GameState> OnGameStateChanged;

    public GameState CurrentState { get; private set; }

    public PlayerShooterAgent PlayerShooterAgent;

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        // Olayı tetikle
        OnGameStateChanged?.Invoke(CurrentState);

        switch (CurrentState)
        {
            case GameState.Day:
                break;
            case GameState.Night:
                break;
            case GameState.Cinematic:
                break;
            case GameState.BuyingBeforeNight:
                UIManager.Instance.OpenBeforeNightCanvas();
                break;
            case GameState.Start:
                ChangeState(GameState.Day);
                break;
        }

        Debug.Log("Game State changed to: " + CurrentState);
    }

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

    private void Start()
    {
        ChangeState(GameState.Start);
    }

}
