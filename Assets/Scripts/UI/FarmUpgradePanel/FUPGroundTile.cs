using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FUPGroundTile : MonoBehaviour
{
    public Button upgradeButton;
    public int toolLevel;
    public int upgradeCost;
    public Text toolLvlCounterNow;
    public Text toolLvlCounterNext;
    public Text toolUpgradeCost;
    public MBagResourceCounter mBagResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(upgradeElement);
        toolLevel = 0;
        upgradeCost = 500;
    }

    void upgradeElement()
    {
        if (upgradeCost <= mBagResourceCounter.count)
        {
            subtractCostFromMainCounter();
            addToCounter();
            addToUpgradeCost();
        }
    }

    void addToCounter()
    {
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
        upgradeCost *= 3;
        refreshUpgradeCostCounter();
    }

    void refreshUpgradeCostCounter()
    {
        toolUpgradeCost.text = "x " + upgradeCost.ToString();
    }
    void subtractCostFromMainCounter()
    {
        mBagResourceCounter.AddToCounter(-upgradeCost);
    }
}
