using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [SerializeField] private AudioDataSO footstepSounds;
    [SerializeField] private float meterPerFootstep = 1.5f;
    [SerializeField] private SoundEmitter footstepEmitter;

    private Vector3 lastPos;
    private float curDistBuild = 0; 

    private void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float dist = (transform.position - lastPos).magnitude;

        curDistBuild += dist;

        if (curDistBuild > meterPerFootstep) {
            curDistBuild -= meterPerFootstep;
            SoundManager.Audio?.PlaySFXSound(footstepSounds, Vector3.zero, transform);
            footstepEmitter?.EmitSound();
        }

        lastPos = transform.position;
    }
}
