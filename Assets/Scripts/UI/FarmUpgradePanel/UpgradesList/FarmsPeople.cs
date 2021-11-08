using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmsPeople : MonoBehaviour
{
    [SerializeField] GoldResourceCounter GoldResourceCounter;
    [SerializeField] Transform Person1;
    [SerializeField] Transform Person2;
    [SerializeField] Button UpgradeButton;
    [SerializeField] Text CurrentLevelText;
    [SerializeField] Text NextLevelText;
    [SerializeField] Text UpgradeCostText;
    [SerializeField] Text PayementText;
    int _currentLevel;
    int _nextLevel;
    int _maxLevel;
    int _paymentEachSecond;
    int _paymentOverall;
    int _upgradeCost;
    bool _personOneActive;
    bool _personTwoActive;


    void Start()
    {
        _currentLevel = 0;
        _nextLevel = 1;
        _maxLevel = 2;
        _paymentEachSecond = 1;
        _upgradeCost = 100;
        UpgradeButton.onClick.AddListener(HirePerson);
    }

    void HirePerson()
    {
        if (_currentLevel < _maxLevel && GoldResourceCounter.count >= _upgradeCost)
        {
            AddToLevel();
            ShowPerson();
            AddPayement();
            RefreshLevels();
        }
    }

    void AddToLevel()
    {
        _currentLevel++;
        _nextLevel++;
    }

    void AddPayement()
    {
        _paymentOverall -= _paymentEachSecond;
        RefreshPayements();
    }

    void RefreshPayements()
    {
        PayementText.text = _paymentOverall.ToString();
    }

    void ShowPerson()
    {
        if (_currentLevel == 1)
        {
            Person1.gameObject.SetActive(true);
        }
        else if (_currentLevel == 2)
        {
            Person2.gameObject.SetActive(true);
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
