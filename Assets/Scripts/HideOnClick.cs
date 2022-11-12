using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnClick : MonoBehaviour
{
    public GameObject[] toHide; 
    public void HideAll()
    {
        foreach (GameObject ob in toHide)
        {
            ob.SetActive(false);
        }
    }
}
