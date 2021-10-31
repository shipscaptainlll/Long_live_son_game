using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Tree : MonoBehaviour, IResource
{
    public LogResourceCounter treeResourceCounter; //Logs resource main counter script
    public RShardResourceCounter tshardResourceCounter; //Rock shards resource main counter script
    public MeditationPanel MeditationPanel; //Script that will change mining speed when character meditating
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
        //Subscribe on meditation event, when event happens, mining speed will be changed correspondigly to state(meditation started/stopped)
        MeditationPanel.startedMeditating += enterLeaveMeditationMode;
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

    //Refreshes tool parameters (speed of mining animation, level of this tool) and then recalculates mining speed
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

    //During meditation time flows faster, it speeds up/ or descres the mining process using miningMultiplier parameter
    public void enterLeaveMeditationMode(byte miningSpeedMultiplier, bool IsMeditatingStatus)
    {
        if (IsMeditatingStatus == true) //will be used when meditation started
        {
            miningAnimation.speed *= miningSpeedMultiplier;
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
