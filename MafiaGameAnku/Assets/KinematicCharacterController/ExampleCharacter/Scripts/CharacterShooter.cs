using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform gunTransform;

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
        if (Input.GetMouseButtonDown(0)) // Sol týk
        {
            Ray ray = new Ray(gunTransform.position, gunTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200))
            {
                Debug.Log("Vurulan nesne: " + hit.collider.name);
                Debug.DrawLine(gunTransform.position, hit.point, Color.red, 1f); // Kýrmýzý çizgi: isabet
            }
            else
            {
                // Ýsabet yoksa ileriye sabit uzunlukta çiz
                Vector3 endPoint = gunTransform.position + gunTransform.forward * 200;
                Debug.DrawLine(gunTransform.position, endPoint, Color.yellow, 1f); // Sarý çizgi: ýskalama
            }
        }
    }

}
