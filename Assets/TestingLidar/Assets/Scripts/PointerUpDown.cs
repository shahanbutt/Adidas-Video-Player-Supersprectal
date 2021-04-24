using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerUpDown : MonoBehaviour
{
    public VideoController videoController;

    public float timerVal;
    public float timerResetVal = 0.25f;

    public bool startTimer;
    public bool pUp, pDown = true;

    private void Awake()
    {
        if (videoController == null)
            videoController = GetComponent<VideoController>();

        EventTrigger trigger = GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) => { PointerDown(); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((eventData) => { PointerUp(); });
        trigger.triggers.Add(entry);
    }

    public void PointerDown()
    {
        startTimer = false;
        timerVal = timerResetVal;

        if (!pDown)
        {
            print("Pointer Down");
            pDown = true;
            pUp = false;

            videoController.playContent = false;
            videoController.Play_Holding_Video(videoController.holdingVideo);
        }
    }

    public void PointerUp()
    {
        startTimer = true;
    }

    public void TimerFinished()
    {
        timerVal = timerResetVal;
        if (pDown)
        {
            print("Pointer Up");
            pDown = false;
            pUp = true;

            videoController.playContent = true;
            videoController.Play_Content_Video(videoController.contentVideo);
        }
    }

    private void FixedUpdate()
    {
        if (startTimer)
        {
            timerVal = timerVal - Time.deltaTime;
            if (timerVal < 0)
            {
                TimerFinished();
            }
        }
    }
}
