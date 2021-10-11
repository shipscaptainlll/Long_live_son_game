using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UBScissors : MonoBehaviour
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
    

    public event Action<float, int> toolParametersRefreshed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        toolLevel = 1;
        upgradeCost = 10;
        toolSpeed = 1;
        upgradeBarFillAmmount = 0.1f;
        lvlCounter = OLvlCounter.GetComponent<Text>();
        upgradeCostCounter = OUpgradeCostCounter.GetComponent<Text>();
        upgradeBarImage = upgradeBar.GetComponent<Image>();
        RefreshLvlCounter();
        RefreshUpgradeCostCounter();
        StartCoroutine(upgradeBarTest(0.1f));
    }

    // Update is called once per frame

    void RefreshLvlCounter()
    {
        lvlCounter.text = toolLevel.ToString();
    }

    void RefreshUpgradeCostCounter()
    {
        upgradeCostCounter.text = upgradeCost.ToString(); Debug.Log(upgradeCost);
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
        upgradeCost += 150;
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
        upgradeBarFillAmmount += 0.1f;
    }

    void refreshToolInstanceParameters(float speed, int lvl)
    {
        toolParametersRefreshed(speed, lvl);
        Debug.Log("ready ub");
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
}
