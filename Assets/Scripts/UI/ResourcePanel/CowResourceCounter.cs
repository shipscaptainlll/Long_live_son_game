using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowResourceCounter : MonoBehaviour
{
    public int count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;


    public string Type
    {
        get
        {
            return "Cow";
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
        float roundItUp = Mathf.Round(mineSpeedCount * 100f);
        float roundAgain = roundItUp * 0.01f;
        mineSpeedCount = roundAgain;
        RefreshMineSpeedCounter();
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
