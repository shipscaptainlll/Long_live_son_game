using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFarmUI123
{
    public byte FarmLevel
    {
        get;
    }

    public byte FarmNumber
    {
        get;
    }

    public bool IsKnown
    {
        get;
    }

    public bool IsReached
    {
        get;
    }

    public bool IsCreated
    {
        get;
    }

}
