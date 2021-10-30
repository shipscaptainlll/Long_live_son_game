using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors_tree : MonoBehaviour
{
    public event Action PlantHarvested = delegate { };

    void FinishedHarvestingPlant()
    {
        NotifyThatFininshedGrowing();
        HideThisObject();
    }

    private void NotifyThatFininshedGrowing()
    {
        if (PlantHarvested != null)
        {
            PlantHarvested();
        }
    }

    private void HideThisObject()
    {
        transform.gameObject.SetActive(false);
        transform.parent.Find("Scissors1").gameObject.SetActive(false);
        transform.parent.Find("Carrots").gameObject.SetActive(false);
        transform.parent.Find("Apples").gameObject.SetActive(false);
    }
}
