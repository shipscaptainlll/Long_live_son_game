using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FUPGroundTile : MonoBehaviour
{
    public Button upgradeButton; //Button that is needed to create new tile
    public List<GameObject> GroundTilesList; //List that keeps all ground tiles in script
    public GameObject GroundTiles; //Gameobject that contains ground tiles in unity hierarchy
    public Text toolLvlCounterNow; //Text that shows current number of tiles
    public Text toolLvlCounterNext; //Text that shows next number of tiles if upgraded
    public Text toolUpgradeCost; //Text that shows cost of upgrade on UI panel
    public MBagResourceCounter mBagResourceCounter; //Manure bag main counter script
    public int toolLevel; //Number of tiles already created
    public int toolMaxLevel; //Number of max tiles available for creating
    public int upgradeCost; //Cost of one tile creation

    //Notifies Seventeenth quest - that first ground tile was created
    public event Action GroundTileAdded = delegate { };

    //Farm upgrade panel quest of ground tile element, controls upgrades from UI panel
    void Start()
    {
        //Add listener to upgrade button on UI
        upgradeButton.onClick.AddListener(upgradeElement);
        //Initialize basic parameters
        toolLevel = 0;
        toolMaxLevel = 6;
        upgradeCost = 1;
        //Find all tiles from unity ierarchy and add them to the list object
        for (int i = 0; i < 6; i++)
        {
            GroundTilesList.Add(GroundTiles.transform.GetChild(i).gameObject);
        }
    }

    //Notifies quest, subtracts cost of pile creation, add count of piles to counter, shows tile in world
    void upgradeElement()
    {
        if (upgradeCost <= mBagResourceCounter.count && toolLevel < toolMaxLevel)
        {
            if (GroundTileAdded != null)
            {
                GroundTileAdded();
            }
            subtractCostFromMainCounter();
            addToCounter();
            ShowTileObject();
            if (toolLevel == toolMaxLevel)
            {
                toolLvlCounterNext.text = "MAX";
            }
        }
    }

    //Adds to number of tiles counter
    void addToCounter()
    {
        toolLevel += 1;
        refreshLvlCounter();
    }

    //Refreshes counters on UI table that shows current number of tiles and number after upgrades
    void refreshLvlCounter()
    {
        toolLvlCounterNow.text = toolLevel.ToString();
        toolLvlCounterNext.text = (toolLevel + 1).ToString();
    }

    //Shows created tile object in unity
    void ShowTileObject()
    {
        GroundTilesList[toolLevel - 1].SetActive(true);
    }

    //Refreshes upgrade cost of tile creation on UI table
    void refreshUpgradeCostCounter()
    {
        toolUpgradeCost.text = "x " + upgradeCost.ToString();
    }

    //Subtracts cost of tile creation from manure bag main resource counter
    void subtractCostFromMainCounter()
    {
        mBagResourceCounter.AddToCounter(-upgradeCost);
    }
}
