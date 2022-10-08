using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SoundEmitter))]
public class PlaySoundOnClick : MonoBehaviour
{
    private SoundEmitter _emitter;
    private void Start()
    {
        _emitter = GetComponent<SoundEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _emitter.EmitSound();
        }
    }
}
