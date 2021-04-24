using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip holdingVideo;
    public VideoClip contentVideo;

    public bool playContent = true;

    private void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();
    }

    public void Play_Holding_Video(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }

    public void Play_Content_Video(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();

        //videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        print(gameObject.name + " Content Video Ended");
        Play_Holding_Video(holdingVideo);
        videoPlayer.loopPointReached -= EndReached;
    }
}