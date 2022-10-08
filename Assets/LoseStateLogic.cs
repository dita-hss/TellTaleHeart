using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseStateLogic : MonoBehaviour
{
    private InGameUILogic ui;

    private void Start()
    {
        ui = GetComponent<InGameUILogic>();
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        ui.ShowLoseUI();

    }

}
