using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AgentProjectile : MonoBehaviour
{
    Rigidbody rb;

    public float ProjectileSpeed;
    public int DamageAmount;

    public bool isPlayerProjectile = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward*ProjectileSpeed);
        DestoryAfterSeconds();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void DestoryAfterSeconds() 
    {
        Invoke("DestroyObject",4f);
    }
    
    void DestroyObject() 
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hitActor = other.GetComponent<IDamageable>();
        if(hitActor != null)
        {
            Debug.Log("HIT SOMETHING");
            hitActor.GetDamage(DamageAmount);
            if (isPlayerProjectile)
            {
                GameManager.Instance.PlayerShooterAgent.PlayerHitSomething();
                Debug.Log("PLAYER HIT SOMETHING");
            }
        }
        if (!other.GetComponent<AgentProjectile>())
        {
            Destroy(gameObject);
        }
            
    }
}
