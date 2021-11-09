using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMainFarm : MonoBehaviour
{
    [SerializeField] Button MiddleButton;
    [SerializeField] Transform FloorUpgrades;

    public event Action FarmChosen = delegate { };
    public event Action FarmsUnchosen = delegate { };
    private void Start()
    {
        MiddleButton.onClick.AddListener(OpenCurrent);
    }

    void OpenCurrent()
    {
        CloseAllPanels();
        CallUpgradePanel();
        AdjustPriority();
    }

    void CloseAllPanels()
    {
        FarmsUnchosen();
    }

    void CallUpgradePanel()
    {
        FarmChosen();
    }

    void AdjustPriority()
    {
        FloorUpgrades.SetAsLastSibling();
    }
}
