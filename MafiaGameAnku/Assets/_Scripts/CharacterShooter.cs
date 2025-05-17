using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform gunTransform;
    public int DamageAmount = 10;
    public ParticleSystem MuzzleFlash;

    public int AmmoInChamber = 8;
    public float ReloadTime = 1.2f;
    public bool isReloading = false;

    public AgentProjectile Projectile;

    void Update()
    {
        RotatePlayer();
        MovePlayer();
        HandleShooting();
    }

    void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction = (hit.point - transform.position);
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A-D / Sol-Sað
        float vertical = Input.GetAxisRaw("Vertical");     // W-S / Ýleri-Geri

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void HandleShooting()
    {
        if (isReloading) return;

        if (Input.GetMouseButtonDown(0)) // Sol týk
        {
            Ray ray = new Ray(gunTransform.position, gunTransform.forward);
            RaycastHit hit;
            MuzzleFlash.Play();
            AmmoInChamber--;

            //if (Physics.Raycast(ray, out hit, 200))
            //{
            //    Debug.Log("Vurulan nesne: " + hit.collider.name);
            //    IDamageable agent = hit.transform.GetComponent<IDamageable>();
            //    if (agent != null) 
            //    {
            //        agent.GetDamage(DamageAmount);
            //    }
            //    Debug.DrawLine(gunTransform.position, hit.point, Color.red, 1f); // Kýrmýzý çizgi: isabet
            //}
            //else
            //{
            //    // Ýsabet yoksa ileriye sabit uzunlukta çiz
            //    Vector3 endPoint = gunTransform.position + gunTransform.forward * 200;
            //    Debug.DrawLine(gunTransform.position, endPoint, Color.yellow, 1f); // Sarý çizgi: ýskalama
            //}
            AgentProjectile projectile = Instantiate(Projectile,gunTransform.position,transform.rotation);
            projectile.isPlayerProjectile = true; //For hit notify



            Debug.Log(AmmoInChamber.ToString());
            if(AmmoInChamber <= 0) 
            {
                Reload();
            }
        }
    }

    public void Reload() 
    {
        isReloading = true;
        Invoke("FullyReload",ReloadTime);
    }
    private void FullyReload() 
    {
        AmmoInChamber = 8;
        isReloading = false;
    }
}
