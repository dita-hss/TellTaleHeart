using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuStartAnimationLogic : MonoBehaviour
{

    public VideoPlayer onStartClickedVideoObject;
    public VideoPlayer loopingVideo;
    public Image fadeToWhite; 
    public float fadeToWhiteTime = 3.0f;
    public float isWhiteTime = 1.0f;

    private bool tryStartOpen = false; 

    public string gotoScene; 

    private float _curFadeToWhiteTime = -1.0f;



    private void Start()
    {
        loopingVideo.gameObject.SetActive(true);
        loopingVideo.isLooping = true; 
        onStartClickedVideoObject.gameObject.SetActive(false);
        //onStartClickedVideoObject.Prepare();
    }

    public void OnStartButtonClicked()
    {
        
        // https://forum.unity.com/threads/how-to-know-video-player-is-finished-playing-video.483935/ thx
        loopingVideo.loopPointReached += AfterClickVideoEnd;
        //onStartClickedVideoObject.prepareCompleted += AfterClickVideoEnd;
        
    }

    void AfterClickVideoEnd(VideoPlayer vp)
    {

        onStartClickedVideoObject.gameObject.SetActive(true);
        tryStartOpen = true;
        onStartClickedVideoObject.Prepare();
        onStartClickedVideoObject.loopPointReached += AfterOpenDoorAnimation;
        loopingVideo.loopPointReached -= AfterClickVideoEnd;
    }

    public void AfterOpenDoorAnimation(VideoPlayer vp)
    {
        _curFadeToWhiteTime = fadeToWhiteTime;
    }


    private void Update()
    {
        
        if (tryStartOpen)
        {
            if (onStartClickedVideoObject.isPrepared)
            {

                loopingVideo.gameObject.SetActive(false);
                onStartClickedVideoObject.frame = 0;
                onStartClickedVideoObject.isLooping = false;
                onStartClickedVideoObject.Play();
                tryStartOpen = false; 
            }
        }

        if (_curFadeToWhiteTime > 0.0f)
        {
            fadeToWhite.color = new Color(1, 1, 1, Mathf.Clamp((fadeToWhiteTime - _curFadeToWhiteTime) / isWhiteTime, 0, 1));
            _curFadeToWhiteTime -= Time.deltaTime;

            if (_curFadeToWhiteTime <= 0.0f)
            {
                SceneManager.LoadScene(gotoScene);
            }
        }
    }



}
