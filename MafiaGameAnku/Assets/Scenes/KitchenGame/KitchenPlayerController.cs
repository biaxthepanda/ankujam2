
// }
using UnityEngine;

public class KitchenPlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 target;
    private bool isMoving = false;
    public GameObject carriedFood;



    void Update()
    {
        if(LevelManager.Instance.DayIndex < 2) return;
        if (Input.GetMouseButtonDown(0) && RayInputManager.Instance.IsMouseOverCamera(RayInputManager.Instance.cameras[2], Input.mousePosition))
        {

            Ray ray = RayInputManager.Instance.cameras[2].ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = new Vector3(hit.point.x, transform.position.y, hit.point.z); // Y sabit
                isMoving = true;
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.1f)
                isMoving = false;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + "ALINDI " + carriedFood.ToString());
        // 🍗 Yemek al
        if (other.CompareTag("Food") && carriedFood == null)
        {
            Debug.Log("Yemek alındı2");
            carriedFood = other.gameObject;
            carriedFood.transform.SetParent(transform);
            carriedFood.transform.localPosition = new Vector3(0, 1f, 0);
            carriedFood.GetComponent<Collider>().enabled = false;
        }

        // 🍽 Masaya yemek ver
        if (other.CompareTag("Table") && carriedFood != null)
        {
            Table table = other.GetComponent<Table>();
            if (table != null && table.isWaiting)
            {
                int foodType = carriedFood.GetComponent<Food>().foodType;
                table.TryDeliverFood(foodType);
                LevelManager.Instance.EarnMoney(10);
                Destroy(carriedFood.gameObject);
                carriedFood = null;
            }
        }
    }
}
