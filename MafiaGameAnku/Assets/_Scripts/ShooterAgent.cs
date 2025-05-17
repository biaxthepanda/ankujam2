using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShooterAgent : MonoBehaviour, IDamageable
{
    public int InitialHealth = 100;
    public int Health;

    private void Start()
    {
        Health = InitialHealth;
    }

    public void GetDamage(int damageAmount)
    {
        throw new System.NotImplementedException();
    }
}
