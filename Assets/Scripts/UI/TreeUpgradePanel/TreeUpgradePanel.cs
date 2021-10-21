using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeUpgradePanel : MonoBehaviour
{
    public Slider treeUpgradeSlider;
    public Text upgradeCostCounter;
    public float upgradeCost;
    public float upgradeCostOrig;

    // Start is called before the first frame update
    void Start()
    {
        treeUpgradeSlider.onValueChanged.AddListener(delegate { ega(); });
        upgradeCostOrig = 1;
        upgradeCost = upgradeCostOrig;
        
        RefreshCounter();
    }

    public void ega()
    {
        float sliderValue = treeUpgradeSlider.value * 100;
        upgradeCost = (float)sliderValue * sliderValue * upgradeCostOrig;
        RefreshCounter();
    }

    public void RefreshCounter()
    {
        upgradeCostCounter.text = "x " + upgradeCost.ToString("0.0");
    }
}
