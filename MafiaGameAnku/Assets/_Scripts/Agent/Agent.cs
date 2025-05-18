using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public bool isFriendly = false; // E�er dostsa true, d��man ise false
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float fireRate = 1f; // Ne kadar s�k ate� eder
    public int maxAmmo = 8; // Maksimum mermi say�s�
    private int currentAmmo;
    public float reloadTime = 1.2f; // Reload s�resi

    private bool isReloading = false;
    private float nextFireTime = 0f;
    private Transform target;

    public Transform gunTransform; // Silah�n bulundu�u transform (mermi at��� i�in)
    public LayerMask targetLayer; // Hedefin bulundu�u layer (dost ve d��manlar)

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
                // E�er hedef yoksa yeni bir hedef se�, varsa mevcut hedefe ate� et
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

    // Rastgele hareket (sa�a ya da sola kayma)
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
        // E�er zaten bir hedef varsa, yeni bir hedef se�me
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

    // Hedefe do�ru d�nme (baz� durumlarda biraz yan�na bakma)
    void RotateTowardsTarget()
    {
        if (target == null) return;

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0; // Y eksenini s�f�rl�yoruz ��nk� top-down bir oyun

        // Hedefin pozisyonunu do�ru bir �ekilde hesapla
        //float angle = Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(0, -angle, 0);
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        // Hedefe do�ru d�nme i�lemi
        Root.transform.rotation = Quaternion.Slerp(Root.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Ate� etme
    void Fire()
    {
        if (currentAmmo <= 0) return;

        if (target != null)
        {
            nextFireTime = Time.time + fireRate;
            currentAmmo--;
             SoundManager.Instance.PlaySFX(SoundEffects.Pistol); 
            // Y�n vekt�r�ne rastgele bir a��yla sapma ekle
            float randomAngle = Random.Range(-25f, 25f); // -5 ile 5 derece aras� sapma
            Quaternion spreadRotation = Quaternion.Euler(0f, randomAngle, 0f); // Yaln�zca yatay d�zlemde sapma
            Vector3 spreadDirection = spreadRotation * gunTransform.forward; // Sapm�� y�n
            Instantiate(Projectile,gunTransform.position,Root.transform.rotation);
            // Ray'i olu�tur ve �iz
            MuzzleFlash.Play();
        }
    }

    // Reload i�lemi
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