using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffects
{
    Pistol,
    Box,
    BoxSurface,
    Footstep,
    Hit,
    Horn,
    Reload,
    Money,
    Right,
    Wrong,
    Ring

}

public class SoundManager : MonoBehaviour
{


    public static SoundManager Instance { get; private set; }

    public AudioSource audSrc;
    public AudioSource musicSrc;
    public AudioClip[] SFXClips;
    public AudioClip[] DayMusicClips;
    public AudioClip ActionClip;
   

    

     void OnEnable()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }


    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Day)
        {
            musicSrc.clip = DayMusicClips[LevelManager.Instance.DayIndex+1];
            musicSrc.Play();
        }
        else if (state == GameState.Night)
        {
            musicSrc.clip = ActionClip;
            musicSrc.Play();
        }
    }

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


    public void PlaySFX(SoundEffects soundEffects)
    {
        audSrc.PlayOneShot(SFXClips[(int)soundEffects]);

    }

 
}
