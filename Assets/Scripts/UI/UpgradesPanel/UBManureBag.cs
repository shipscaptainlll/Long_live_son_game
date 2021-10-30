using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UBManureBag : MonoBehaviour
{
    public MBagResourceCounter MBagResourceCounter; //Manure bags resource main counter script
    public ManureResourceCounter ManureResourceCounter; //Manure resource main counter script
    public Button SynthetizeManureButton; //Button on panel that will convert manure into manure bag
    public Text ManureBagsCounter; //UI panel text that shows count of manure bags character has
    private int synthetizeCost; //Cost of one manure bag convertion

    public event Action ManureBagCreated = delegate { };
    void Start()
    {
        //Subscribe on button click
        SynthetizeManureButton.onClick.AddListener(SynthetizeManure);
        //Subscribe on event, when manure bags count in main counter changes, call method that refreshes count in upgrade panel
        MBagResourceCounter.ManureBagsCountChanged += RefreshManureBagsCounter;
        //Initialize cost of convertion
        synthetizeCost = 50;
        //Refresh counter from start, just in case
        RefreshManureBagsCounter(MBagResourceCounter.count);
        
    }

    //When button clicked, adds 1 manure bag to main counter and takes manure from manure counter
    private void SynthetizeManure()
    {
        if (ManureResourceCounter.count >= synthetizeCost)
        {
            ManureResourceCounter.AddToCounter(-synthetizeCost);
            MBagResourceCounter.AddToCounter(1);
            if (ManureBagCreated != null)
            {
                ManureBagCreated();
            }
        } else { }
    }

    //When manure bags count changes in main counter, refreshes text object counter
    void RefreshManureBagsCounter(int manureBagsCount)
    {
        ManureBagsCounter.text = manureBagsCount.ToString(); 
    }
}
