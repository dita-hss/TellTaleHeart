using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EyeTester : MonoBehaviour
{
    public Eyes toTest; 
}



/*
// https://learn.unity.com/tutorial/editor-scripting#
[CustomEditor(typeof(EyeTester))]
public class EyeTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EyeTester myTarget = (EyeTester)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Check Targets"))
        {
            myTarget.toTest.CheckTargets();
        }
    }
}*/
