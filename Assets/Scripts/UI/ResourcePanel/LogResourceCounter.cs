using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogResourceCounter : MonoBehaviour
{
    public float count;
    public float mineSpeedCount;
    public Text countToText;
    public Text mineSpeedToText;
    public CargoResourceCounter cargoResourceCounter;

    public event Action<int> LogCollected = delegate { };
    public string Type
    {
        get
        {
            return "Log";
        }
    }

    public void Start()
    {
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
        mineSpeedToText = transform.Find("CounterPerMinute").Find("CounterText").GetComponent<Text>();
    }
    

    public void AddToCounter(float e)
    {
        count += e;
        RefreshCounter();
        NotifyThatLogCollected((int)e);
    }

    public void AddToMineSpeedCounter(float e)
    {
        mineSpeedCount += e;
        RefreshMineSpeedCounter();
    }

    void RefreshCounter()
    {
        countToText.text = count.ToString("0");
        SyncWithCargoCounter(); 
    }
    public void SyncWithCargoCounter()
    {
        cargoResourceCounter.count = count;
        cargoResourceCounter.RefreshCounter();
    }

    void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString("0.0");
        SyncWithCargoSpeedCounter();
    }

    public void SyncWithCargoSpeedCounter()
    {
        cargoResourceCounter.mineSpeedCount = mineSpeedCount;
        cargoResourceCounter.RefreshMineSpeedCounter();
    }

    void NotifyThatLogCollected(int LoggsAmmount)
    {
        if (LogCollected != null)
        {
            LogCollected(LoggsAmmount);
        }
    }
}
