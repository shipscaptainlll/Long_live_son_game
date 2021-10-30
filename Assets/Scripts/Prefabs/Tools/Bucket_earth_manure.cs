using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_earth_manure : MonoBehaviour
{
    public event Action FinishedFertilizingEarth = delegate { };
    
    void finishedFertilizing()
    {
        NotifyThatFininshedFertilizing();
        HideThisObject();
    }

    private void NotifyThatFininshedFertilizing()
    {
        if (FinishedFertilizingEarth != null)
        {
            FinishedFertilizingEarth();
        }
    }

    private void HideThisObject()
    {
        transform.gameObject.SetActive(false);
    }
}
