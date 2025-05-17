using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Camera ShootingCamera;

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
        StartNextDay();
    }


    void Update()
    {
        if (IsDayStarted) 
        {
            CurrentDayTime += Time.deltaTime;
            if(CurrentDayTime >= DayTime[DayIndex])
            {
                EndDay();
            }
            
        }
    }


    public bool IsDayStarted;
    private float CurrentDayTime;


    public int DayIndex = -1;
    public float[] DayTime;

    public int EarnedMoneyInDay;
    public int[] DayQuota;



    public void StartNextDay() //Call when win the shooting
    {
        DayIndex++;
        CurrentDayTime = 0;
        EarnedMoneyInDay = 0;
        IsDayStarted = true;
        GameManager.Instance.ChangeState(GameState.Day);
    }

    void EndDay() 
    {
        Debug.Log("DAY ENDED");
        IsDayStarted = false;
        GameManager.Instance.ChangeState(GameState.BuyingBeforeNight);
    }


    public void EarnMoney(int amount)
    {
        EarnedMoneyInDay += amount;
        if(EarnedMoneyInDay >= DayQuota[DayIndex]) 
        {
            ReachedQuota();    
        }
    }

    public void StartShooting() 
    {
        Debug.Log("ShootingStarted");
        GameManager.Instance.ChangeState(GameState.Night);
        SwitchCameraMode(true);
    }

    void ReachedQuota() 
    {
        
    }

    public void PlayerDied() 
    {
        
    }

    public void SwitchCameraMode(bool isShottingCamera) 
    {
        if (isShottingCamera)
        {
            foreach (var cam in RayInputManager.Instance.cameras)
            {
                cam.enabled= false;
            }
            ShootingCamera.enabled = true;
            
        }
        else
        {
            foreach (var cam in RayInputManager.Instance.cameras)
            {
                cam.enabled = true;
            }
            ShootingCamera.enabled = false;
        }

    
    }

}
