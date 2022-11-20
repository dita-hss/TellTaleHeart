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

    public static bool Paused { get; private set; } = false;

    public AudioDataSO onWinPlay;


    private void Start()
    {
        Paused = false; 
        ShowTutUI();
    }



    public void OnPause()
    {
        Paused = true;
        Cursor.lockState = CursorLockMode.None;

        inGameUI.SetActive(false);
        
        pauseUI?.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnUnpause()
    {

        Time.timeScale = 1;
        inGameUI.SetActive(true);
        HideTutUI();
        Cursor.lockState = CursorLockMode.Locked;
        Paused = false; 
        pauseUI?.SetActive(false);
    }


    public void TogglePause()
    {
        if (Paused)
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
        inGameUI?.SetActive(false);
        tutorialUI?.SetActive(true);

        Paused = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
    }
    public void HideTutUI()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Paused = false;
        inGameUI?.SetActive(true);
        tutorialUI?.SetActive(false);
    }

    public void ShowWinUI()
    {
        Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        SoundManager.Audio?.PlaySFXSound(onWinPlay, Vector3.zero);

        inGameUI?.SetActive(false);
        winUI.SetActive(true);
    }
    public void HideWinUI()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Paused = false;
        inGameUI.SetActive(true);
        winUI.SetActive(false);
    }


    public void ShowLoseUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        loseUI.SetActive(true);
        Paused = true;
    }
    public void HideLoseUI()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Paused = false;
        inGameUI.SetActive(true);
        loseUI.SetActive(false);
    }

}
