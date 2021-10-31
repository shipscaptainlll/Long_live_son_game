using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public MouseRotation camera;
    public GrassResourceCounter grassResourceCounter;
    public InputController inputController;
    [SerializeField]
    private QuickAccessController quickAccessController;

    //pickaxetoolpanel
    public PickAxeToolsPanel pickAxeToolsPanel;
    public event Action<int> contactedBoulder = delegate { };

    //scissorstoolpanel
    public ScissorsToolsPanel scissorsToolsPanel;
    public event Action<string> contactedMoss = delegate { };
    public event Action<GameObject, string> ScissorsContactedWithEarth = delegate { };

    //buckettoolpanel
    public BucketToolsPanel bucketToolsPanel;
    public event Action<int> contactedWell = delegate { };
    public event Action<GameObject, string> BucketContactedWithEarth = delegate { };

    //axetoolpanel
    public AxeToolsPanel axeToolsPanel;
    public event Action<int> contactedTree = delegate { };

    //grass tent
    public FUPGrassTent fUPGrassTent;

    //Manually mine
    public Animator characterAnimator;
    public GameObject lastContactedObject;
    public GameObject axe;
    public GameObject pickAxe;
    private bool isMiningSomething;

    //Quests
    public event Action startedRockoreAutomining = delegate { }; //Send notification that started ore auto mining to fourth quest
    public event Action startedWaterAutomining = delegate { }; //Send notification that started water auto mining to seventh quest
    public event Action startedGrassAutomining = delegate { };
    public event Action SeedsAdded = delegate { };
    public event Action BucketAdded = delegate { };
    public event Action ScissorsAdded = delegate { };

    //Boulder blast
    [SerializeField] GameObject BoulderBlastPanel;
    // Start is called before the first frame update
    void Start()
    {
        //inputController.somethingDetected += InteractWithObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithObject()
    {
        GameObject objectDetected = camera.detectObject();

        if (objectDetected.GetComponent<IResource>() != null && objectDetected.GetComponent<IResource>().Type == "Boulder")
        {
            BoulderInteract(objectDetected);
        } else if (objectDetected.transform.parent.parent.GetComponent<IResource>() != null && objectDetected.transform.parent.parent.GetComponent<IResource>().Type == "Tree")
        {
            TreeInteract(objectDetected);
        } else if (objectDetected.transform.parent.parent.GetComponent<IResource>() != null && objectDetected.transform.parent.parent.GetComponent<IResource>().Type == "Well")
        {

            WellInteract(objectDetected);
        } else if (objectDetected.transform.parent.GetComponent<IResource>() != null && objectDetected.transform.parent.GetComponent<IResource>().Type == "Moss")
        {
            Debug.Log(objectDetected);
            MossInteract(objectDetected);
        } else if (objectDetected.transform.parent.GetComponent<IResource>() != null && objectDetected.transform.parent.GetComponent<IResource>().Type == "Earth")
        {
            
            if (CheckIfToolPanelOpened(scissorsToolsPanel.gameObject))
            {
                UseScissorsOnEarth(objectDetected);
            } 
            else if (CheckIfToolPanelOpened(bucketToolsPanel.gameObject))
            {
                UseBucketOnEarth(objectDetected);
            } else if (CheckIfToolPanelOpened(bucketToolsPanel.gameObject))
            {
                EarthInteract(objectDetected);
            }

        } else if (objectDetected.transform.parent.GetComponent<IResource>() != null && objectDetected.transform.parent.GetComponent<IResource>().Type == "FoodBowl")
        {
            FoodBowlInteract(objectDetected);
        } else if (objectDetected.GetComponent<Upgrade_Opener>().Type == "Upgrade opener")
        {
            UpgradeOpenerInteract(objectDetected);
        }

        //Debug.Log(objectDetected);
    }

    private void BoulderInteract(GameObject boulder)
    {
        if (boulder.GetComponent<Boulder>().isProcessed == false && pickAxeToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1 && pickAxeToolsPanel.toolsUsed < pickAxeToolsPanel.toolsCount)
        {
            boulder.transform.Find("HealthBarCanvas").gameObject.SetActive(true);
            GameObject pickAxe = boulder.transform.Find("Pickaxe").gameObject;
            pickAxe.SetActive(true);
            boulder.GetComponent<Boulder>().isProcessed = true;
            pickAxe.GetComponent<Animator>().Play("AMining");
            boulder.GetComponent<Boulder>().calculateMiningSpeed();
            //??
            if (contactedBoulder != null)
            {
                contactedBoulder(1);
            }
            //Send notification to fourth quest, that rock ore auto mining started
            if (startedRockoreAutomining != null)
            {
                startedRockoreAutomining();
            }
            
            //Debug.Log("Activated");
        }

        else if (boulder.GetComponent<Boulder>().isProcessed == true && pickAxeToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject pickAxe = boulder.transform.Find("Pickaxe").gameObject;
            boulder.GetComponent<Boulder>().isProcessed = false;
            boulder.GetComponent<Boulder>().adjustMiningSpeedNegative();
            pickAxe.GetComponent<Animator>().Play("NewState");
            pickAxe.SetActive(false);
            boulder.transform.Find("HealthBarCanvas").gameObject.SetActive(false);
            contactedBoulder(0);
        }
    }

    private void TreeInteract(GameObject tree)
    {
        if (tree.transform.parent.parent.GetComponent<Tree>().isProcessed == false && axeToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1 && axeToolsPanel.toolsUsed < axeToolsPanel.toolsCount)
        {
            tree.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            GameObject axe = tree.transform.parent.parent.Find("Axe").gameObject;
            axe.SetActive(true);
            tree.transform.parent.parent.GetComponent<Tree>().isProcessed = true;
            axe.GetComponent<Animator>().Play("AMining");
            tree.transform.parent.parent.GetComponent<Tree>().calculateMiningSpeed();
            contactedTree(1);
            //Debug.Log("Activated");
        }

        else if (tree.transform.parent.parent.GetComponent<Tree>().isProcessed == true && axeToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject axe = tree.transform.parent.parent.Find("Axe").gameObject;
            tree.transform.parent.parent.GetComponent<Tree>().isProcessed = false;
            tree.transform.parent.parent.GetComponent<Tree>().adjustMiningSpeedNegative();
            axe.GetComponent<Animator>().Play("NewState");
            axe.SetActive(false);
            tree.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(false);
            contactedTree(0);
        }
    }

    private void WellInteract(GameObject well)
    {
        if (well.transform.parent.parent.GetComponent<Well>().isProcessed == false && bucketToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1 && bucketToolsPanel.toolsUsed < bucketToolsPanel.toolsCount)
        {
            well.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            well.transform.parent.parent.Find("HealthBarCanvas").gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            GameObject bucket_well = well.transform.parent.parent.Find("Bucket").gameObject;
            bucket_well.SetActive(true);
            well.transform.parent.parent.GetComponent<Well>().isProcessed = true;
            bucket_well.GetComponent<Animator>().Play("AMining");
            well.transform.parent.parent.GetComponent<Well>().calculateMiningSpeed();
            contactedWell(1);
            if (startedWaterAutomining != null)
            {
                startedWaterAutomining();
            }
            //Debug.Log("Activated");
        }

        else if (well.transform.parent.parent.GetComponent<Well>().isProcessed == true && bucketToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject bucket_well = well.transform.parent.parent.Find("Bucket").gameObject;
            well.transform.parent.parent.GetComponent<Well>().isProcessed = false;
            well.transform.parent.parent.GetComponent<Well>().adjustMiningSpeedNegative();
            bucket_well.GetComponent<Animator>().Play("NewState");
            bucket_well.SetActive(false);
            well.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(false);
            contactedWell(0);
        }
    }

    private void MossInteract(GameObject moss)
    {
        if (moss.transform.parent.GetComponent<Moss>().isProcessed == false && scissorsToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1 && scissorsToolsPanel.toolsUsed < scissorsToolsPanel.toolsCount && fUPGrassTent.isConstructed == true)
        {
            moss.transform.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            moss.transform.parent.Find("HealthBarCanvas").gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            GameObject scissors_Moss = moss.transform.parent.Find("Scissors").gameObject;
            GameObject scissors1_Moss = moss.transform.parent.Find("Scissors1").gameObject;
            scissors_Moss.SetActive(true);
            scissors1_Moss.SetActive(true);
            moss.transform.parent.GetComponent<Moss>().isProcessed = true;
            scissors_Moss.GetComponent<Animator>().Play("AMining");
            scissors1_Moss.GetComponent<Animator>().Play("AMining1");
            moss.transform.parent.GetComponent<Moss>().calculateMiningSpeed();
            contactedMoss("Activate");
            if (startedGrassAutomining != null)
            {
                startedGrassAutomining();
            }
            //Debug.Log("Activated");
        }

        else if (moss.transform.parent.GetComponent<Moss>().isProcessed == true && scissorsToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject scissors_Moss = moss.transform.parent.Find("Scissors").gameObject;
            GameObject scissors1_Moss = moss.transform.parent.Find("Scissors1").gameObject;
            moss.transform.parent.GetComponent<Moss>().isProcessed = false;
            moss.transform.parent.GetComponent<Moss>().adjustMiningSpeedNegative();
            scissors_Moss.GetComponent<Animator>().Play("NewState");
            scissors1_Moss.GetComponent<Animator>().Play("NewState");
            scissors_Moss.SetActive(false);
            scissors1_Moss.SetActive(false);
            moss.transform.parent.Find("HealthBarCanvas").gameObject.SetActive(false);
            contactedMoss("Deactivate");
        }
    }

    private void EarthInteract(GameObject earth)
    {
        if (earth.transform.parent.GetComponent<Earth>().isProcessed == false && 
            scissorsToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1 && scissorsToolsPanel.toolsUsed < scissorsToolsPanel.toolsCount)
        {
            earth.transform.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            earth.transform.parent.Find("HealthBarCanvas").gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            GameObject bucket_Water = earth.transform.parent.Find("Bucket with water").gameObject;
            GameObject bucket_Manure = earth.transform.parent.Find("Bucket with manure").gameObject;
            GameObject scissors_Moss = earth.transform.parent.Find("Scissors").gameObject;
            GameObject scissors1_Moss = earth.transform.parent.Find("Scissors1").gameObject;
            //bucket_Water.SetActive(true);
            bucket_Manure.SetActive(true);
            //scissors_Moss.SetActive(true);
            //scissors1_Moss.SetActive(true);
            earth.transform.parent.GetComponent<Earth>().isProcessed = true;
            //bucket_Water.GetComponent<Animator>().Play("Watering");
            //earth.transform.parent.GetComponent<Earth>().collectMinedOre();
            //bucket_Manure.GetComponent<Animator>().Play("Fertilizing");
            //scissors_Moss.GetComponent<Animator>().Play("AMining");
            //scissors1_Moss.GetComponent<Animator>().Play("AMining1");
            //earth.transform.parent.GetComponent<Earth>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }

        else if (earth.transform.parent.GetComponent<Earth>().isProcessed == true &&
            scissorsToolsPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject bucket_Water = earth.transform.parent.Find("Bucket with water").gameObject;
            GameObject bucket_Manure = earth.transform.parent.Find("Bucket with manure").gameObject;
            GameObject scissors_Moss = earth.transform.parent.Find("Scissors").gameObject;
            GameObject scissors1_Moss = earth.transform.parent.Find("Scissors1").gameObject;
            earth.transform.parent.GetComponent<Earth>().isProcessed = false;
            //earth.transform.parent.GetComponent<Earth>().adjustMiningSpeedNegative();
            //bucket_Water.GetComponent<Animator>().Play("NewState");
            bucket_Manure.GetComponent<Animator>().Play("NewState");
            scissors_Moss.GetComponent<Animator>().Play("NewState");
            scissors1_Moss.GetComponent<Animator>().Play("NewState");
            bucket_Water.SetActive(false);
            bucket_Manure.SetActive(false);
            scissors_Moss.SetActive(false);
            scissors1_Moss.SetActive(false);
            earth.transform.parent.Find("HealthBarCanvas").gameObject.SetActive(false);
        }
    }

    //Sends a notification to earth pile 
    private void UseScissorsOnEarth(GameObject earth)
    {
        if (!earth.transform.parent.GetComponent<Earth>().ScissorsActivated &&
            CheckIfToolPanelOpened(scissorsToolsPanel.gameObject) && scissorsToolsPanel.toolsUsed < scissorsToolsPanel.toolsCount)
        {
            string toActivateOrDeactivate = "Activate";
            NotifyScissorsToolCounter(earth, toActivateOrDeactivate);
            NotifyEighteenthQuestScissorsAdded();
        }

        else if (earth.transform.parent.GetComponent<Earth>().ScissorsActivated &&
            CheckIfToolPanelOpened(scissorsToolsPanel.gameObject))
        {
            string toActivateOrDeactivate = "Deactivate";
            NotifyScissorsToolCounter(earth, toActivateOrDeactivate);
        }
    }

    private void NotifyScissorsToolCounter(GameObject earth, string toActivateOrDeactivate)
    {
        if (ScissorsContactedWithEarth != null)
        {
            ScissorsContactedWithEarth(earth, toActivateOrDeactivate);
        }
    }

    private bool CheckIfToolPanelOpened(GameObject toolPanel)
    {
        return toolPanel.gameObject.GetComponent<CanvasGroup>().alpha == 1;
    }

    void NotifyEighteenthQuestScissorsAdded()
    {
        if (ScissorsAdded != null)
        {
            ScissorsAdded();
        }
    }

    private void UseBucketOnEarth(GameObject earth)
    {
        if (!earth.transform.parent.GetComponent<Earth>().BucketActivated &&
            CheckIfToolPanelOpened(bucketToolsPanel.gameObject) && bucketToolsPanel.toolsUsed < bucketToolsPanel.toolsCount)
        {
            string toActivateOrDeactivate = "Activate";
            NotifyBucketToolCounter(earth, toActivateOrDeactivate);
            NotifyEighteenthQuestBucketAdded();
        }

        else if (earth.transform.parent.GetComponent<Earth>().BucketActivated &&
            CheckIfToolPanelOpened(bucketToolsPanel.gameObject))
        {
            string toActivateOrDeactivate = "Deactivate";
            NotifyBucketToolCounter(earth, toActivateOrDeactivate);
        }
    }

    private void NotifyBucketToolCounter(GameObject earth, string toActivateOrDeactivate)
    {
        if (BucketContactedWithEarth != null)
        {
            BucketContactedWithEarth(earth, toActivateOrDeactivate);
        }
    }

    void NotifyEighteenthQuestBucketAdded()
    {
        if (BucketAdded != null)
        {
            BucketAdded();
        }
    }

    private void FoodBowlInteract(GameObject foodBowl)
    {
        FoodBowl foodBowlScript = foodBowl.transform.parent.GetComponent<FoodBowl>();
        if (foodBowl.transform.parent.GetComponent<FoodBowl>().isProcessed == false && grassResourceCounter.count >= foodBowlScript.costShard)
        {
            
            //foodBowl.transform.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            foodBowl.transform.parent.GetComponent<FoodBowl>().isProcessed = true;
            foodBowl.transform.parent.GetComponent<FoodBowl>().produceManure();
            foodBowl.transform.parent.GetComponent<FoodBowl>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }

        else if (foodBowl.transform.parent.GetComponent<FoodBowl>().isProcessed == true)
        {
            foodBowl.transform.parent.GetComponent<FoodBowl>().isProcessed = false;
            foodBowl.transform.parent.GetComponent<FoodBowl>().adjustMiningSpeedNegative();
            //foodBowl.transform.Find("HealthBarCanvas").gameObject.SetActive(false);
        } else { }
    }

    private void UpgradeOpenerInteract(GameObject upgradeOpener)
    {
        if (upgradeOpener.GetComponent<Upgrade_Opener>().isOpened == false)
        {
            //foodBowl.transform.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            upgradeOpener.GetComponent<Upgrade_Opener>().isOpened = true;
            upgradeOpener.GetComponent<Upgrade_Opener>().OpenPanel();
            //Debug.Log("Activated");
        }

        else if (upgradeOpener.GetComponent<Upgrade_Opener>().isOpened == true)
        {
            upgradeOpener.GetComponent<Upgrade_Opener>().isOpened = false;
            upgradeOpener.GetComponent<Upgrade_Opener>().ClosePanel();
            //foodBowl.transform.Find("HealthBarCanvas").gameObject.SetActive(false);
        }
    }

    public void InteractManuallyWithObject()
    {
        GameObject objectDetected = camera.detectObject();
        //Debug.Log(objectDetected);
        //Debug.Log(objectDetected);
        if (objectDetected != null
            && objectDetected.GetComponent<IResource>() != null
            && objectDetected.GetComponent<IResource>().Type == "Boulder")
        {
            BoulderInteractManually(objectDetected);
        }
        else if (objectDetected != null
            && objectDetected.transform.parent.parent.GetComponent<IResource>() != null
            && objectDetected.transform.parent.parent.GetComponent<IResource>().Type == "Tree")
        {
            TreeInteractManually(objectDetected);
        }
        else if (objectDetected != null
            && objectDetected.transform.parent.GetComponent<IResource>() != null
            && objectDetected.transform.parent.GetComponent<IResource>().Type == "Earth"
            && quickAccessController.currentlyOpened == null
            && quickAccessController.QAPIsOpened == false
            //&& quickAccessController.transform.gameObject.GetComponent<CanvasGroup>().alpha == 0
            && quickAccessController.isPanelOpened == false
            )
        {
            OpenSeedChoosePanelFor(objectDetected);
        } else if (objectDetected != null
            && objectDetected.name == "WallDestroyable")
        {
            WallDestroy(objectDetected);
        }
    }

    public void BoulderInteractManually(GameObject boulder)
    {
        
            pickAxe.SetActive(true);
            lastContactedObject = boulder;
            boulder.transform.Find("HealthBarCanvas").gameObject.SetActive(true);
            boulder.GetComponent<Boulder>().isProcessedManually = true;
            characterAnimator.Play("Mining ore");
        isMiningSomething = true;
            //Debug.Log("Hello");
            //boulder.GetComponent<Boulder>().calculateMiningSpeed();
            //Debug.Log("Activated");

    }

    public void stopBoulderInteractManually()
    {
        lastContactedObject.GetComponent<Boulder>().isProcessedManually = false;
        lastContactedObject.GetComponent<Health>().ModifyHealth(-50);
        lastContactedObject.GetComponent<Boulder>().timeElapsedSinceStart = 0;
        if (lastContactedObject.GetComponent<Boulder>().timerStarted == false) { StartCoroutine(lastContactedObject.GetComponent<Boulder>().hideHealthBar()); }
        
        lastContactedObject = null;
        isMiningSomething = false;
        pickAxe.SetActive(false);
    }


    public void TreeInteractManually(GameObject tree)
    {
        if (tree.transform.parent.parent.GetComponent<Tree>().isProcessedManually == false)
        {
            axe.SetActive(true);
            lastContactedObject = tree;
            tree.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            tree.transform.parent.parent.GetComponent<Tree>().isProcessedManually = true;
            characterAnimator.Play("Chopping Tree");
            isMiningSomething = true;
            //Debug.Log("Hello tree");
            //boulder.GetComponent<Boulder>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }
    }

    public void stopTreeInteractManually()
    {
        lastContactedObject.transform.parent.parent.GetComponent<Tree>().isProcessedManually = false;
        lastContactedObject.transform.parent.parent.GetComponent<Health>().ModifyHealth(-50);
        lastContactedObject.transform.parent.parent.GetComponent<Tree>().timeElapsedSinceStart = 0;
        if (lastContactedObject.transform.parent.parent.GetComponent<Tree>().timerStarted == false) { StartCoroutine(lastContactedObject.transform.parent.parent.GetComponent<Tree>().hideHealthBar()); }

        lastContactedObject = null;
        isMiningSomething = false;
        axe.SetActive(false);
    }

    public bool IsMiningSomething
    {
        get { return this.isMiningSomething; }
    }

    private void OpenSeedChoosePanelFor(GameObject detectedEarth)
    {
        quickAccessController.OpenSeedChoosePanelFor(detectedEarth);
    }

    void WallDestroy(GameObject objectDetected)
    {
        if (CheckIfToolPanelOpened(BoulderBlastPanel.gameObject))
        {
            objectDetected.transform.parent.gameObject.SetActive(false);
            Debug.Log("Wall is destroyed");
        }
        
    }
}
