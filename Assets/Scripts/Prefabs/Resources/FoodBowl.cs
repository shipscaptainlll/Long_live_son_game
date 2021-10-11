using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class FoodBowl : MonoBehaviour, IResource
{
    public ManureResourceCounter manureResourceCounter;
    public GrassResourceCounter grassResourceCounter;
    public float miningSpeed = 0f;
    public float miningShardSpeed = 0f;
    private float aminingSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int manureInCow;
    public float grassPreCow;

    public string Type
    {
        get
        {
            return "FoodBowl";
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
    public bool isWaiting;

    void Start()
    {
        objectMaxHealth = 100;
        manureInCow = 1;
        grassPreCow = 100f;
        //transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        calculateOreCost();
        //transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
    }

    public void produceManure()
    {
        if (grassResourceCounter.count >= costShard)
        {
            StartCoroutine(CollectingManure());
        } else {
            isProcessed = false;
            adjustMiningSpeedNegative();
        }
        
    }

    private IEnumerator CollectingManure()
    {
        while (isProcessed == true && grassResourceCounter.count >= costShard) {
            Debug.Log("Hello world");
            collectMinedOre();
            yield return new WaitForSeconds(1);
        }
        if (isProcessed == true && grassResourceCounter.count <= costShard)
        {
            isProcessed = false;
            adjustMiningSpeedNegative();
        }
        
    }

    public void calculateOreCost()
    {
        cost = manureInCow;
        costShard = grassPreCow;
    }

    public void calculateMiningSpeed()
    {
        
        miningSpeed = 1 ;
        miningShardSpeed = 25;
        adjustMiningSpeedCounter();
    }
    
    public void collectMinedOre()
    {
        if (isProcessed == true)
        {
            manureResourceCounter.AddToCounter((int)cost);
            grassResourceCounter.AddToCounter((int)-costShard);
        }
       
        if (isProcessed == true && grassResourceCounter.count <= costShard)
        {
            isProcessed = false;
            adjustMiningSpeedNegative();
        }
    }

    public void adjustMiningSpeedCounter()
    {
        manureResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
        grassResourceCounter.AddToMineSpeedCounter((float)-miningShardSpeed);
    }

    public void adjustMiningSpeedNegative()
    {
        float negativeMiningSpeed = - miningSpeed;
        float negativeShardMiningSpeed = miningShardSpeed;
        manureResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
        grassResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
    }
}
