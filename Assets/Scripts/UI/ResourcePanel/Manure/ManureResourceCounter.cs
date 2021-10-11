using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManureResourceCounter : MonoBehaviour
{
    private int count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    public string Type
    {
        get
        {
            return "Water";
        }
    }

    public void Start()
    {
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(int e)
    {
        count += e;
        RefreshCounter(); 
    }

    public void AddToMineSpeedCounter(float e)
    {
        mineSpeedCount += e;
        RefreshMineSpeedCounter();
    }

    void RefreshCounter()
    {
        countToText.text = count.ToString();
    }

    void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString();
    }
}
