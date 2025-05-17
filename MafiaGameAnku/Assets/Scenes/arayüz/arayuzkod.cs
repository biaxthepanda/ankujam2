using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class arayuzkod : MonoBehaviour
{
    public void stargame()
    {
        SceneManager.LoadScene("Taha");
    }
    public void exitgame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

