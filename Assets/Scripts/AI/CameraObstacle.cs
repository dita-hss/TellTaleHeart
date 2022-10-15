using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Eyes), typeof(SoundEmitter))]
public class CameraObstacle : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float alarmPauseTime = 0.8f;
    [SerializeField] private float stopAfterNotSeenTime = 1.5f; 



    private Eyes cameraVision;
    private SoundEmitter alarmEmitter; 
    private GameObject curSeen;


    private float timeSinceLastSeen = 0.0f;
    private bool alarmPlaying = false; 

    private void Awake()
    {
        alarmEmitter = gameObject.GetComponent<SoundEmitter>();
        cameraVision = gameObject.GetComponent<Eyes>();
        cameraVision.onSeeTarget.AddListener(OnSeenTarget);
    }


    private void OnSeenTarget(GameObject seen)
    {
        if (seen.tag.Equals(targetTag))
        {
            timeSinceLastSeen = 0.0f;
            curSeen = seen;
            if (!alarmPlaying)
            {
                alarmPlaying = true; 
                StartCoroutine(RepeatedAlarm());
            }
        }
    }


    private void PlaySingleAlarm()
    {
        alarmEmitter.EmitSound();
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
