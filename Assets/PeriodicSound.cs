using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicSound : MonoBehaviour
{

    [SerializeField] private AudioDataSO soundToPlay;
    [SerializeField] private float timeBetweenSound = 1.0f;
    [SerializeField] private float timeBetweenSoundOffset = 0.0f; 
    private float _timeBeforeNextSound = 0.0f;

    private void Start()
    {
        _timeBeforeNextSound = timeBetweenSound + Random.Range(-timeBetweenSoundOffset, timeBetweenSoundOffset);
    }

    // Update is called once per frame
    void Update()
    {
        _timeBeforeNextSound -= Time.deltaTime; 
        if (_timeBeforeNextSound <= 0)
        {
            _timeBeforeNextSound += timeBetweenSound + Random.Range(-timeBetweenSoundOffset, timeBetweenSoundOffset);
            SoundManager.Audio.PlaySFXSound(soundToPlay, Vector3.zero, transform);
        }
    }

    public void Enable()
    {
        enabled = true; 
    }

    public void Disable()
    {
        enabled = false;
    }
}

