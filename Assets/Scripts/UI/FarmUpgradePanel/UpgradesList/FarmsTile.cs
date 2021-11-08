using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmsTile : MonoBehaviour
{
    [SerializeField] MBagResourceCounter MBagResourceCounter;
    [SerializeField] Transform PileOFDirt1;
    [SerializeField] Transform PileOFDirt2;
    [SerializeField] Button UpgradeButton;
    [SerializeField] Text CurrentLevelText;
    [SerializeField] Text NextLevelText;
    [SerializeField] Text UpgradeCostText;
    [SerializeField] Text WasteCarrotsText;
    [SerializeField] Text CarrotsOverallText;
    [SerializeField] Text WasteApplesText;
    [SerializeField] Text ApplesOverallText;
    int _currentLevel;
    int _nextLevel;
    int _maxLevel;
    int _carrotSeedsPerSecond;
    int _appleSeedsPerSecond;
    int _carrotsPerSecond;
    int _applesPerSecond;
    int _carrotSeedsOverall;
    int _appleSeedsOverall;
    int _carrotsOverall;
    int _applesOverall;
    int _upgradeCost;
    bool _pileOneActive;
    bool _pileTwoActive;


    void Start()
    {
        _currentLevel = 0;
        _nextLevel = 1;
        _maxLevel = 2;
        _carrotSeedsPerSecond = 1;
        _carrotsPerSecond = 1;
        _appleSeedsPerSecond = 1;
        _applesPerSecond = 1;
        _upgradeCost = 1;
        UpgradeButton.onClick.AddListener(CreatePile);
    }

    void CreatePile()
    {
        if (_currentLevel < _maxLevel && MBagResourceCounter.count >= _upgradeCost)
        {
            AddToLevel();
            ShowPile();
            WasteResources();
            RefreshLevels();
        }
    }

    void AddToLevel()
    {
        _currentLevel++;
        _nextLevel++;
    }

    void WasteResources()
    {
        if (PileOFDirt2.gameObject.activeSelf == true)
        {
            _appleSeedsOverall = -_appleSeedsPerSecond;
            _applesOverall = _appleSeedsPerSecond;
        }
        else if (PileOFDirt1.gameObject.activeSelf == true)
        {
            _carrotSeedsOverall = -_carrotSeedsPerSecond;
            _carrotsOverall = _carrotsPerSecond;
        }
        RefreshPayements();
    }

    void RefreshPayements()
    {
        WasteCarrotsText.text = _carrotSeedsOverall.ToString();
        CarrotsOverallText.text = _carrotsOverall.ToString();
        WasteApplesText.text = _appleSeedsOverall.ToString();
        ApplesOverallText.text = _applesOverall.ToString();
    }

    void ShowPile()
    {
        if (_currentLevel == 1)
        {
            PileOFDirt1.gameObject.SetActive(true);
            _pileOneActive = true;
        }
        else if (_currentLevel == 2)
        {
            PileOFDirt2.gameObject.SetActive(true);
            _pileTwoActive = true;
        }
    }

    void RefreshLevels()
    {
        CurrentLevelText.text = _currentLevel.ToString();
        NextLevelText.text = _nextLevel.ToString();
        if (_nextLevel == 3)
        {
            NextLevelText.text = "MAX";
        }
    }

}
