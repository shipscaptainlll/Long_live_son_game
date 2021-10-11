using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IResource;

public class Earth : MonoBehaviour, IResource
{
    public AppleResourceCounter appleResourceCounter;
    public ASeedsResourceCounter aSeedsResourceCounter;
    public CarrotResourceCounter carrotResourceCounter;
    public CSeedsResourceCounter cSeedsResourceCounter;
    public float miningSpeed = 0f;
    public float neededResourcesSpeed = 0f;
    public float miningShardSpeed = 0f;
    public float neededResourcesSpeed2 = 0f;
    public Animator miningAnimation;
    public Bucket_earth_water bucket_Earth_Water;
    public Bucket_earth_manure bucket_Earth_Manure;
    public Scissors_tree scissors_Tree;
    private float aminingSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int applesInEarth;
    public int aSeedsPerEarth;
    public int carrotsInEarth;
    public int cSeedsPerEarth;

    public float fertilizingOSpeed;
    public float fertilizingSpeed;
    public float wateringOSpeed;
    public float wateringSpeed;
    public float growingCOSpeed;
    public float growingCSpeed;
    public float growingAOSpeed;
    public float growingASpeed;
    public float harvestingOSpeed;
    public float harvestingSpeed;

    public bool isGatheringApples;
    public bool isGatheringCarrots;

