using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeatDisplay : MonoBehaviour
{

    public Image displayImage; 
    public Sprite defaultHeart;
    public Sprite[] heartBeatAnimation;
    private HeartBeat heartBeat;
    public AudioDataSO heartBeatSound;

    private int heartBeatIndex = 0;
    private bool playing = false;
    private float defaultAnimationSpeed = 0.5f;
    private float currentAnimationSpeed = 0.5f;
    private float curBeatTime = 0.0f; 
        

    // Start is called before the first frame update
    void Start()
    {
        heartBeat = FindObjectOfType<HeartBeat>();
        heartBeat.Beating.AddListener(OnBeat);
        displayImage.sprite = defaultHeart;
    }

    public void OnBeat()
    {
        currentAnimationSpeed = ((float) heartBeat.defaultHeartRate / (float) heartBeat.HeartRate) * defaultAnimationSpeed;

        playing = true; 
        SoundManager.Audio?.PlaySFXSound(heartBeatSound, heartBeat.transform.position);
    }

    public void PlayBeatAnimation()
    {
        heartBeatIndex = 0; 
    }

    private void Update()
    {
        if (playing)
        {
            curBeatTime += Time.deltaTime;

            if (curBeatTime > currentAnimationSpeed / heartBeatAnimation.Length)
            {
                
                heartBeatIndex++;
                curBeatTime = 0; 

                if (heartBeatIndex >= heartBeatAnimation.Length)
                {
                    heartBeatIndex = 0;
                    playing = false; 
                }
                else
                {
                    displayImage.sprite = heartBeatAnimation[heartBeatIndex];
                }
            }
        }
    }


}
