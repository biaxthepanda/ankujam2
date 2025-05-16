using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

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



    public void StartNextDay() 
    {
        DayIndex++;
        CurrentDayTime = 0;
        EarnedMoneyInDay = 0;
        IsDayStarted = true;
    }

    void EndDay() 
    {
        IsDayStarted = false;
    }


    public void EarnMoney(int amount)
    {
        EarnedMoneyInDay += amount;
        if(EarnedMoneyInDay >= DayQuota[DayIndex]) 
        {
            ReachedQuota();    
        }
    }

    void ReachedQuota() 
    {
        
    }


}
