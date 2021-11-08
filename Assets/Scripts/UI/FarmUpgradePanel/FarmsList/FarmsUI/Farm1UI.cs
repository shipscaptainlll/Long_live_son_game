using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm1UI : MonoBehaviour, IFarmUI
{
    [SerializeField] InputController InputController;
    [SerializeField] Transform FarmTransform;
    [SerializeField] Button CreateFarmButton;
    [SerializeField] Farm1 Farm;
    byte _farmLevel;
    byte _farmNumber;
    bool _isKnown;
    bool _isReached;
    bool _isCreated;

    public event Action<IFarmUI> FarmReached = delegate { };
    public event Action FarmIsCreated = delegate { };
    public int FarmLevel
    {
        get
        {
            return _farmLevel;
        }
    }

    public int FarmNumber
    {
        get
        {
            return _farmNumber;
        }
    }

    public bool IsKnown
    {
        get
        {
            return _isKnown;
        }
    }

    public bool IsReached
    {
        get
        {
            return _isReached;
        }
    }

    public bool IsCreated
    {
        get
        {
            return _isCreated;
        }
    }

    // Update is called once per frame
    void Start()
    {
        Farm.FarmFound += ReachFarm;
        _farmLevel = 2;
        _farmNumber = 1;
        _isKnown = false;
        _isReached = false;
        _isCreated = false;
        CreateFarmButton.onClick.AddListener(CreateFarm);
    }

    void ReachFarm()
    {
        _isReached = true;
        UnsubscribeFarmFound();
        NotifyManager();
    }

    void NotifyManager()
    {
        if (FarmReached != null)
        {
            FarmReached(this);
        }
    }

    void UnsubscribeFarmFound()
    {
        Farm.FarmFound -= ReachFarm;
    }

    void CreateFarm()
    {
        FarmTransform.gameObject.SetActive(true);
        _isCreated = true;
        NotifyUI();
    }

    void NotifyUI()
    {
        if (FarmIsCreated != null)
        {
            FarmIsCreated();
        }
    }
}
