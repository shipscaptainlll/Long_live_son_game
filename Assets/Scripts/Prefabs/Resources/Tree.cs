using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Tree : MonoBehaviour, IResource
{
    public LogResourceCounter treeResourceCounter;
    public RShardResourceCounter tshardResourceCounter;
    public int toolLevel;
    public bool isProcessedManually = false;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    public Animator miningAnimation;
    public Axe axe;
    private float aminingSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int logsInTree;
    public float shardsInTree;

    public string Type
    {
        get
        {
            return "Tree";
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
    public UBAxe uBAxe;

    public bool timerStarted = false;
    public int timeElapsedSinceStart;
    void Start()
    {
        uBAxe.toolParametersRefreshed += refreshToolParameters;
        objectMaxHealth = 100;
        logsInTree = 14;
        shardsInTree = 1f;
        transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        calculateOreCost();
        transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
        axe = transform.Find("Axe").GetComponent<Axe>();
    }

    public void calculateOreCost()
    {
        cost = logsInTree;
        costShard = shardsInTree;
    }

    public void calculateMiningSpeed()
    {
        damagePerHit = axe.damagePerHit;
        miningSpeed = logsInTree*(damagePerHit * miningAnimation.speed / objectMaxHealth) ;
        miningShardSpeed = shardsInTree * (damagePerHit * miningAnimation.speed / objectMaxHealth);
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
        treeResourceCounter.AddToCounter((int)cost);
        tshardResourceCounter.AddToCounter((int)costShard);
    }

    public void adjustMiningSpeedCounter()
    {
        treeResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
        tshardResourceCounter.AddToMineSpeedCounter((float)miningShardSpeed);
    }

    public void adjustMiningSpeedNegative()
    {
        float negativeMiningSpeed = - miningSpeed;
        float negativeShardMiningSpeed = -miningShardSpeed;
        treeResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
        tshardResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
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
