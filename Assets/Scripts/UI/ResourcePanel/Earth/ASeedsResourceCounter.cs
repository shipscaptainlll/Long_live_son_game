using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ASeedsResourceCounter : MonoBehaviour
{
    public float count;
    public float mineSpeedCount;
    public Text countToText;
    public Text mineSpeedToText;
    public CargoResourceCounter cargoResourceCounter;

    public event Action AppleSeedsCollected = delegate { };
    public string Type
    {
        get
        {
            return "AppleSeeds";
        }
    }

    public void Start()
    {
        count = 0;
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(float e)
    {
        count += e;
        NotifyThatResourceCollected();
        RefreshCounter(); 
    }

    private void NotifyThatResourceCollected()
    {
        if (AppleSeedsCollected != null)
        {
            AppleSeedsCollected();
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
        SyncWithCargoCounter();
    }
    public void SyncWithCargoCounter()
    {
        cargoResourceCounter.count = count;
        cargoResourceCounter.RefreshCounter();
    }
    void RefreshMineSpeedCounter()
    {
        float roundItUp = Mathf.Round(mineSpeedCount * 10.0f);
        float roundAgain = roundItUp * 0.1f;
        Debug.Log(roundAgain);
        mineSpeedToText.text = roundAgain.ToString();
        SyncWithCargoSpeedCounter();
    }
    public void SyncWithCargoSpeedCounter()
    {
        cargoResourceCounter.mineSpeedCount = mineSpeedCount;
        cargoResourceCounter.RefreshMineSpeedCounter();
    }
}
