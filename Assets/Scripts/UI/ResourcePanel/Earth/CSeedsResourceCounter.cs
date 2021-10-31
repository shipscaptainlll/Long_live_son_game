using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSeedsResourceCounter : MonoBehaviour
{
    public float count;
    public float mineSpeedCount;
    public Text countToText;
    public Text mineSpeedToText;
    public CargoResourceCounter cargoResourceCounter;

    //Event that notifies eighteenth quests that some carrot seeds gathered
    public event Action cSeedsCollected = delegate { };
    public string Type
    {
        get
        {
            return "CarrotSeeds";
        }
    }

    public void Start()
    {
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(float e)
    {
        if (cSeedsCollected != null)
        {
            cSeedsCollected();
        }
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
        mineSpeedToText.text = roundAgain.ToString();
        SyncWithCargoSpeedCounter();
    }
    public void SyncWithCargoSpeedCounter()
    {
        cargoResourceCounter.mineSpeedCount = mineSpeedCount;
        cargoResourceCounter.RefreshMineSpeedCounter();
    }
}
