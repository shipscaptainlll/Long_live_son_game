using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFarmUI : MonoBehaviour, IFarmUI
{
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
            return 8;
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
        _farmLevel = 2;
        _farmNumber = 8;
        _isKnown = true;
        _isReached = false;
        _isCreated = false;
    }
}
