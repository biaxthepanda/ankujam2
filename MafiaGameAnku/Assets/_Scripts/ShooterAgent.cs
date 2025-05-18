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
         Debug.Log("HIT");
        Health -= damageAmount;
        if(Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("DIED");
        Destroy(gameObject);
        ShootingManager.Instance.EnemyDied();
    }
}
