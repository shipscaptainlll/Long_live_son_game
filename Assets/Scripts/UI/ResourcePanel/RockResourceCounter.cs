using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockResourceCounter : MonoBehaviour
{
    public float count;
    public float mineSpeedCount;
    private Text countToText;
    private Text mineSpeedToText;

    //Cargo panel
    public CargoResourceCounter cargoResourceCounter;
    public event Action<float> rocksMined;

    public event Action rocksSold = delegate { }; //Send notification that character sold some stones to the twelfth quest
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
    

    public void AddToCounter(float e)
    {
        if (rocksSold != null && e < 0)
        {
            rocksSold();
        }
        count += e;
        RefreshCounter();
        if (rocksMined != null)
        {
            rocksMined(e);
        }
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
        countToText.text = count.ToString("0");
        SyncWithCargoCounter();
    }

    public void SyncWithCargoCounter()
    {
        cargoResourceCounter.count = count;
        cargoResourceCounter.RefreshCounter();
    }

    public void RefreshMineSpeedCounter()
    {
        mineSpeedToText.text = mineSpeedCount.ToString("0.00");
        SyncWithCargoSpeedCounter();
    }

    public void SyncWithCargoSpeedCounter()
    {
        cargoResourceCounter.mineSpeedCount = mineSpeedCount;
        cargoResourceCounter.RefreshMineSpeedCounter();
    }
}
