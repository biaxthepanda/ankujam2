using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public static ShootingManager Instance { get; private set; }


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

    public GameObject CurrentShootingScene;
    public GameObject[] ShootingScenes;

    private int _currentDeadEnemy;

    public int[] EnemyAmountPerNight;

    public Transform ShootingSceneSpawnLoc;
    public void SpawnShootingScene(int idx)
    {
        CurrentShootingScene = Instantiate(ShootingScenes[idx], ShootingSceneSpawnLoc.transform.position, Quaternion.identity);
        GameManager.Instance.PlayerShooterAgent.ResetPlayer();
    }

    public void DestroyShootingScene()
    {
        Destroy(CurrentShootingScene.gameObject);
        CurrentShootingScene = null;
    }

    public void EnemyDied()
    {
        _currentDeadEnemy++;
        if (_currentDeadEnemy >= EnemyAmountPerNight[LevelManager.Instance.DayIndex])
        {
            Debug.LogWarning("All enemies are dead");
            _currentDeadEnemy = 0;
            DestroyShootingScene();
            GameManager.Instance.ChangeState(GameState.Cinematic);
        }
    }

}
