using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowFeedController : MonoBehaviour
{
    public bool IsFeeded; //Mark if cow is being feeded
    public GrassResourceCounter GrassResourceCounter; //Grass resource main counter script
    public WaterResourceCounter WaterResourceCounter; //Water resource main counter script
    public ManureResourceCounter ManureResourceCounter; //Manure resource main counter script
    public MeditationPanel MeditationPanel; //Script that will change mining speed when character meditating
    public Animator ManureProductionTimer; //Animation that tics each n seconds and restarts production process
    public byte AnimationProductionSpeed; //Speed of base animation in animator ManureProductionTimer
    public byte TimerCycleLifetime;
    public byte CycleLifetime; //How long does feeding cycle lasts
    public byte GrassPerFeedingCycle; //How much grass cow eats per cycle
    public float GrassSpendingSpeed; //How much grass cow eats each second
    public byte WaterPerFeedingCycle; //How much water cow drinks per cycle
    public float WaterSpendingSpeed; //How much water cow drinks each second
    public byte ManurePerFeedingCycle; //How much manure is produced per cycle
    public float ManureProducingSpeed; //How much manure cow produces each second

    // Start is called before the first frame update
    void Start()
    {
        //Feeding cycle will last that long (seconds)
        CycleLifetime = 10;
        TimerCycleLifetime = 60;
        //Cow will eat that much per cycle
        GrassPerFeedingCycle = 100;
        //Cow will drink that much per cycle
        WaterPerFeedingCycle = 100;
        //Cow will poop that much per cycle
        ManurePerFeedingCycle = 1;
        AnimationProductionSpeed = 6;
        AdjustAnimationSpeed();
        IsFeeded = false;
        CalculateMiningSpeed();
        //Subscribe on meditation event, when event happens, mining speed will be changed correspondigly to state(meditation started/stopped)
        MeditationPanel.startedMeditating += EnterLeaveMeditationMode;
    }

    // Checks if it is enough resources to start eating
    public void RemindCowToEat()
    {
        if (WaterPerFeedingCycle <= WaterResourceCounter.count 
            && GrassPerFeedingCycle <= GrassResourceCounter.count)
        {
            //StartCoroutine(EatAndPoop());
            StartProductionTimer();
        }
    }

    //Calculates speeds (per second) of giving and getting resources during feeding
    private void CalculateMiningSpeed()
    {
        WaterSpendingSpeed = (WaterPerFeedingCycle * AnimationProductionSpeed) / TimerCycleLifetime;
        GrassSpendingSpeed = (GrassPerFeedingCycle * AnimationProductionSpeed) / TimerCycleLifetime;
        ManureProducingSpeed = (float) (ManurePerFeedingCycle * AnimationProductionSpeed) / TimerCycleLifetime;
    }

    //Starts process of eating by animation that works as a timer, also changes speeds of production in UI
    private void StartProductionTimer()
    {
        IsFeeded = true;
        AddProductionSpeedsToUI();
        ManureProductionTimer.Play("PastureAnimation");
    }

    //Stops process of eating by changing to hollow animation, also changes speed of production in UI
    private void StopProductionTimer()
    {
        IsFeeded = false;
        RemoveProductionSpeedsFromUI();
        ManureProductionTimer.Play("Idle");
    }

    //Adds manure and subtracts resources each feeding cycle, stops eating if it is not enough resources
    private void EatAndPoopAnimation()
    {
        
        ManureResourceCounter.AddToCounter(ManurePerFeedingCycle);
        if (WaterPerFeedingCycle <= WaterResourceCounter.count
            && GrassPerFeedingCycle <= GrassResourceCounter.count)
        {
            WaterResourceCounter.AddToCounter(-WaterPerFeedingCycle);
            GrassResourceCounter.AddToCounter(-GrassPerFeedingCycle);
        } else if (WaterPerFeedingCycle > WaterResourceCounter.count
            || GrassPerFeedingCycle > GrassResourceCounter.count)
        {
            StopProductionTimer();
        }
    }
    
    //During meditation time flows faster, it speeds up/ or descres the mining process using miningMultiplier parameter
    public void EnterLeaveMeditationMode(byte miningSpeedMultiplier, bool IsMeditatingStatus)
    {
        if (IsMeditatingStatus == true) //will be used when meditation started
        {
            AnimationProductionSpeed *= miningSpeedMultiplier;
            AdjustAnimationSpeed();
            if (IsFeeded == true)
            {
                RemoveProductionSpeedsFromUI();
                CalculateMiningSpeed();
                AddProductionSpeedsToUI();
            }
        }
        else if (IsMeditatingStatus == false) //will be used when meditation stopped
        {
            AnimationProductionSpeed /= miningSpeedMultiplier;
            AdjustAnimationSpeed();
            if (IsFeeded == true)
            {
                RemoveProductionSpeedsFromUI();
                CalculateMiningSpeed();
                AddProductionSpeedsToUI();
            }
        }
    }
    
    //Adjusts animation speed of the main animation that works as a timer
    private void AdjustAnimationSpeed()
    {
        ManureProductionTimer.speed = AnimationProductionSpeed;
    }

    //Adds speeds of manure production, grass spending and water spending to UI panel
    private void AddProductionSpeedsToUI()
    {
        WaterResourceCounter.AddToMineSpeedCounter(-WaterSpendingSpeed);
        GrassResourceCounter.AddToMineSpeedCounter(-GrassSpendingSpeed);
        ManureResourceCounter.AddToMineSpeedCounter(ManureProducingSpeed);
    }

    //Removes speeds of manure production, grass spending and water spending from UI panel
    private void RemoveProductionSpeedsFromUI()
    {
        WaterResourceCounter.AddToMineSpeedCounter(WaterSpendingSpeed);
        GrassResourceCounter.AddToMineSpeedCounter(GrassSpendingSpeed);
        ManureResourceCounter.AddToMineSpeedCounter(-ManureProducingSpeed);
    }
}
