using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public List<CowFeedController> ListOfCows; //List of all cows, activated in the game
    public FUPCows MainFarmPastures; //Script that provides us with new cows
    private bool CowsActivated;
    
    // Start is called before the first frame update
    void Start()
    {
        CowsActivated = false;
        //Subscribe for mainFarmUpgradePanel, to get notification about new cows activated
        MainFarmPastures.cowWasPlacedForController += InitializeCow;
        MainFarmPastures.cowWasPlacedForController += InitializeFirstCow;
    }

    private void InitializeFirstCow(CowFeedController cowFeedController)
    {
        CowsActivated = true;
        StartCoroutine(ArrangeFeedingCows());
        MainFarmPastures.cowWasPlacedForController -= InitializeFirstCow;
    }
    //Initializes new cow - by adding it into the list of activated cows
    private void InitializeCow(CowFeedController cowFeedController)
    {
        ListOfCows.Add(cowFeedController);
        
    }

    //Checks each cow in the list and notifies them each second that they should start eating 
    private IEnumerator ArrangeFeedingCows()
    {
        while(CowsActivated == true)
        {
            for (int i = 0; i < ListOfCows.Count; i++)
            {
                if (ListOfCows[i].IsFeeded == false)
                {
                    ListOfCows[i].RemindCowToEat();
                }
            }
            yield return new WaitForSeconds(1);
        }
        
    }
}
