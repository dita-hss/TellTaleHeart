using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOutputTester : MonoBehaviour
{

    public string toPrint = "Test"; 
    
    public void PrintTest()
    {
        Debug.Log(toPrint + " " + gameObject.name);
    }
}
