using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUILogic : MonoBehaviour
{
    public GameObject loseUI;


    public void ShowLoseUI()
    {
        loseUI.SetActive(true);
    }
    public void HideLoseUI()
    {
        loseUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
