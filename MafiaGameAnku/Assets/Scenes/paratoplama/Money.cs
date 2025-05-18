using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    public bool isReal;
    public int moneyAmount;

    public float[] TimeToDestroyPerDay;

    

    void Start()
    {
        Invoke("DestroyObject", TimeToDestroyPerDay[LevelManager.Instance.DayIndex]);
    }

    public virtual void Interacted()
    {
        if (isReal)
        {
            LevelManager.Instance.EarnMoney(moneyAmount);
            SoundManager.Instance.PlaySFX(SoundEffects.Money);
            Destroy(this.gameObject);
        }
        else
        {
            LevelManager.Instance.EarnMoney(-moneyAmount);
            SoundManager.Instance.PlaySFX(SoundEffects.Wrong);
            Destroy(this.gameObject);
        }
    }

    public virtual void UnInteracted()
    {

    }
    
    void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    
}
