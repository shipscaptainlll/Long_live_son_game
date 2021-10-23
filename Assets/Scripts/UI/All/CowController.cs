using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public List<CowFeedController> listOfCows;
    public FUPCows mainFarmPastures;
    
    // Start is called before the first frame update
    void Start()
    {
        mainFarmPastures.cowWasPlacedForController += initializeCow;
    }

    public void initializeCow(CowFeedController cowFeedController)
    {
        listOfCows.Add(cowFeedController);
        
    }
}
