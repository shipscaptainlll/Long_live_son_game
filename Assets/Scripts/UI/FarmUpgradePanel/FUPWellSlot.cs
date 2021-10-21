using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FUPWellSlot : MonoBehaviour
{
    public Button upgradeButton;
    public bool isConstructed;
    public GameObject wellGO;
    public int toolLevel;
    public int upgradeCost;
    public Text toolLvlCounterNow;
    public Text toolLvlCounterNext;
    public Text toolUpgradeCost;
    public RockResourceCounter rockResourceCounter;

    public event Action wellConstructer = delegate { };  //Send notification that well is constructed to fifth quest

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(upgradeElement);
        toolLevel = 0;
        upgradeCost = 100;
        refreshUpgradeCostCounter();
    }

    void upgradeElement()
    {
        if (upgradeCost <= rockResourceCounter.count)
        {
            subtractCostFromMainCounter();
            addToCounter();
            addToUpgradeCost();
        }
    }

    void addToCounter()
    {
        toolLevel += 1;
        if (toolLevel > 0 && isConstructed == false)
        {
            if (wellConstructer != null)
            {
                wellConstructer(); //Send notification that well is constructed to fifth quest
            }
            isConstructed = true;
            wellGO.SetActive(true);
        }
        refreshLvlCounter();
    }

    void refreshLvlCounter()
    {
        toolLvlCounterNow.text = toolLevel.ToString();
        toolLvlCounterNext.text = (toolLevel + 1).ToString();
    }
    void addToUpgradeCost()
    {
        upgradeCost *= 3;
        refreshUpgradeCostCounter();
    }

    void refreshUpgradeCostCounter()
    {
        toolUpgradeCost.text = "x " + upgradeCost.ToString();
    }
    void subtractCostFromMainCounter()
    {
        rockResourceCounter.AddToCounter(-upgradeCost);
    }
}
