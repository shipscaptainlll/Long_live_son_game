using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm1 : MonoBehaviour, IFarm
{
    [SerializeField] 
    byte farmLevel;
    byte farmNumber;

    public byte FarmLevel
    {
        get
        {
            return farmLevel;
        }
    }

    public byte FarmNumber
    {
        get
        {
            return farmNumber;
        }
    }


    // Update is called once per frame
    void Start()
    {
        farmLevel = 2;
        farmNumber = 1;
    }
}
