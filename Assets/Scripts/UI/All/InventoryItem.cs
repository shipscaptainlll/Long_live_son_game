using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name
    {
        get;
    }

    Sprite Image
    {
        get;
    }

    String Type
    {
        get;
    }

    String Pickable
    {
        get;
    }
    int Cost
    {
        get;
    }

    int CountPerObject
    {
        get;
    }

    int Count { get; set; }


    void OnPickUp();

    void OnDrop();
    
}



public class InventoryItemEventArgs : EventArgs, System.ICloneable
{
    public object Clone()
    {
        return MemberwiseClone();
    }

    public IInventoryItem Item;

    public InventoryItemEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}