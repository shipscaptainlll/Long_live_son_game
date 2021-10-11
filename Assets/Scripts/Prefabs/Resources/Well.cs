using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Well : MonoBehaviour, IResource
{
    public WaterResourceCounter waterResourceCounter;
    public RShardResourceCounter rshardResourceCounter;
    public int toolLevel;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    public Animator miningAnimation;
    public Bucket_well bucket_well;
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
    public void refreshToolParameters(float speed, int lvl)
    {

        miningAnimation.speed = speed;

        toolLevel = lvl;
        if (isProcessed == true)
        {
            adjustMiningSpeedNegative();
            calculateMiningSpeed();
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
