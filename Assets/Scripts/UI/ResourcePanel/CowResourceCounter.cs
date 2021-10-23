using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowResourceCounter : MonoBehaviour
{
    public CowMainResourceCounter cowMainResourceCounter; //Main cow resource controller, sending notification about new cow bought
    public float count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    public event Action cowBought = delegate { }; //Send notification that character bought cow to thirteenth quest
    public string Type
    {
        get
        {
            return "Cow";
        }
    }

    public void Start()
    {
        cowMainResourceCounter.cowBought += AddToCounter;
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(float e)
    {
        if (cowBought != null)
        {
            cowBought();
        }
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
