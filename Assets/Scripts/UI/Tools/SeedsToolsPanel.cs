using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedsToolsPanel : MonoBehaviour
{
    [SerializeField] Button ChooseCarrotSeedsButton;
    [SerializeField] Button ChooseAppleSeedsButton;
    [SerializeField] Button CancelProductionButton;
    [SerializeField] QuickAccessController QuickAccessController;
    private GameObject CurrentlyContactedEarth;
    public event Action<GameObject, String> ProductionTypeChosen = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        ChooseCarrotSeedsButton.onClick.AddListener(ChooseCarrotSeeds);
        ChooseAppleSeedsButton.onClick.AddListener(ChooseAppleSeeds);
        CancelProductionButton.onClick.AddListener(ChooseStopProduction);
        QuickAccessController.ChooseSeedsPanelOpenedFor += RememberEarth;
    }

    private void RememberEarth(GameObject detectedEarth)
    {
        CurrentlyContactedEarth = detectedEarth;
    }

    private void ChooseCarrotSeeds()
    {
        NotifyEarthAboutChoice("CarrotSeeds");
        ForgetEarth();
    }

    private void ChooseAppleSeeds()
    {
        NotifyEarthAboutChoice("AppleSeeds");
        ForgetEarth();
    }

    private void ChooseStopProduction()
    {
        NotifyEarthAboutChoice("StopProduction");
        ForgetEarth();
    }

    private void NotifyEarthAboutChoice(String TypeOfProduction)
    {
        if (ProductionTypeChosen != null)
        {
            ProductionTypeChosen(CurrentlyContactedEarth, TypeOfProduction);
        }
    }

    private void ForgetEarth()
    {
        CurrentlyContactedEarth = null;
    }
}
