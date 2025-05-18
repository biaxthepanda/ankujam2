using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterAgent : ShooterAgent
{
    public void PlayerHitSomething()
    {
        SoundManager.Instance.PlaySFX(SoundEffects.Hit);
    }
    public override void Die()
    {
        Debug.LogWarning("Player Died");
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
