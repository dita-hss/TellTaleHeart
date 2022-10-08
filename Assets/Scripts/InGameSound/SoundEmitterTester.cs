using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundEmitterTester : MonoBehaviour
{
    public SoundEmitter toEmit;

    public void CallEmitter()
    {
        toEmit.EmitSound();
    }



}


// https://learn.unity.com/tutorial/editor-scripting#
[CustomEditor(typeof(SoundEmitterTester))]
public class SoundEmitterTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SoundEmitterTester myTarget = (SoundEmitterTester)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Emit Sound"))
        {
            myTarget.CallEmitter();
        }
    }
}