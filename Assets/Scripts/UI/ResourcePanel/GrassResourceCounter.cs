using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassResourceCounter : MonoBehaviour
{
    public int count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    public string Type
    {
        get
        {
            return "Grass";
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
