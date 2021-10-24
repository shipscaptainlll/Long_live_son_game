using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Boulder : MonoBehaviour, IResource
{
    public RockResourceCounter rockResourceCounter; //Rocks resource main counter script
    public RShardResourceCounter rshardResourceCounter; //Rock shards resource main counter script
    public Animator miningAnimation;
    public PickAxe pickAxe;
    public UBPickAxe uBPickAxe;
    public MeditationPanel MeditationPanel; //Script that will change mining speed when character meditating
    public int toolLevel;
    public bool isProcessedManually = false;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    
    private float aminingSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int stonesInBoulder;
    public int shardsInBoulder;
    public bool isProcessed;
    public float cost;
    public float costShard;
    public bool timerStarted = false;
    public int timeElapsedSinceStart;
    public string Type
    {
        get
        {
            return "Boulder";
        }
    }

    public int MaxHealth
    {
        get
        {
            return 100;
        }
    }

    void Start()
    {
        uBPickAxe.toolParametersRefreshed += refreshToolParameters;
        objectMaxHealth = 100;
        stonesInBoulder = 9;
        shardsInBoulder = 1;
        transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        calculateOreCost();
        transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
        pickAxe = transform.Find("Pickaxe").GetComponent<PickAxe>();
        //Subscribe on meditation event, when event happens, mining speed will be changed correspondigly to state(meditation started/stopped)
        MeditationPanel.startedMeditating += enterLeaveMeditationMode; 
    }

    public void calculateOreCost()
    {
        cost = stonesInBoulder;
        costShard = shardsInBoulder;
    }

    public void calculateMiningSpeed()
    {
        Debug.Log(miningAnimation.speed);
        damagePerHit = pickAxe.damagePerHit;
        miningSpeed = stonesInBoulder*(damagePerHit * miningAnimation.speed / objectMaxHealth) ;
        miningShardSpeed = shardsInBoulder * (damagePerHit * miningAnimation.speed / objectMaxHealth);
        adjustMiningSpeedCounter();
    }

    //Refreshes tool parameters (speed of mining animation, level of this tool) and then recalculates mining speed
    public void refreshToolParameters(float speed, int lvl)
    {
        miningAnimation.speed = speed;
        toolLevel = lvl;

        //if ore was automatically mined during tool upgrade, we want to resent previous mine speed and set new one
        if (isProcessed == true)
        {
            //Delete previous mining speed
            adjustMiningSpeedNegative();
            //Recalculate mining speed and set new one
            calculateMiningSpeed();
        }
    }

    //During meditation time flows faster, it speeds up/ or descres the mining process using miningMultiplier parameter
    public void enterLeaveMeditationMode(byte miningSpeedMultiplier, bool IsMeditatingStatus)
    {
        if (IsMeditatingStatus == true) //will be used when meditation started
        {
            Debug.Log("mining speed before multiplying: " + miningAnimation.speed);
            miningAnimation.speed *= miningSpeedMultiplier;
            Debug.Log("mining speed after multiplying: " + miningAnimation.speed);
            //if ore was automatically mined when meditation started, we want to reset previous mine speed and set new one
            if (isProcessed == true)
            {
                //Delete previous mining speed
                adjustMiningSpeedNegative();
                //Recalculate mining speed and set new one
                calculateMiningSpeed();
            }
        } else if (IsMeditatingStatus == false) //will be used when meditation stopped
        {
            miningAnimation.speed /= miningSpeedMultiplier;
            Debug.Log("mining speed after meditation: " + miningAnimation.speed);
            //if ore was automatically mined when meditation stopped, we want to reset previous mine speed and set new one
            if (isProcessed == true)
            {
                //Delete previous mining speed
                adjustMiningSpeedNegative();
                //Recalculate mining speed and set new one
                calculateMiningSpeed();
            }
        }
        
    }

    public void collectMinedOre()
    {
        rockResourceCounter.AddToCounter((int)cost);
        rshardResourceCounter.AddToCounter((int)costShard);
    }

    public void adjustMiningSpeedCounter()
    {
        rockResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
        rshardResourceCounter.AddToMineSpeedCounter((float)miningShardSpeed);
    }

    public void adjustMiningSpeedNegative()
    {
        float negativeMiningSpeed = - miningSpeed;
        float negativeShardMiningSpeed = -miningShardSpeed;
        rockResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
        rshardResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
    }

    public IEnumerator hideHealthBar()
    {
        timerStarted = true;
        timeElapsedSinceStart = 0;
        while (timeElapsedSinceStart < 3)
        {
            yield return new WaitForSeconds(1);
            timeElapsedSinceStart++;
        }
        timerStarted = false;
        if (isProcessed == false) { transform.Find("HealthBarCanvas").gameObject.SetActive(false); }
        
    }
}
