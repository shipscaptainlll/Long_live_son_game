using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FUPGrassTent : MonoBehaviour
{
    public Button upgradeButton;
    public int toolLevel;
    public int upgradeCost;
    public bool isConstructed;
    public GameObject grassTentGO;
    public Text toolLvlCounterNow;
    public Text toolLvlCounterNext;
    public Text toolUpgradeCost;
    public LogResourceCounter logResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        isConstructed = true;
        upgradeButton.onClick.AddListener(upgradeElement);
        toolLevel = 1;
        upgradeCost = 500;
        refreshLvlCounter();
    }

    void upgradeElement()
    {
        if (upgradeCost <= logResourceCounter.count)
        {
            subtractCostFromMainCounter();
            addToCounter();
            addToUpgradeCost();
        }
    }

    void addToCounter()
    {
        toolLevel += 1;
        if (toolLevel > 0 && isConstructed == false && !grassTentGO.activeSelf)
        {
            isConstructed = true;
            grassTentGO.SetActive(true);
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
        logResourceCounter.AddToCounter(-upgradeCost);
    }
}
