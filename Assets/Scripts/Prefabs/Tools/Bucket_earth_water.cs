using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_earth_water : MonoBehaviour
{
    public event Action FinishedWateringEarth = delegate { };

    void finishedWatering()
    {
        NotifyThatFininshedWatering();
        HideThisObject();
    }

    private void NotifyThatFininshedWatering()
    {
        if (FinishedWateringEarth != null)
        {
            FinishedWateringEarth();
        }
    }

    private void HideThisObject()
    {
        transform.gameObject.SetActive(false);
    }
}
