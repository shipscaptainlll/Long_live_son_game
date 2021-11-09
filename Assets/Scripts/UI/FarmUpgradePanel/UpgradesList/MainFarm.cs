using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFarm : MonoBehaviour
{
    [SerializeField] ChooseFarm ChooseFarm;
    [SerializeField] ChooseMainFarm ChooseMainFarm;

    void Start()
    {
        ChooseFarm.FarmsUnchosen += CloseFarm;
        ChooseMainFarm.FarmChosen += ShowFarm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowFarm()
    {
        transform.GetComponent<CanvasGroup>().alpha = 1;
    }

    void CloseFarm()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
    }
}
