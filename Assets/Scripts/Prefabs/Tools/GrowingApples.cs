using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingApples : MonoBehaviour
{
    public event Action FinishedGrowingPlant = delegate { };

    void FinishGrowingApples()
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
