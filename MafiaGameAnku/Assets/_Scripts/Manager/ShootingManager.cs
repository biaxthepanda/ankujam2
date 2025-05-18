using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public static ShootingManager Instance { get; private set; }

    public GameObject FriendlyAgentPrefab;

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

    private int currentFriendlyAmount;

    public Transform ShootingSceneSpawnLoc;
    public void SpawnShootingScene(int idx)
    {
        CurrentShootingScene = Instantiate(ShootingScenes[idx], ShootingSceneSpawnLoc.transform.position, Quaternion.identity);
        GameManager.Instance.PlayerShooterAgent.ResetPlayer();
        currentFriendlyAmount = LevelManager.Instance.EarnedMoneyInDay / 100;
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("FriendlySpawn");
        for (int i = 0; i < spawns.Length; i++)
        {
            if (i < currentFriendlyAmount)
            {
                Debug.Log("Friendly Spawned");
                GameObject friendly = Instantiate(FriendlyAgentPrefab, spawns[i].transform.position, Quaternion.identity);
                //friendly.transform.SetParent(spawns[i].transform);
            }
        }
        LevelManager.Instance.EarnedMoneyInDay = 0;
    }

    public void DestroyShootingScene()
    {
        Destroy(CurrentShootingScene.gameObject);
        GameObject[] agents = GameObject.FindGameObjectsWithTag("FriendlyAgent");
        foreach (GameObject agent in agents)
        {
            Destroy(agent);
        }

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
            GameManager.Instance.ChangeState(GameState.NextDayScreen);
        }
    }

}
