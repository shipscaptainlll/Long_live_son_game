using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MBagResourceCounter : MonoBehaviour
{
    public int count;
    private float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;
    public CargoResourceCounter cargoResourceCounter;

    public event Action<int> ManureBagsCountChanged = delegate { };
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
        if ( ManureBagsCountChanged != null)
        {
            ManureBagsCountChanged(count);
        }
    }

    public void AddToMineSpeedCounter(float e)
    {
        mineSpeedCount += e;
        RefreshMineSpeedCounter();
    }

    void RefreshCounter()
    {
        countToText.text = count.ToString();
        SyncWithCargoCounter();
    }
    public void SyncWithCargoCounter()
    {
        cargoResourceCounter.count = count;
        cargoResourceCounter.RefreshCounter();
    }
    void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString();
        SyncWithCargoSpeedCounter();
    }
    public void SyncWithCargoSpeedCounter()
    {
        cargoResourceCounter.mineSpeedCount = mineSpeedCount;
        cargoResourceCounter.RefreshMineSpeedCounter();
    }
}
