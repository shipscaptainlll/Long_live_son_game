using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingCarrots : MonoBehaviour
{
    public event Action FinishedGrowingPlant = delegate { };

    void FinishGrowingCarrots()
    {
        NotifyThatFininshedGrowing();
    }

    private void NotifyThatFininshedGrowing()
    {
        if (FinishedGrowingPlant != null)
        {
            FinishedGrowingPlant();
        }
    }
}
