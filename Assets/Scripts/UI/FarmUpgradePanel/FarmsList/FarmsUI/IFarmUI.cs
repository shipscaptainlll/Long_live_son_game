
using System;

public interface IFarmUI
{
    public int FarmLevel
    {
        get;
    }

    public int FarmNumber
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
    public event Action<IFarmUI> FarmReached;
    public event Action FarmIsCreated;
}
