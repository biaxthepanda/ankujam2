using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    public bool isReal;
    public int moneyAmount;

    public virtual void Interacted()
    {
        if (isReal)
        {
            LevelManager.Instance.EarnMoney(moneyAmount);
            Destroy(this.gameObject);
        }
        else
        {
            LevelManager.Instance.EarnMoney(-moneyAmount);
            Destroy(this.gameObject);
        }
    }

    public virtual void UnInteracted()
    {
       
    }

    
}
