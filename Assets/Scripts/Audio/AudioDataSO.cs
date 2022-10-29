using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Audio/Audio Data")]
public class AudioDataSO : ScriptableObject
{
    public AudioCueSO clipData;
    public bool is3D;

    [Range(0, 1)]
    public float volume = 1.0f;
    [Range(0, 2)]
    public float pitch = 1.0f;

    public float minDist = 1.0f;
    public float maxDist = 500.0f;
}
