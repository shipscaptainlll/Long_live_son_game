using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farms : MonoBehaviour
{
    [SerializeField] ChooseFarm ChooseFarm;
    [SerializeField] ChooseMainFarm ChooseMainFarm;
    [SerializeField] int _farmID;

    void Start()
    {
        ChooseFarm.FarmChosen += ShowFarm;
        ChooseFarm.FarmsUnchosen += CloseFarm;
        ChooseMainFarm.FarmsUnchosen += CloseFarm;
    }

    void ShowFarm(int farmID)
    {
        //Debug.Log("Show farm" + _farmID);
        if (farmID == this._farmID)
        {
            transform.GetComponent<CanvasGroup>().alpha = 1;
            transform.SetAsLastSibling();
        }
    }

    void CloseFarm()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
    }
}
