using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInputManager : MonoBehaviour
{
    public static RayInputManager Instance { get; private set; }

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


    public Camera[] cameras; // 4 kamerayý inspector'dan atayýn
    public Camera CurrentCamera;

    private IInteractable _currentInteractable = null;

    public float rayLength = 100f;
    public Color rayColor = Color.red;

    private void Start()
    {
        CurrentCamera = cameras[0];
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Day) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;

            foreach (Camera cam in cameras)
            {
                if (IsMouseOverCamera(cam, mousePos))
                {
                    if (CurrentCamera != cam) { CurrentCamera = cam; }

                    Ray ray = cam.ScreenPointToRay(mousePos);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, rayLength))
                    {
                        Debug.Log($"Hit from camera '{cam.name}' on: {hit.collider.name}");
                        _currentInteractable = hit.transform.gameObject.GetComponent<IInteractable>();
                        _currentInteractable.Interacted();
                        Debug.DrawLine(ray.origin, hit.point, rayColor, 2f); // 2 saniye çizsin
                    }
                    else
                    {
                        // Eðer bir yere çarpmadýysa düz çizgi çiz
                        Vector3 endPoint = ray.origin + ray.direction * rayLength;
                        Debug.DrawLine(ray.origin, endPoint, Color.yellow, 2f);
                        Debug.Log($"No hit from camera '{cam.name}'");
                    }

                    break; // sadece bir kamera ilgilenecek
                }
            }
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if(_currentInteractable != null)
            {
                _currentInteractable.UnInteracted();
                _currentInteractable = null;
            }
        }
    }

    bool IsMouseOverCamera(Camera cam, Vector2 mousePos)
    {
        Rect viewportRect = cam.rect;

        Vector2 viewportPoint = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);

        return viewportRect.Contains(viewportPoint);
    }
}
