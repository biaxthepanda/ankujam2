using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public static ShootingManager Instance { get; private set; }

    public Camera ShootingCamera;

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

    public Transform SceneSpawnLoc;
    public void SpawnShootingScene(int idx) 
    {
        Instantiate(ShootingScenes[idx],SceneSpawnLoc.transform);
    }

    public void DestroyShootingScene() 
    {
        Destroy(CurrentShootingScene.gameObject);
        CurrentShootingScene = null;
    }

}
