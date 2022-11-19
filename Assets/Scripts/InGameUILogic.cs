using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUILogic : MonoBehaviour
{
    public GameObject loseUI;

    public GameObject pauseUI;

    public GameObject inGameUI;

    public GameObject winUI;

    public GameObject tutorialUI; 

    private bool _paused = false;


    public void OnPause()
    {
        _paused = true;
        Cursor.lockState = CursorLockMode.None;

        inGameUI.SetActive(false);
        
        pauseUI?.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnUnpause()
    {

        inGameUI.SetActive(true);
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


    public void ShowTutUI()
    {
        inGameUI.SetActive(false);
        tutorialUI.SetActive(true);
    }
    public void HideTutUI()
    {
        inGameUI.SetActive(true);
        tutorialUI.SetActive(false);
    }

    public void ShowWinUI()
    {
        inGameUI.SetActive(false);
        winUI.SetActive(true);
    }
    public void HideWinUI()
    {
        inGameUI.SetActive(true);
        winUI.SetActive(false);
    }


    public void ShowLoseUI()
    {
        inGameUI.SetActive(false);
        loseUI.SetActive(true);
    }
    public void HideLoseUI()
    {
        inGameUI.SetActive(true);
        loseUI.SetActive(false);
    }

}
