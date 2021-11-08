using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmsCows : MonoBehaviour
{
    [SerializeField] CowMainResourceCounter CowResourceCounter;
    [SerializeField] Transform Cow;
    [SerializeField] Button UpgradeButton;
    [SerializeField] Text CurrentLevelText;
    [SerializeField] Text NextLevelText;
    [SerializeField] Text UpgradeCostText;
    [SerializeField] Text WasteCarrotsText;
    [SerializeField] Text WasteApplesText;
    [SerializeField] Text ManureText;
    int _currentLevel;
    int _nextLevel;
    int _maxLevel;
    int _carrotsPerSecond;
    int _applesPerSecond;
    int _manurePerSecond;
    int _carrotsOverall;
    int _applesOverall;
    int _upgradeCost;
    int _manureOverall;
    bool _cowActive;


    void Start()
    {
        _currentLevel = 0;
        _nextLevel = 1;
        _maxLevel = 1;
        _carrotsPerSecond = 1;
        _applesPerSecond = 1;
        _manurePerSecond = 1;
        _upgradeCost = 1;
        UpgradeButton.onClick.AddListener(CreateCow);
    }

    void CreateCow()
    {
        if (_currentLevel < _maxLevel && CowResourceCounter.count >= _upgradeCost)
        {
            AddToLevel();
            ShowCow();
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
        _carrotsOverall = -_carrotsPerSecond;
        _applesOverall = -_applesPerSecond;
        _manureOverall = _manurePerSecond;
        RefreshPayements();
    }

    void RefreshPayements()
    {
        WasteCarrotsText.text = _carrotsOverall.ToString();
        WasteApplesText.text = _applesOverall.ToString();
        ManureText.text = _manureOverall.ToString();
    }

    void ShowCow()
    {
        Cow.gameObject.SetActive(true);
        _cowActive = true;
    }

    void RefreshLevels()
    {
        CurrentLevelText.text = _currentLevel.ToString();
        NextLevelText.text = _nextLevel.ToString();
        if (_nextLevel >= _maxLevel)
        {
            NextLevelText.text = "MAX";
        }
    }
}
