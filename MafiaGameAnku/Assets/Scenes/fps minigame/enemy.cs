using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    public int health = 100;

    public float EnemySpeed;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy can: " + health);

        if (health <= 0)
        {
            Die();
            
        }
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * EnemySpeed;
    }

    void Die()
    {
        Debug.Log("Enemy öldü.");
        LevelManager.Instance.EarnMoney(10);
        Destroy(gameObject);
    }


    public virtual void Interacted()
    {
        Debug.Log("Enemy interacted");
        TakeDamage(10);
    }
    
    public virtual void UnInteracted()
    {
        
    }
}
