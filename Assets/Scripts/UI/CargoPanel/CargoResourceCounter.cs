using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoResourceCounter : MonoBehaviour
{
    public int count;
    public float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    public string Type
    {
        get
        {
            return "CargoResource";
        }
    }

    public void Start()
    {
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }

    public void RefreshCounter()
    {        
        countToText.text = count.ToString();
    }

    public void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString("0.00");
    }
}
