using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm3 : MonoBehaviour, IFarm
{
    [SerializeField] DestroyableWall DestroyableWall;
    int _farmLevel;
    int _farmNumber;
    bool _isReached;

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

    public event Action FarmFound = delegate { }; 
    // Start is called before the first frame update
    void Start()
    {
        _farmNumber = 3;
        DestroyableWall.WallWasDestroyed += ReachFarm;
        gameObject.SetActive(false);
    }

    void ReachFarm(int wallNumber)
    {
        if (_farmNumber == wallNumber)
        {
            _isReached = true;
            UnsubscribeWallDestroyed();
            NotifyUI();
        }
    }

    void NotifyUI()
    {
        if (FarmFound != null)
        {
            FarmFound();
        }
    }
    void UnsubscribeWallDestroyed()
    {
        DestroyableWall.WallWasDestroyed -= ReachFarm;
    }
}
