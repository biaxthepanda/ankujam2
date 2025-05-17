using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 45f; // günün süresi

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

        }
        else
        {           
            SceneManager.LoadScene("MainMenu");
        }
    }
}
