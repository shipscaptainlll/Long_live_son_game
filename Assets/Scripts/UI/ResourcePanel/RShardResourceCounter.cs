using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RShardResourceCounter : MonoBehaviour
{
    public float count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    public string Type
    {
        get
        {
            return "BoulderShard";
        }
    }

    public void Start()
    {
        count = 100000;
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(float e)
    {
        count += e;
        RefreshCounter(); 
    }

    public void SubtractFromCounter(int e)
    {
        count -= e;
        RefreshCounter();
    }

    public void AddToMineSpeedCounter(float e)
    {
        mineSpeedCount += e;
        float roundItUp = Mathf.Round(mineSpeedCount * 100.0f);
        float roundAgain = roundItUp * 0.01f;
        mineSpeedCount = roundAgain;
        //Debug.Log(mineSpeedCount);
        RefreshMineSpeedCounter();
    }

    void RefreshCounter()
    {
        countToText.text = count.ToString("0");
    }

    void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString("0.00");
    }
}
