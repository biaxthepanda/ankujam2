using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseThrowable : MonoBehaviour, IInteractable
{
    protected Rigidbody rb;
    private bool isInteracted = false;
    private Vector3 lastVelocity;

    public float followSpeed = 100f;
    public float distanceFromCamera = 2f;

    private Camera activeCamera;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Interacted()
    {
        isInteracted = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        activeCamera = RayInputManager.Instance.CurrentCamera;
        Debug.Log("Interacted");
    }

    public virtual void UnInteracted()
    {
        isInteracted = false;
        rb.useGravity = true;
        rb.velocity = lastVelocity;
        Debug.Log("UnInteracted");
    }

    protected virtual void Update()
    {
        if (isInteracted && activeCamera != null)
        {
            FollowMouseConstrainedToCameraPlane();
        }
    }

    private void FollowMouseConstrainedToCameraPlane()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = activeCamera.ScreenPointToRay(mousePos);

        // Objenin bulunduðu noktada, kameranýn baktýðý yönle dik düzlem tanýmla
        Plane movementPlane = new Plane(activeCamera.transform.forward, transform.position);

        if (movementPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPoint = ray.GetPoint(enter);

            // Kamera yönündeki hareketi engelle
            Vector3 camForward = activeCamera.transform.forward.normalized;
            Vector3 toTarget = targetPoint - transform.position;
            Vector3 projectedMove = Vector3.ProjectOnPlane(toTarget, camForward);

            Vector3 move = projectedMove * followSpeed;

            rb.velocity = move;
            lastVelocity = rb.velocity;
        }
    }
}