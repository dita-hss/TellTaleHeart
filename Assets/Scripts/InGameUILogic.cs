using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUILogic : MonoBehaviour
{
    public GameObject loseUI;

    public GameObject pauseUI;

    private bool _paused = false;


    public void OnPause()
    {
        _paused = true;
        Cursor.lockState = CursorLockMode.None;
        
        pauseUI?.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnUnpause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        _paused = false; 
        pauseUI?.SetActive(false);
    }


    public void TogglePause()
    {
        if (_paused)
        {
            OnUnpause();
        } else
        {
            OnPause();
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1; 
    }





    public void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }
    public void HideLoseUI()
    {
        loseUI.SetActive(false);
    }

}
