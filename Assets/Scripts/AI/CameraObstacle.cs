using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Eyes), typeof(SoundEmitter))]
public class CameraObstacle : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float alarmPauseTime = 0.8f;
    [SerializeField] private float stopAfterNotSeenTime = 1.5f;

    [Header("Audio")]
    [SerializeField] private AudioDataSO onSeePlayer;



    private Eyes cameraVision;
    private SoundEmitter alarmEmitter; 
    private GameObject curSeen;

    private PeriodicSound beepSound;


    private float timeSinceLastSeen = 0.0f;
    private bool alarmPlaying = false; 

    private void Awake()
    {
        alarmEmitter = gameObject.GetComponent<SoundEmitter>();
        cameraVision = gameObject.GetComponent<Eyes>();
        cameraVision.onSeeTarget.AddListener(OnSeenTarget);

        beepSound = GetComponent<PeriodicSound>();
    }

    /*private void OnValidate()
    {
        Light light = GetComponentInChildren<Light>();
        Eyes cameraVis = gameObject.GetComponent<Eyes>();

        if (light != null && cameraVis != null)
        {
            light.innerSpotAngle = cameraVis.ViewAngle;
            light.spotAngle = cameraVis.ViewAngle + 5.0f;
        }
    }*/




    private void OnSeenTarget(GameObject seen)
    {
        if (seen.tag.Equals(targetTag))
        {
            timeSinceLastSeen = 0.0f;
            curSeen = seen;
            if (!alarmPlaying)
            {
                print("start alarm");
                alarmPlaying = true;
                beepSound?.Disable();
                StartCoroutine(RepeatedAlarm());
            }
        }
    }


    private void PlaySingleAlarm()
    {
        alarmEmitter.EmitSound();
        SoundManager.Audio?.PlaySFXSound(onSeePlayer, transform.position, null);
        print("Sound played");
    }


    private IEnumerator RepeatedAlarm()
    {
        while (timeSinceLastSeen < stopAfterNotSeenTime || VisionSeesTarget())
        {
            PlaySingleAlarm();
            yield return new WaitForSeconds(alarmPauseTime);
        }
        alarmPlaying = false; 
        curSeen = null;

        beepSound?.Enable(); 
        yield return null; 
    }

    private bool VisionSeesTarget()
    {
        return cameraVision.GetSeenTargets().Contains(curSeen);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSeen += Time.deltaTime; 
    }
}
