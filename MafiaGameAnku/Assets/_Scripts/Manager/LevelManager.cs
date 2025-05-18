using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        StartDay();

    }

    public void StartDay() //Call when lose the shooting
    {
        CurrentDayTime = 0;
        EarnedMoneyInDay = 0;
        IsDayStarted = true;
        GameManager.Instance.ChangeState(GameState.Day);
        Debug.Log(DayIndex.ToString() + " DAY STARTED");
        RayInputManager.Instance.cameras[DayIndex].GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;

        if (RayInputManager.Instance.cameras[DayIndex].transform.childCount > 0)
            RayInputManager.Instance.cameras[DayIndex].transform.GetChild(0).gameObject.SetActive(false);

        SwitchCameraMode(false);
       
    }

    void EndDay() //Get called when the time runs out
    {
        Debug.Log("DAY ENDED");
        IsDayStarted = false;
        GameManager.Instance.ChangeState(GameState.BuyingBeforeNight);
    }


    public void EarnMoney(int amount)
    {
        EarnedMoneyInDay += amount;
        UIManager.Instance.UpdateMoneyText(EarnedMoneyInDay);
    }

    public void StartShooting() 
    {
        Debug.Log("ShootingStarted");
        GameManager.Instance.ChangeState(GameState.Night);
        ShootingManager.Instance.SpawnShootingScene(DayIndex);
        SwitchCameraMode(true);
    }


    public void PlayerDied()
    {
        EndShootingGame();
        GameManager.Instance.ChangeState(GameState.Died);
    }

    public void EndShootingGame() 
    {
        ShootingManager.Instance.DestroyShootingScene();
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
