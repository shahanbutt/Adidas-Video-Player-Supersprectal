using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChangeVideo : MonoBehaviour
{
    public int screenHeight;

    public VerticalLayoutGroup verticalLayoutGroup;
    public RectTransform rectTransform;

    public VideoPlayer videoPlayer;
    public VideoClip holdingVideo;
    public VideoClip contentVideo;

    private void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        
        Invoke("SetButtonHeight", 0.25f);
    }

    void SetButtonHeight()
    {
        screenHeight = (int)rectTransform.sizeDelta.y / 2;
        verticalLayoutGroup.padding.bottom = screenHeight;
        verticalLayoutGroup.enabled = false;
        Invoke("EnableVerticalLayout", 0.25f);
    }

    void EnableVerticalLayout()
    {
        verticalLayoutGroup.enabled = true;
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

        videoPlayer.loopPointReached += EndReached;
    }

    public void Button_ChangeToContentVideo()
    {
        Play_Content_Video(contentVideo);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        print(gameObject.name + " Content Video Ended");
        Play_Holding_Video(holdingVideo);
        videoPlayer.loopPointReached -= EndReached;
    }
}