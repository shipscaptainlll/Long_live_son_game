using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
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

        public IResource Res;

        public IResourceEventArgs(IResource res)
        {
            Res = res;
        }
    }

    public class ResourcesEventArgs : EventArgs
    {


        public IResource Resource;

        public ResourcesEventArgs(IResource resource)
        {
            Resource = resource;
        }
    }
}
