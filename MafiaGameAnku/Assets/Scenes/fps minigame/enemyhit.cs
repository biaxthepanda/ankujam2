using UnityEngine;

public class enemyhit : MonoBehaviour
{
    public int damage;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                // else
                // {
                //     Debug.Log("Tıklanan obje: " + hit.collider.gameObject.name);
                //     Debug.Log("Tagi: " + hit.collider.gameObject.tag);
                // }
            }
        }
    }
}
