using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FUPCows : MonoBehaviour
{
    public Button upgradeButton;
    public CowFeedController cowFeedController1; //Connect first pasture feed controller
    //public CowFeedController cowFeedController2;
    //public CowFeedController cowFeedController3;
    public Text toolLvlCounterNow;
    public Text toolLvlCounterNext;
    public Text toolUpgradeCost;
    public CowMainResourceCounter cowMainResourceCounter;
    public int toolLevel;
    public int upgradeCost;

    public event Action<CowFeedController> cowWasPlacedForController = delegate { }; //Event notifies main cows controller about new cow to feed etc.
    public event Action cowWasPlacedAtFarm = delegate { }; //Event notifies fourteenth script about placing a cow at farm
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(upgradeElement);
        toolLevel = 0;
        upgradeCost = 1;
    }

    void upgradeElement()
    {
        if (upgradeCost <= cowMainResourceCounter.count)
        {
            subtractCostFromMainCounter();
            addToCounter();
            addToUpgradeCost();
        }
    }

    void addToCounter()
    {
        if (cowWasPlacedForController != null)
        {
            cowWasPlacedForController(cowFeedController1);
        }
        if (cowWasPlacedAtFarm != null)
        {
            cowWasPlacedAtFarm();
        }
        toolLevel += 1;
        refreshLvlCounter();
    }

    void refreshLvlCounter()
    {
        toolLvlCounterNow.text = toolLevel.ToString();
        toolLvlCounterNext.text = (toolLevel + 1).ToString();
    }
    void addToUpgradeCost()
    {
        upgradeCost *= 1;
        refreshUpgradeCostCounter();
    }

    void refreshUpgradeCostCounter()
    {
        toolUpgradeCost.text = "x " + upgradeCost.ToString();
    }
    void subtractCostFromMainCounter()
    {
        cowMainResourceCounter.AddToCounter(-upgradeCost);
    }
}
