using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Well : MonoBehaviour, IResource
{
    public WaterResourceCounter waterResourceCounter; //Water resource main counter script
    public RShardResourceCounter rshardResourceCounter; //Rock shards resource main counter script
    public int toolLevel;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    public Animator miningAnimation;
    public Bucket_well bucket_well;
    public MeditationPanel MeditationPanel; //Script that will change mining speed when character meditating
    private float aminingSpeed;
    private float aminingAOriginalSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int waterInBucket;
    public float shardsInBucket;

    public string Type
    {
        get
        {
            return "Well";
        }
    }

    public int MaxHealth
    {
        get
        {
            return 100;
        }
    }

    public bool isProcessed;
    public float cost;
    public float costShard;
    public UBBucket uBBucket;

    void Start()
    {
        uBBucket.toolParametersRefreshed += refreshToolParameters;
        objectMaxHealth = 100;
        waterInBucket = 1;
        shardsInBucket = 0.5f;
        aminingAOriginalSpeed = 5;
        transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        calculateOreCost();
        transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
        bucket_well = transform.Find("Bucket").GetComponent<Bucket_well>();
        //Subscribe on meditation event, when event happens, mining speed will be changed correspondigly to state(meditation started/stopped)
        MeditationPanel.startedMeditating += enterLeaveMeditationMode;
    }

    public void calculateOreCost()
    {
        cost = waterInBucket;
        costShard = shardsInBucket;
    }

    public void calculateMiningSpeed()
    {
        
        damagePerHit = bucket_well.damagePerHit;
        miningSpeed = waterInBucket*(damagePerHit * miningAnimation.speed / objectMaxHealth) / aminingAOriginalSpeed;
        miningShardSpeed = shardsInBucket * (damagePerHit * miningAnimation.speed / objectMaxHealth) / aminingAOriginalSpeed;
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
            //Debug.Log("mining speed before multiplying: " + miningAnimation.speed);
            miningAnimation.speed *= miningSpeedMultiplier;
            //Debug.Log("mining speed after multiplying: " + miningAnimation.speed);
            //if ore was automatically mined when meditation started, we want to reset previous mine speed and set new one
            if (isProcessed == true)
            {
                //Delete previous mining speed
                adjustMiningSpeedNegative();
                //Recalculate mining speed and set new one
                calculateMiningSpeed();
            }
        }
        else if (IsMeditatingStatus == false) //will be used when meditation stopped
        {
            miningAnimation.speed /= miningSpeedMultiplier;
            //Debug.Log("mining speed after meditation: " + miningAnimation.speed);
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
        waterResourceCounter.AddToCounter((int)cost);
        rshardResourceCounter.AddToCounter(costShard);
    }

    public void adjustMiningSpeedCounter()
    {
        waterResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
        rshardResourceCounter.AddToMineSpeedCounter((float)miningShardSpeed);
    }

    public void adjustMiningSpeedNegative()
    {
        float negativeMiningSpeed = - miningSpeed;
        float negativeShardMiningSpeed = -miningShardSpeed;
        waterResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
        rshardResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
    }
}
