using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockResourceCounter : MonoBehaviour
{
    public int count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    //Cargo panel
    public CargoResourceCounter cargoResourceCounter;

    public string Type
    {
        get
        {
            return "RockShard";
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

    void RefreshCounter()
    {        
        countToText.text = count.ToString();
        SyncWithCargoCounter();
    }

    void SyncWithCargoCounter()
    {
        cargoResourceCounter.count = count;
        cargoResourceCounter.RefreshCounter();
    }

    void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString("0.00");
        SyncWithCargoSpeedCounter();
    }

    void SyncWithCargoSpeedCounter()
    {
        cargoResourceCounter.mineSpeedCount = mineSpeedCount;
        cargoResourceCounter.RefreshMineSpeedCounter();
    }
}