    public string Type
    {
        get
        {
            return "Earth";
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
    public float neededResources;
    public float neededResources2;

    void Start()
    {
        isGatheringCarrots = false;
        isGatheringApples = true;
        objectMaxHealth = 100;
        applesInEarth = 14;
        carrotsInEarth = 9;
        aSeedsPerEarth = 10;
        cSeedsPerEarth = 10;
        transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        calculateOreCost();
        transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
        bucket_Earth_Manure = transform.Find("Bucket with manure").GetComponent<Bucket_earth_manure>();
    }

    public void calculateOreCost()
    {
        cost = applesInEarth;
        costShard = carrotsInEarth;
        neededResources = aSeedsPerEarth;
        neededResources2 = cSeedsPerEarth;
    }

    public void calculateMiningSpeed()
    {
        gameObject.transform.Find("Bucket with manure").gameObject.SetActive(true);
        gameObject.transform.Find("Bucket with water").gameObject.SetActive(true);
        gameObject.transform.Find("Scissors").gameObject.SetActive(true);
        fertilizingOSpeed = 2.5f;
        fertilizingSpeed = gameObject.transform.Find("Bucket with manure").GetComponent<Animator>().GetFloat("speedAMining");
        wateringOSpeed = 1;
        wateringSpeed = gameObject.transform.Find("Bucket with water").GetComponent<Animator>().GetFloat("speedAMining");
        growingCOSpeed = 5;
        growingCSpeed = 1;
        growingAOSpeed = 8;
        growingASpeed = 1;
        harvestingOSpeed = 4.5f;
        harvestingSpeed = gameObject.transform.Find("Scissors").GetComponent<Animator>().GetFloat("speedAMining");
        
        gameObject.transform.Find("Bucket with water").gameObject.SetActive(false);
        gameObject.transform.Find("Scissors").gameObject.SetActive(false);

        miningSpeed = carrotsInEarth / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingCOSpeed / growingCSpeed + harvestingOSpeed / harvestingSpeed); 
        miningShardSpeed = applesInEarth / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingAOSpeed / growingASpeed + harvestingOSpeed / harvestingSpeed);
        neededResourcesSpeed = aSeedsPerEarth / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingCOSpeed / growingCSpeed + harvestingOSpeed / harvestingSpeed);
        neededResourcesSpeed2 = cSeedsPerEarth / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingAOSpeed / growingASpeed + harvestingOSpeed / harvestingSpeed);
        //neededResourcesSpeed = aSeedsPerEarth * (damagePerHit * aminingSpeed / objectMaxHealth);
        //miningShardSpeed = carrotsInEarth * (damagePerHit * aminingSpeed / objectMaxHealth);
        //neededResourcesSpeed2 = cSeedsPerEarth * (damagePerHit * aminingSpeed / objectMaxHealth);
        adjustMiningSpeedCounter();
    }
    


    public void collectMinedOre()
    {
        StartCoroutine(useSeeds());
        StartCoroutine(fertilizeTree());
        
        Debug.Log("Once again");
    }
    public void startWatering()
    {
        StartCoroutine(waterTree());
    }
    public void startGrowing()
    {
        StartCoroutine(growPlant());
    }
    public void startGathering()
    {
        StartCoroutine(gatherPlant());
    }
    public void startCollectingPlantToResources()
    {
        StartCoroutine(collectPlantToResources());
    }
    

    private IEnumerator fertilizeTree()
    {
        GameObject bucket_with_manure = gameObject.transform.Find("Bucket with manure").gameObject;
        bucket_with_manure.SetActive(true);
        bucket_with_manure.GetComponent<Animator>().Play("Fertilizing");

        yield return null;

    }
    
    public IEnumerator waterTree()
    {
        GameObject bucket_with_water = gameObject.transform.Find("Bucket with water").gameObject;
        bucket_with_water.SetActive(true);
        bucket_with_water.GetComponent<Animator>().Play("Watering");
        yield return null;

    }

    public IEnumerator growPlant()
    {
        
        if (isGatheringCarrots == true)
        {
            yield return new WaitForSeconds(growingCOSpeed);
            GameObject carrots = gameObject.transform.Find("Carrots").gameObject;
            carrots.SetActive(true);
        }
        if (isGatheringApples == true)
        {
            yield return new WaitForSeconds(growingAOSpeed);
            GameObject apples = gameObject.transform.Find("Apples").gameObject;
            apples.SetActive(true);
        }
        startGathering();

        //appleResourceCounter.AddToCounter((int)cost);
        //aSeedsResourceCounter.AddToCounter((int)neededResources);
        //carrotResourceCounter.AddToCounter((int)costShard);
        //cSeedsResourceCounter.AddToCounter((int)neededResources2);
    }
    public IEnumerator gatherPlant()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject scissors = gameObject.transform.Find("Scissors").gameObject;
        scissors.SetActive(true);
        GameObject scissors1 = gameObject.transform.Find("Scissors1").gameObject;
        scissors1.SetActive(true);
        scissors.GetComponent<Animator>().Play("Harvesting");
        scissors1.GetComponent<Animator>().Play("Harvesting1");
        yield return null;

        //appleResourceCounter.AddToCounter((int)cost);
        //aSeedsResourceCounter.AddToCounter((int)neededResources);
        //carrotResourceCounter.AddToCounter((int)costShard);
        //cSeedsResourceCounter.AddToCounter((int)neededResources2);
    }

    public IEnumerator collectPlantToResources()
    {
        if (isGatheringCarrots == true)
        {
            carrotResourceCounter.AddToCounter((int)costShard);
        }
        if (isGatheringApples == true)
        {
            appleResourceCounter.AddToCounter((int)cost);
        }
        //appleResourceCounter.AddToCounter((int)cost);
        //aSeedsResourceCounter.AddToCounter((int)neededResources);
        
        //cSeedsResourceCounter.AddToCounter((int)neededResources2);
        yield return null;
    }

    public IEnumerator useSeeds()
    {
        if (isGatheringCarrots == true)
        {
            cSeedsResourceCounter.AddToCounter((int)-neededResources); 
        }
        if (isGatheringApples == true)
        {
            aSeedsResourceCounter.AddToCounter((int)-neededResources2);
        }
        //appleResourceCounter.AddToCounter((int)cost);
        //aSeedsResourceCounter.AddToCounter((int)neededResources);

        //cSeedsResourceCounter.AddToCounter((int)neededResources2);
        yield return null;
    }

    public void adjustMiningSpeedCounter()
    {
        if (isGatheringCarrots == true)
        {
            carrotResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
            cSeedsResourceCounter.AddToMineSpeedCounter((float)-neededResourcesSpeed2);
            
        }
        if (isGatheringApples == true)
        {
            appleResourceCounter.AddToMineSpeedCounter((float)miningShardSpeed);
            aSeedsResourceCounter.AddToMineSpeedCounter((float)-neededResourcesSpeed);

        }
        //appleResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
        //
        
        //

    }

    public void adjustMiningSpeedNegative()
    {
        if (isGatheringCarrots == true)
        {
            float negativeMiningSpeed = -miningSpeed;
            carrotResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
            cSeedsResourceCounter.AddToMineSpeedCounter((float)neededResourcesSpeed2);

        }
        if (isGatheringApples == true)
        {
            float negativeShardMiningSpeed = -miningShardSpeed;
            appleResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
            aSeedsResourceCounter.AddToMineSpeedCounter((float)neededResourcesSpeed);
        }
        
        //float negativeShardMiningSpeed = -miningShardSpeed;
        //float negativeNeededResourcesSpeed = neededResourcesSpeed;
        //float negativeneededResourcesSpeed2 = neededResourcesSpeed2;
        //appleResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
        
        //aSeedsResourceCounter.AddToMineSpeedCounter((float)negativeNeededResourcesSpeed);
        //cSeedsResourceCounter.AddToMineSpeedCounter((float)negativeneededResourcesSpeed2);
    }
}
