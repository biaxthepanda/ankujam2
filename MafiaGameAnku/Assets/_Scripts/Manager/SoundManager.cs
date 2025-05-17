using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffects
{
        Pistol,
        Shotgun,
}

public class SoundManager : MonoBehaviour
{


    public static SoundManager Instance { get; private set; }

    public AudioSource audSrc;

    public AudioClip[] SFXClips;

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
