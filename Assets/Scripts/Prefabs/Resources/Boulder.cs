using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Boulder : MonoBehaviour, IResource
{
    public RockResourceCounter rockResourceCounter;
    public RShardResourceCounter rshardResourceCounter;
    public int toolLevel;
    public bool isProcessedManually = false;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    public Animator miningAnimation;
    public PickAxe pickAxe;
    private float aminingSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int stonesInBoulder;
    public int shardsInBoulder;

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

    public bool isProcessed;
    public float cost;
    public float costShard;
    public UBPickAxe uBPickAxe;

    public bool timerStarted = false;
    public int timeElapsedSinceStart;

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
