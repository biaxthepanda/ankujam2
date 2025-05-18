using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class FallingItem : MonoBehaviour, IInteractable
{
    public BoxController targetBox; // Spawner tarafından atanmalı
    public char itemKey;

    private bool isHeld = false;
    private Rigidbody rb;
    private Vector3 lastVelocity;
    private Camera activeCamera;

    public float followSpeed = 1f;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // itemKey, BoxController’dan atanmalı (örn: Spawner tarafından)
        itemKey = targetBox.requiredKey;
    }

    public void Interacted()
    {
        isHeld = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        activeCamera = RayInputManager.Instance.CurrentCamera; // kendi sistemine göre düzenle
    }

    public void UnInteracted()
    {
        isHeld = false;
        if (rb == null) return;
        rb.useGravity = true;
        rb.velocity = lastVelocity;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Day) Destroy(gameObject);
        if (isHeld)
        {
            FollowMouseConstrainedToCameraPlane();

            if (Input.anyKeyDown && !MouseClicked())
            {
                char inputChar = char.ToUpper(Input.inputString[0]);

                if (inputChar == itemKey.ToString().ToUpper()[0])
                {
                    Debug.Log("Doğru tuşa basıldı");
                    SoundManager.Instance.PlaySFX(SoundEffects.Box);
                    targetBox.FulfillRequest(true); // success
                }
                else
                {
                    Debug.Log("Yanlış tuşa basıldı");
                    targetBox.FulfillRequest(false); // fail
                }

                Destroy(gameObject);
                isHeld = false;
            }
        }
    }

    private void FollowMouseConstrainedToCameraPlane()
    {
        if (activeCamera == null) return;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = activeCamera.ScreenPointToRay(mousePos);
        Plane movementPlane = new Plane(activeCamera.transform.forward, transform.position);

        if (movementPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPoint = ray.GetPoint(enter);
            Vector3 camForward = activeCamera.transform.forward.normalized;
            Vector3 toTarget = targetPoint - transform.position;
            Vector3 projectedMove = Vector3.ProjectOnPlane(toTarget, camForward);

            Vector3 move = projectedMove * followSpeed;
            rb.velocity = move;
            lastVelocity = rb.velocity;
        }
    }

    private bool MouseClicked()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }

    void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySFX(SoundEffects.BoxSurface);
    }
}
