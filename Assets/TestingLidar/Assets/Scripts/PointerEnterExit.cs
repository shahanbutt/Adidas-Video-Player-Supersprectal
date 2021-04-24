using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEnterExit : MonoBehaviour
{
    public VideoController videoController;

    public float timerVal;
    public float timerResetVal = 0.25f;

    public bool startTimer;
    public bool pEnter, pExit = true;

    private void Awake()
    {
        if (videoController == null)
            videoController = GetComponent<VideoController>();

        EventTrigger trigger = GetComponentInChildren<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { PointerEnter(); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((eventData) => { PointerExit(); });
        trigger.triggers.Add(entry);
    }

    public void PointerEnter()
    {
        startTimer = false;
        timerVal = timerResetVal;

        if (!pEnter)
        {
            print("Pointer Enter");
            pEnter = true;
            pExit = false;

            videoController.playContent = false;
            videoController.Play_Holding_Video(videoController.holdingVideo);
        }
    }

    public void PointerExit()
    {
        startTimer = true;
    }

    public void TimerFinished()
    {
        timerVal = timerResetVal;
        if (pEnter)
        {
            print("Pointer Exit");
            pEnter = false;
            pExit = true;

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