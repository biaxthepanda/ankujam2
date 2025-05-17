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
    }
}
