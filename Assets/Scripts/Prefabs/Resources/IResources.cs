using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourcess
{
    public string Type
    {
        get;
    }

    public int MaxHealth
    {
        get;
    }

    public class IResourceEventArgs : EventArgs
    {
        
        public IResources Res;

        public IResourceEventArgs(IResources res)
        {
            Res = res;
        }
    }
}
