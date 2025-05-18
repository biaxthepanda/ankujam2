using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingEffects : MonoBehaviour
{
public ParticleSystem muzzleFlash;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && RayInputManager.Instance.IsMouseOverCamera(RayInputManager.Instance.cameras[3], Input.mousePosition))
        {
            SoundManager.Instance.PlaySFX(SoundEffects.Pistol);
            
            muzzleFlash.Play();
        }
    }
}
