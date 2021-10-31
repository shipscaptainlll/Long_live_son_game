using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleResourceCounter : MonoBehaviour
{
    private int count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    public event Action ApplesCollected = delegate { };
    public string Type
    {
        get
        {
            return "Apple";
        }
    }

    public void Start()
    {
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(int e)
    {
        NotifyEleventhQuest();
        count += e;
        RefreshCounter(); 
    }

    void NotifyEleventhQuest()
    {
        if (ApplesCollected != null)
        {
            ApplesCollected();
        }
    }

    public void AddToMineSpeedCounter(float e)
    {
        mineSpeedCount += e;
        float roundItUp = Mathf.Round(mineSpeedCount * 10.0f);
        float roundAgain = roundItUp * 0.1f;
        mineSpeedCount = roundAgain;
        RefreshMineSpeedCounter();
    }

    void RefreshCounter()
    {
        float roundItUp = Mathf.Round(count * 10.0f);
        float roundAgain = roundItUp * 0.1f;
        countToText.text = roundAgain.ToString();
    }

    void RefreshMineSpeedCounter()
    {
        float roundItUp = Mathf.Round(mineSpeedCount * 10.0f);
        float roundAgain = roundItUp * 0.1f;
        Debug.Log(roundAgain);
        mineSpeedToText.text = roundAgain.ToString();
    }
}
