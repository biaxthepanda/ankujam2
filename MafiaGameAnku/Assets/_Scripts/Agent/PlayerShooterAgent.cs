using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterAgent : ShooterAgent
{
    public void PlayerHitSomething()
    {

    }
    public override void Die()
    {
        LevelManager.Instance.PlayerDied();
        this.gameObject.SetActive(false);
    }

    public void ResetPlayer()
    {
        Health = InitialHealth;
        transform.rotation = Quaternion.identity;
        this.gameObject.SetActive(true);
    }
}
