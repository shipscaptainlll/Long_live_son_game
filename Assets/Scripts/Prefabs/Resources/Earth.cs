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
    [SerializeField] ManureResourceCounter ManureResourceCounter;
    [SerializeField] MeditationPanel MeditationPanel;
    public InteractionController InteractionController; //Interaction controller that notifies earth tile script when scissors, bucket or seeds were used on it
    public Animator miningAnimation;
    Bucket_earth_water bucket_Earth_Water;
    Bucket_earth_manure BucketEarthManure;
    GrowingCarrots GrowingCarrots;
    GrowingApples GrowingApples;
    GameObject Carrots;
    GameObject Apples;
    public Scissors_tree scissors_Tree;
    public float miningSpeed = 0f;
    public float neededResourcesSpeed = 0f;
    public float miningShardSpeed = 0f;
    public float neededResourcesSpeed2 = 0f;
    private float manureSpendingSpeedCarrots = 0f;
    private float manureSpendingSpeedApples = 0f;
    [SerializeField] SeedsToolsPanel SeedsToolsPanel;
    private float aminingSpeed;
    public int damagePerHit;
    public int objectMaxHealth;
    public int applesPerCycle;
    public int aSeedsPerEarth;
    public int carrotsPerCycle;
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
    public bool HasCarrotSeeds;
    public bool HasAppleSeeds;
    public bool ScissorsActivated;
    public bool BucketActivated;
    private String TypeOfProduction;
    private bool productionTypeIsChosen;
    private bool readyToGrow;

    private int appleSeedsPerCycle;
    private int carrotSeedsPerCycle;
    private int manurePerCycle;
    bool adjustmentsAreReady;
    bool suppliesAreReady;
    bool growingInProgress;
    string productionTypeInProgress;
    byte meditationModeMultiplier;

    private event Action AdjustmentsChanged = delegate { };
    void Start()
    {
        Carrots = gameObject.transform.Find("Carrots").gameObject;
        Carrots.GetComponent<Animator>().speed = 0.1f;
        Apples = gameObject.transform.Find("Apples").gameObject;
        Apples.GetComponent<Animator>().speed = 0.1f;
        InteractionController.ScissorsContactedWithEarth += ActivateOrDeactivateScissors;
        InteractionController.BucketContactedWithEarth += ActivateOrDeactivateBucket;
        HasCarrotSeeds = false;
        HasAppleSeeds = false;
        ScissorsActivated = false;
        BucketActivated = false;
        isGatheringCarrots = false;
        isGatheringApples = true;
        productionTypeIsChosen = false;
        readyToGrow = false;
        growingInProgress = false;
        productionTypeInProgress = "default";
        objectMaxHealth = 100;
        applesPerCycle = 14;
        carrotsPerCycle = 9;
        aSeedsPerEarth = 10;
        cSeedsPerEarth = 10;
        carrotSeedsPerCycle = 10;
        appleSeedsPerCycle = 10;
        manurePerCycle = 1;
        meditationModeMultiplier = 1;
        transform.GetComponent<Health>().currentHealth = objectMaxHealth;
        isProcessed = false;
        //calculateOreCost();
        //transform.Find("HealthBarCanvas").GetComponent<HealthBar>().OreMined += collectMinedOre;
        SeedsToolsPanel.ProductionTypeChosen += ManageProductionState;
        AdjustmentsChanged += TryStartGrowing;
        ManureResourceCounter.ManureCollectedCounter += TryStartGrowing;
        cSeedsResourceCounter.cSeedsCollected += TryStartGrowing;
        aSeedsResourceCounter.AppleSeedsCollected += TryStartGrowing;
        BucketEarthManure = transform.Find("Bucket with manure").GetComponent<Bucket_earth_manure>();
        BucketEarthManure.FinishedFertilizingEarth += WaterTree;
        bucket_Earth_Water = transform.Find("Bucket with water").GetComponent<Bucket_earth_water>();
        bucket_Earth_Water.FinishedWateringEarth += GrowPlant;
        GrowingCarrots = transform.Find("Carrots").GetComponent<GrowingCarrots>();
        GrowingCarrots.FinishedGrowingPlant += GatherPlant;
        GrowingApples = transform.Find("Apples").GetComponent<GrowingApples>();
        GrowingApples.FinishedGrowingPlant += GatherPlant;
        scissors_Tree = transform.Find("Scissors").GetComponent<Scissors_tree>();
        scissors_Tree.PlantHarvested += RestartGrowing;
        MeditationPanel.startedMeditating += EnterOrLeaveMeditationMode;
    }

    //Activates scissors for this pile of earth
    private void ActivateOrDeactivateScissors(GameObject earth, string toActivateOrDeactivate)
    {
        if (gameObject.transform.parent.gameObject == earth.transform.parent.parent.gameObject)
        {
            ScissorsActivated = toActivateOrDeactivate == "Activate" ? true : false;
            NotifyAboutAdjustmentsChange();
        }
    }

    private void ActivateOrDeactivateBucket(GameObject earth, string toActivateOrDeactivate)
    {
        if (gameObject.transform.parent.gameObject == earth.transform.parent.parent.gameObject)
        {
            BucketActivated = toActivateOrDeactivate == "Activate" ? true : false;
            NotifyAboutAdjustmentsChange();
        }
    }

    private void ManageProductionState(GameObject ChosenEarth, String TypeOfProduction)
    {
        if (gameObject.transform.parent.gameObject == ChosenEarth.transform.parent.parent.gameObject)
        {
            if (TypeOfProduction == "CarrotSeeds" || TypeOfProduction == "AppleSeeds")
            {
                productionTypeIsChosen = true;
                this.TypeOfProduction = TypeOfProduction;
                NotifyAboutAdjustmentsChange();
            } else
            {
                productionTypeIsChosen = false;
                this.TypeOfProduction = "StopProduction";
                NotifyAboutAdjustmentsChange();
            }
        } else { }
    }

    private void NotifyAboutAdjustmentsChange()
    {
        if (AdjustmentsChanged != null)
        {
            AdjustmentsChanged();
        }
    }
    
    private void TryStartGrowing()
    {
        CheckAdjustmentsReadiness();
        CheckSuppliesReadiness();
        if (growingInProgress)
        {
            if (!adjustmentsAreReady || CheckIfProductionTypeChanged())
            {
                StopGrowingProcess();
            }
        }
        if (adjustmentsAreReady && suppliesAreReady && !growingInProgress)
        {
            StartGrowingProcess();
        }
    }

    private void StopGrowingProcess()
    {
        growingInProgress = false;
        adjustMiningSpeedNegative();
        productionTypeInProgress = "default";
        GameObject bucket_with_manure = gameObject.transform.Find("Bucket with manure").gameObject;
        bucket_with_manure.SetActive(false);
        bucket_with_manure.GetComponent<Animator>().Play("New State");
        GameObject bucket_with_water = gameObject.transform.Find("Bucket with water").gameObject;
        bucket_with_water.SetActive(false);
        bucket_with_water.GetComponent<Animator>().Play("New State");
        GameObject carrots = gameObject.transform.Find("Carrots").gameObject;
        carrots.SetActive(false);
        carrots.GetComponent<Animator>().Play("New State");
        GameObject apples = gameObject.transform.Find("Apples").gameObject;
        apples.SetActive(false);
        apples.GetComponent<Animator>().Play("New State");
        GameObject scissors = gameObject.transform.Find("Scissors").gameObject;
        scissors.SetActive(false);
        GameObject scissors1 = gameObject.transform.Find("Scissors1").gameObject;
        scissors1.SetActive(false);
        scissors.GetComponent<Animator>().Play("New State");
        scissors1.GetComponent<Animator>().Play("New State");
        Debug.Log("Growing has stopped");
    }

    private void StartGrowingProcess()
    {
        growingInProgress = true;
        productionTypeInProgress = TypeOfProduction;
        FertilizeTree();
        TakeResourcesForCycle();
        TakeCareOfMiningSpeeds();
    }

    private void RestartGrowing()
    {
        AddPlantsToCargo();
        StopGrowingProcess();
        TryStartGrowing();
    }

    private void CheckAdjustmentsReadiness()
    {
        readyToGrow = BucketActivated && ScissorsActivated && productionTypeIsChosen;
        adjustmentsAreReady = readyToGrow;
    }

    private bool CheckIfProductionTypeChanged()
    {
        return productionTypeInProgress != TypeOfProduction;
    }

    private void CheckSuppliesReadiness()
    {
        bool enoughManureForCycle = manurePerCycle <= ManureResourceCounter.count;
        bool enoughSeedsForCycle = false;
        if (TypeOfProduction == "CarrotSeeds")
        {
            enoughSeedsForCycle = carrotSeedsPerCycle <= cSeedsResourceCounter.count;
        } else if (TypeOfProduction == "AppleSeeds")
        {
            enoughSeedsForCycle = appleSeedsPerCycle <= aSeedsResourceCounter.count;
        }
        suppliesAreReady = enoughManureForCycle && enoughSeedsForCycle;
    }

    private void FertilizeTree()
    {
        GameObject bucket_with_manure = gameObject.transform.Find("Bucket with manure").gameObject;
        bucket_with_manure.SetActive(true);
        bucket_with_manure.GetComponent<Animator>().Play("Fertilizing");
    }

    private void WaterTree()
    {
        GameObject bucket_with_water = gameObject.transform.Find("Bucket with water").gameObject;
        bucket_with_water.SetActive(true);
        bucket_with_water.GetComponent<Animator>().Play("Watering");
    }

    private void GrowPlant()
    {
        if (TypeOfProduction == "CarrotSeeds")
        {
            Carrots.SetActive(true);
            Carrots.GetComponent<Animator>().Play("GrowingCarrots");
        }
        if (TypeOfProduction == "AppleSeeds")
        {
            Apples.SetActive(true);
            Apples.GetComponent<Animator>().Play("GrowingApples");
        }
    }

    private void GatherPlant()
    {
        GameObject scissors = gameObject.transform.Find("Scissors").gameObject;
        scissors.SetActive(true);
        GameObject scissors1 = gameObject.transform.Find("Scissors1").gameObject;
        scissors1.SetActive(true);
        scissors.GetComponent<Animator>().Play("Harvesting");
        scissors1.GetComponent<Animator>().Play("Harvesting1");
    }

    private void AddPlantsToCargo()
    {
        if (TypeOfProduction == "CarrotSeeds")
        {
            carrotResourceCounter.AddToCounter((int)carrotsPerCycle);
        }
        if (TypeOfProduction == "AppleSeeds")
        {
            appleResourceCounter.AddToCounter((int)applesPerCycle);
        }
    }

    private void TakeResourcesForCycle()
    {
        if (TypeOfProduction == "CarrotSeeds")
        {
            cSeedsResourceCounter.AddToCounter((int)-carrotSeedsPerCycle);
        }
        if (TypeOfProduction == "AppleSeeds")
        {
            aSeedsResourceCounter.AddToCounter((int)-appleSeedsPerCycle);
        }
    }
    
    void calculateOreCost()
    {
        cost = applesPerCycle;
        costShard = carrotsPerCycle;
        neededResources = aSeedsPerEarth;
        neededResources2 = cSeedsPerEarth;
    }

    void TakeCareOfMiningSpeeds()
    {
        calculateMiningSpeed();
        adjustMiningSpeedCounter();
    }

    void calculateMiningSpeed()
    {
        
        fertilizingOSpeed = 2.5f;
        fertilizingSpeed = 1;
        wateringOSpeed = 1;
        wateringSpeed = 1;
        growingCOSpeed = 0.83f;
        growingCSpeed = 0.1f;
        growingAOSpeed = 0.916f;
        growingASpeed = 0.1f;
        harvestingOSpeed = 4.5f;

        harvestingSpeed = 1;
        
        
        miningSpeed = meditationModeMultiplier * carrotsPerCycle / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingCOSpeed / growingCSpeed + harvestingOSpeed / harvestingSpeed); 
        miningShardSpeed = meditationModeMultiplier * applesPerCycle / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingAOSpeed / growingASpeed + harvestingOSpeed / harvestingSpeed);
        neededResourcesSpeed = meditationModeMultiplier * carrotSeedsPerCycle / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingCOSpeed / growingCSpeed + harvestingOSpeed / harvestingSpeed);
        neededResourcesSpeed2 = meditationModeMultiplier * appleSeedsPerCycle / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingAOSpeed / growingASpeed + harvestingOSpeed / harvestingSpeed);
        manureSpendingSpeedCarrots = meditationModeMultiplier * manurePerCycle / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingCOSpeed / growingCSpeed + harvestingOSpeed / harvestingSpeed);
        manureSpendingSpeedApples = meditationModeMultiplier * manurePerCycle / (fertilizingOSpeed / fertilizingSpeed + wateringOSpeed / wateringSpeed + growingAOSpeed / growingASpeed + harvestingOSpeed / harvestingSpeed);
    }

    void adjustMiningSpeedCounter()
    {
        if (TypeOfProduction == "CarrotSeeds")
        {
            carrotResourceCounter.AddToMineSpeedCounter((float)miningSpeed);
            cSeedsResourceCounter.AddToMineSpeedCounter((float)-neededResourcesSpeed2);
            ManureResourceCounter.AddToMineSpeedCounter((float)-manureSpendingSpeedCarrots);
        }
        if (TypeOfProduction == "AppleSeeds")
        {
            appleResourceCounter.AddToMineSpeedCounter((float)miningShardSpeed);
            aSeedsResourceCounter.AddToMineSpeedCounter((float)-neededResourcesSpeed);
            ManureResourceCounter.AddToMineSpeedCounter((float)-manureSpendingSpeedApples);
        }
    }

    void adjustMiningSpeedNegative()
    {
        if (productionTypeInProgress == "CarrotSeeds")
        {
            float negativeMiningSpeed = -miningSpeed;
            carrotResourceCounter.AddToMineSpeedCounter((float)negativeMiningSpeed);
            cSeedsResourceCounter.AddToMineSpeedCounter((float)neededResourcesSpeed2);
            ManureResourceCounter.AddToMineSpeedCounter((float)manureSpendingSpeedCarrots);
        }
        if (productionTypeInProgress == "AppleSeeds")
        {
            float negativeShardMiningSpeed = -miningShardSpeed;
            appleResourceCounter.AddToMineSpeedCounter((float)negativeShardMiningSpeed);
            aSeedsResourceCounter.AddToMineSpeedCounter((float)neededResourcesSpeed);
            ManureResourceCounter.AddToMineSpeedCounter((float)manureSpendingSpeedApples);
        }
    }

    void EnterOrLeaveMeditationMode(byte miningSpeedMultiplier, bool isMeditating)
    {
        
            ChangeAnimationsSpeed(miningSpeedMultiplier, isMeditating);
            ChangeAnimationsSpeedCounters(miningSpeedMultiplier, isMeditating);
    }

    void ChangeAnimationsSpeed(byte miningSpeedMultiplier, bool isMeditating)
    {
        if (isMeditating == true)
        {
            GameObject bucket_with_manure = gameObject.transform.Find("Bucket with manure").gameObject;
            GameObject bucket_with_water = gameObject.transform.Find("Bucket with water").gameObject;
            GameObject carrots = gameObject.transform.Find("Carrots").gameObject;
            GameObject apples = gameObject.transform.Find("Apples").gameObject;
            GameObject scissors = gameObject.transform.Find("Scissors").gameObject;
            GameObject scissors1 = gameObject.transform.Find("Scissors1").gameObject;
            bucket_with_manure.GetComponent<Animator>().speed *= miningSpeedMultiplier;
            bucket_with_water.GetComponent<Animator>().speed *= miningSpeedMultiplier;
            carrots.GetComponent<Animator>().speed *= miningSpeedMultiplier;
            apples.GetComponent<Animator>().speed *= miningSpeedMultiplier;
            scissors.GetComponent<Animator>().speed *= miningSpeedMultiplier;
            scissors1.GetComponent<Animator>().speed *= miningSpeedMultiplier;
        } else
        {
            GameObject bucket_with_manure = gameObject.transform.Find("Bucket with manure").gameObject;
            GameObject bucket_with_water = gameObject.transform.Find("Bucket with water").gameObject;
            GameObject carrots = gameObject.transform.Find("Carrots").gameObject;
            GameObject apples = gameObject.transform.Find("Apples").gameObject;
            GameObject scissors = gameObject.transform.Find("Scissors").gameObject;
            GameObject scissors1 = gameObject.transform.Find("Scissors1").gameObject;
            bucket_with_manure.GetComponent<Animator>().speed /= miningSpeedMultiplier;
            bucket_with_water.GetComponent<Animator>().speed /= miningSpeedMultiplier;
            carrots.GetComponent<Animator>().speed /= miningSpeedMultiplier;
            apples.GetComponent<Animator>().speed /= miningSpeedMultiplier;
            scissors.GetComponent<Animator>().speed /= miningSpeedMultiplier;
            scissors1.GetComponent<Animator>().speed /= miningSpeedMultiplier;
        }
        
    }

    void ChangeAnimationsSpeedCounters(byte miningSpeedMultiplier, bool isMeditating)
    {
        if (isMeditating)
        {
            adjustMiningSpeedNegative();
            meditationModeMultiplier = miningSpeedMultiplier;
            TakeCareOfMiningSpeeds();
        } else
        {
            adjustMiningSpeedNegative();
            meditationModeMultiplier = 1;
            TakeCareOfMiningSpeeds();
        }
        
    }
}
