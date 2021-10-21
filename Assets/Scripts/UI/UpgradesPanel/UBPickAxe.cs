using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UBPickAxe : MonoBehaviour
{
    public int toolLevel;
    public float toolSpeed;
    public int upgradeCost;
    public float upgradeBarFillAmmount;
    public GameObject OLvlCounter;
    private Text lvlCounter;
    public GameObject OUpgradeCostCounter;
    private Text upgradeCostCounter;
    public GameObject upgradeBar;
    private Image upgradeBarImage;

    //Number of available
    public GameObject totalObjectHolder;
    public List<GameObject> totalObjects;
    public Button buyNewToolButton;
    public int toolsBought;
    public int toolsBoughtMax = 8;
    public float toolNumberUpgradeCost;
    public float toolNumberUpgradeCostMultiplier;
    public Text toolNumberUpgradeCostCounter;
    public RShardResourceCounter rShardResourceCounter;
    public PickAxeToolsPanel pickAxeToolsPanel;

    public event Action<float, int> toolParametersRefreshed = delegate { };
    public event Action<int> newToolBought = delegate { };
    
    public event Action pickAxeConjured = delegate { }; //Create an event for third quest - learn pickaxe conjuration
    // Start is called before the first frame update
    void Start()
    {
        totalObjects = new List<GameObject>();
        toolsBought = 0;
        toolsBoughtMax = 8;
        toolNumberUpgradeCost = 10;
        toolNumberUpgradeCostMultiplier = 2.7f;
        toolLevel = 1;
        upgradeCost = 10;
        toolSpeed = 1;
        upgradeBarFillAmmount = 0.04f;
        lvlCounter = OLvlCounter.GetComponent<Text>();
        upgradeCostCounter = OUpgradeCostCounter.GetComponent<Text>();
        upgradeBarImage = upgradeBar.GetComponent<Image>();
        RefreshLvlCounter();
        RefreshUpgradeCostCounter();
        StartCoroutine(upgradeBarTest(0.04f));
        for (int i = 0; i < 8; i++)
        {
            totalObjects.Add(totalObjectHolder.transform.GetChild(i).gameObject);

        }
        toolNumberCostCounterRefresh();
        buyNewToolButton.onClick.AddListener(buyNewTool);
        pickAxeToolsPanel.enabled = true;
    }

    // Update is called once per frame

    void RefreshLvlCounter()
    {
        lvlCounter.text = toolLevel.ToString();
    }

    void RefreshUpgradeCostCounter()
    {
        upgradeCostCounter.text = upgradeCost.ToString(); //Debug.Log(upgradeCost);
    }

    void RefreshUpgradeBar()
    {
        upgradeBarImage.fillAmount = upgradeBarFillAmmount;
    }

    void RecalculateToolSpeed()
    {
        toolSpeed += toolLevel * 0.1f;
    }

    void RecalculateUpgradeCost()
    {
        upgradeCost += 100;
    }

    private IEnumerator upgradeBarTest(float pct)
    {
        //Debug.Log("4.ChangeToPct");
        float preChangePct = upgradeBarImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < 0.25f)
        {
            elapsed += Time.deltaTime;
            upgradeBarImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / 0.25f);
            yield return null;
        }


        upgradeBarImage.fillAmount = pct;
    }
        void RecalculateUpgradeBarAmmount()
    {
        upgradeBarFillAmmount += 0.04f;
    }

    void refreshToolInstanceParameters(float speed, int lvl)
    {
        toolParametersRefreshed(speed, lvl);
    }

    public void UpgradeTool()
    {
        toolLevel += 1;
        RecalculateToolSpeed();
        RefreshLvlCounter();
        RecalculateUpgradeCost();
        RefreshUpgradeCostCounter();
        RecalculateUpgradeBarAmmount();
        StartCoroutine(upgradeBarTest(upgradeBarFillAmmount));
        //RefreshUpgradeBar();
        refreshToolInstanceParameters(toolSpeed, toolLevel);
    }

    public void buyNewTool()
    {
        if (pickAxeConjured != null)
        {
            pickAxeConjured();
        }
        if (toolsBought < toolsBoughtMax && toolNumberUpgradeCost <= rShardResourceCounter.count)
        {
            toolsBought += 1;
            refreshToolsUI();
            rShardResourceCounter.AddToCounter(-toolNumberUpgradeCost);
            toolNumberCostIncrease();
            newToolBought(toolsBought);
            
        } else if (toolsBought >= toolsBoughtMax)
        {
            Debug.Log("Reached max tools");
        } else if (toolNumberUpgradeCost > rShardResourceCounter.count)
        {
            Debug.Log("Too costy");
            Debug.Log("Upgrade cost: " + toolNumberUpgradeCost + " magic shards");
            Debug.Log("You have: " + rShardResourceCounter.count + " ms");
        }
    }

    public void refreshToolsUI()
    {
        for (int i = 0; i < toolsBought; i++)
        {
            totalObjects[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    public void toolNumberCostIncrease()
    {
        toolNumberUpgradeCost *= toolNumberUpgradeCostMultiplier;
        if (toolsBought < toolsBoughtMax)
        {
            toolNumberCostCounterRefresh();
        } else if (toolsBought >= toolsBoughtMax)
        {
            toolNumberCostCounterRefresh(1);
        }
        
    }

    public void toolNumberCostCounterRefresh()
    {
        toolNumberUpgradeCostCounter.text = toolNumberUpgradeCost.ToString("0");
    }
    public void toolNumberCostCounterRefresh(int i)
    {
        toolNumberUpgradeCostCounter.text = "MAX";
    }
}
