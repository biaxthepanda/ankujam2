using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public bool isFriendly = false; // Eðer dostsa true, düþman ise false
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float fireRate = 1f; // Ne kadar sýk ateþ eder
    public int maxAmmo = 8; // Maksimum mermi sayýsý
    private int currentAmmo;
    public float reloadTime = 1.2f; // Reload süresi

    private bool isReloading = false;
    private float nextFireTime = 0f;
    private Transform target;

    public Transform gunTransform; // Silahýn bulunduðu transform (mermi atýþý için)
    public LayerMask targetLayer; // Hedefin bulunduðu layer (dost ve düþmanlar)

    public GameObject Projectile;

    public ParticleSystem MuzzleFlash;

    public float RandomMovementCooldown = 1f;
    private float RandomMovementTimeLeft = 1f;

    private int _currentRandomDirection;

    public Transform Root;
    public Rigidbody rb;
    private void Start()
    {
        currentAmmo = maxAmmo;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            Reload();
        }
        else
        {
            if (Time.time >= nextFireTime)
            {
                // Eðer hedef yoksa yeni bir hedef seç, varsa mevcut hedefe ateþ et
                if (target == null)
                {
                    FindTarget();
                }
                Fire();
            }
            RotateTowardsTarget();
            
        }
        RandomMovement();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_currentRandomDirection * moveSpeed, rb.velocity.y, rb.velocity.z);

    }

    // Rastgele hareket (saða ya da sola kayma)
    void RandomMovement()
    {
        if (RandomMovementTimeLeft < 0)
        { 
            RandomMovementTimeLeft = RandomMovementCooldown;
            _currentRandomDirection = Mathf.RoundToInt(Random.Range(-1f, 1f));
        
        }
        else 
        {
            RandomMovementTimeLeft -= Time.deltaTime;
        }


        //transform.Translate(Vector3.right * _currentRandomDirection * moveSpeed * Time.deltaTime);
        


    }

    // Hedef belirleme, sadece mevcut hedef yoksa
    void FindTarget()
    {
        // Eðer zaten bir hedef varsa, yeni bir hedef seçme
        if (target != null) return;

        Collider[] enemies;

        if (isFriendly)
        {
            enemies = Physics.OverlapSphere(transform.position, 20f, LayerMask.GetMask("Enemy"));
        }
        else
        {
            enemies = Physics.OverlapSphere(transform.position, 20f, LayerMask.GetMask("Friendly"));
        }

        if (enemies.Length > 0)
        {
            target = enemies[Random.Range(0, enemies.Length)].transform;
        }
    }

    // Hedefe doðru dönme (bazý durumlarda biraz yanýna bakma)
    void RotateTowardsTarget()
    {
        if (target == null) return;

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0; // Y eksenini sýfýrlýyoruz çünkü top-down bir oyun

        // Hedefin pozisyonunu doðru bir þekilde hesapla
        //float angle = Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(0, -angle, 0);
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        // Hedefe doðru dönme iþlemi
        Root.transform.rotation = Quaternion.Slerp(Root.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Ateþ etme
    void Fire()
    {
        if (currentAmmo <= 0) return;

        if (target != null)
        {
            nextFireTime = Time.time + fireRate;
            currentAmmo--;

            // Yön vektörüne rastgele bir açýyla sapma ekle
            float randomAngle = Random.Range(-25f, 25f); // -5 ile 5 derece arasý sapma
            Quaternion spreadRotation = Quaternion.Euler(0f, randomAngle, 0f); // Yalnýzca yatay düzlemde sapma
            Vector3 spreadDirection = spreadRotation * gunTransform.forward; // Sapmýþ yön
            Instantiate(Projectile,gunTransform.position,Root.transform.rotation);
            // Ray'i oluþtur ve çiz
            MuzzleFlash.Play();
        }
    }

    // Reload iþlemi
    void Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        Invoke("FullyReload", reloadTime);
    }

    void FullyReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload complete.");
    }
}