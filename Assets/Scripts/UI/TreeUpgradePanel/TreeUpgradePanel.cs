using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeUpgradePanel : MonoBehaviour
{
    public Slider treeUpgradeSlider;
    public MBagResourceCounter MBagResourceCounter;
    public Text upgradeCostCounter;
    public Text LevelCounter;
    public Button UpgradeTreeButton;
    Vector3 LevelCounterOriginPosition;
    public int TreeLevel;
    public int TreeFloor;
    public float upgradeCost;
    public float upgradeCostOrig;

    public event Action<int> TreeLevelChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        TreeLevel = 1;
        TreeFloor = 1;
        LevelCounterOriginPosition = LevelCounter.transform.position;
        treeUpgradeSlider.onValueChanged.AddListener(delegate { UseSlider(); });
        UpgradeTreeButton.onClick.AddListener(delegate { UpgradeTree(); });
        upgradeCostOrig = 0.1f;
        upgradeCost = upgradeCostOrig;
        RefreshCounter();
    }

    //Upgrades tree level and takes appropriate resources for that
    private void UpgradeTree()
    {
        if (upgradeCost <= MBagResourceCounter.count)
        {
            TreeLevel = (int) treeUpgradeSlider.value;
            CheckTreeFloor();
            CalculateUpgradeCost();
            if (TreeLevelChanged != null)
            {
                TreeLevelChanged(TreeFloor);
            }
        }
        
    }

    //Checks which floor is right now available and saves it in memory
    private void CheckTreeFloor()
    {
        if (TreeLevel < 10)
        {
            TreeFloor = 1;
        } else if (TreeLevel < 25)
        {
            TreeFloor = 2;
        } else if (TreeLevel < 100)
        {
            TreeFloor = 3;
        } else if (TreeLevel == 100)
        {
            TreeFloor = 4;
        }
    }

    //Calls tree upgrade cost calculation and cost display on UI panel, keeps slider on min position and moves level counter with slider
    public void UseSlider()
    {
        //Kepps slider on min height, if tree level is 14, slider will not go lower
        if (treeUpgradeSlider.value < TreeLevel)
        {
            treeUpgradeSlider.value = TreeLevel;
        }
        //Keeps level counter on the same position as slider and shows it s value on UI panel
        LevelCounter.transform.position = (LevelCounterOriginPosition + new Vector3(0,treeUpgradeSlider.value,0) * 4.35f);
        //Calculate upgrade cost
        CalculateUpgradeCost();
    }

    //Calculates upgrade cost when slide is moved
    private void CalculateUpgradeCost()
    {
        int sliderValue = (int)treeUpgradeSlider.value;
        LevelCounter.text = sliderValue.ToString();
        upgradeCost = (int) Math.Ceiling(sliderValue * sliderValue * upgradeCostOrig - TreeLevel * TreeLevel * upgradeCostOrig);
        RefreshCounter();
    }

    //Displays this tree upgrade cost on UI panel
    public void RefreshCounter()
    {
        upgradeCostCounter.text = "x " + Math.Ceiling(upgradeCost).ToString("0.0");
    }
}
