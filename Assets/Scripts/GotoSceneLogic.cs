using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoSceneLogic : MonoBehaviour
{
    public static void GotoNewScene(string newScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(newScene);
    }

    public static void RestartScene()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }
}
