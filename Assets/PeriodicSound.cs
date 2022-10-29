using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicSound : MonoBehaviour
{

    [SerializeField] private AudioDataSO soundToPlay;
    [SerializeField] float timeBetweenSound;
    private float _curtime; 

    // Update is called once per frame
    void Update()
    {
        _curtime += Time.deltaTime; 
        if (_curtime > timeBetweenSound)
        {
            _curtime -= timeBetweenSound;
            SoundManager.Audio.PlaySFXSound(soundToPlay, Vector3.zero, transform);
        }
    }
}
