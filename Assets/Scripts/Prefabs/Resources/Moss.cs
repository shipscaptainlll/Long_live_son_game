using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Moss : MonoBehaviour, IResource
{
    public GrassResourceCounter grassResourceCounter;
    public RShardResourceCounter rshardResourceCounter;
    public int toolLevel;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    public Animator miningAnimation;
    public Animator miningAnimationSecScissors;
    public Scissors_moss scissors_Moss;
    private float aminingSpeed;
    private float aminingAOriginalSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int grassInMoss;
    public float shardsInMoss;

    public string Type
    {
        get
        {
            return "Moss";
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
    public UBScissors uBScissors;

    void Start()
    {
        uBScissors.toolParametersRefreshed += refreshToolParameters;
        objectMaxHealth = 100;
        grassInMoss = 1;
        shardsInMoss = 0.08f;
        aminingAOriginalSpeed = 5;
        transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        calculateOreCost();
        transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
        scissors_Moss = transform.Find("Scissors").GetComponent<Scissors_moss>();
    }

    public void calculateOreCost()
    {
        cost = grassInMoss;
        costShard = shardsInMoss;
    }

    public void calculateMiningSpeed()
    {
        damagePerHit = scissors_Moss.damagePerHit;
        miningSpeed = grassInMoss * (damagePerHit * miningAnimation.speed / objectMaxHealth)  / 0.67f;
        miningShardSpeed = shardsInMoss * (damagePerHit * miningAnimation.speed / objectMaxHealth) / 0.67f;
        adjustMiningSpeedCounter();
    }

    public void refreshToolParameters(float speed, int lvl)
    {
        
        miningAnimation.speed = speed;
        miningAnimationSecScissors.speed = speed;
        Debug.Log(speed);

        toolLevel = lvl;
        if (isProcessed == true)
        {
            adjustMiningSpeedNegative();
            calculateMiningSpeed();
        }
    }

    public void collectMinedOre()
    {
        grassResourceCounter.AddToCounter((int)cost);
        rshardResourceCounter.AddToCounter(costShard);
    }

    public void adjustMiningSpeedCounter()
    {
        grassResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
        rshardResourceCounter.AddToMineSpeedCounter((float)miningShardSpeed);
    }

    public void adjustMiningSpeedNegative()
    {
        float negativeMiningSpeed = - miningSpeed;
        float negativeShardMiningSpeed = -miningShardSpeed;
        grassResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
        rshardResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
    }
}
