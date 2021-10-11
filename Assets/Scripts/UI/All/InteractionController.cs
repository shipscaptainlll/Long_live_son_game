using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public MouseRotation camera;
    public GrassResourceCounter grassResourceCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithObject()
    {
        GameObject objectDetected = camera.detectObject();
        //Debug.Log(objectDetected);
        //Debug.Log(objectDetected);
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

            MossInteract(objectDetected);
        } else if (objectDetected.transform.parent.GetComponent<IResource>() != null && objectDetected.transform.parent.GetComponent<IResource>().Type == "Earth")
        {

            EarthInteract(objectDetected);
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
        if (boulder.GetComponent<Boulder>().isProcessed == false)
        {
            boulder.transform.Find("HealthBarCanvas").gameObject.SetActive(true);
            GameObject pickAxe = boulder.transform.Find("Pickaxe").gameObject;
            pickAxe.SetActive(true);
            boulder.GetComponent<Boulder>().isProcessed = true;
            pickAxe.GetComponent<Animator>().Play("AMining");
            boulder.GetComponent<Boulder>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }

        else if (boulder.GetComponent<Boulder>().isProcessed == true)
        {
            GameObject pickAxe = boulder.transform.Find("Pickaxe").gameObject;
            boulder.GetComponent<Boulder>().isProcessed = false;
            boulder.GetComponent<Boulder>().adjustMiningSpeedNegative();
            pickAxe.GetComponent<Animator>().Play("NewState");
            pickAxe.SetActive(false);
            boulder.transform.Find("HealthBarCanvas").gameObject.SetActive(false);
        }
    }

    private void TreeInteract(GameObject tree)
    {
        if (tree.transform.parent.parent.GetComponent<Tree>().isProcessed == false)
        {
            tree.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            GameObject axe = tree.transform.parent.parent.Find("Axe").gameObject;
            axe.SetActive(true);
            tree.transform.parent.parent.GetComponent<Tree>().isProcessed = true;
            axe.GetComponent<Animator>().Play("AMining");
            tree.transform.parent.parent.GetComponent<Tree>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }

        else if (tree.transform.parent.parent.GetComponent<Tree>().isProcessed == true)
        {
            GameObject axe = tree.transform.parent.parent.Find("Axe").gameObject;
            tree.transform.parent.parent.GetComponent<Tree>().isProcessed = false;
            tree.transform.parent.parent.GetComponent<Tree>().adjustMiningSpeedNegative();
            axe.GetComponent<Animator>().Play("NewState");
            axe.SetActive(false);
            tree.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(false);
        }
    }

    private void WellInteract(GameObject well)
    {
        if (well.transform.parent.parent.GetComponent<Well>().isProcessed == false)
        {
            well.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(true);
            well.transform.parent.parent.Find("HealthBarCanvas").gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            GameObject bucket_well = well.transform.parent.parent.Find("Bucket").gameObject;
            bucket_well.SetActive(true);
            well.transform.parent.parent.GetComponent<Well>().isProcessed = true;
            bucket_well.GetComponent<Animator>().Play("AMining");
            well.transform.parent.parent.GetComponent<Well>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }

        else if (well.transform.parent.parent.GetComponent<Well>().isProcessed == true)
        {
            GameObject bucket_well = well.transform.parent.parent.Find("Bucket").gameObject;
            well.transform.parent.parent.GetComponent<Well>().isProcessed = false;
            well.transform.parent.parent.GetComponent<Well>().adjustMiningSpeedNegative();
            bucket_well.GetComponent<Animator>().Play("NewState");
            bucket_well.SetActive(false);
            well.transform.parent.parent.Find("HealthBarCanvas").gameObject.SetActive(false);
        }
    }

    private void MossInteract(GameObject moss)
    {
        if (moss.transform.parent.GetComponent<Moss>().isProcessed == false)
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
            //Debug.Log("Activated");
        }

        else if (moss.transform.parent.GetComponent<Moss>().isProcessed == true)
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
        }
    }

    private void EarthInteract(GameObject earth)
    {
        if (earth.transform.parent.GetComponent<Earth>().isProcessed == false)
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
            earth.transform.parent.GetComponent<Earth>().collectMinedOre();
            //bucket_Manure.GetComponent<Animator>().Play("Fertilizing");
            //scissors_Moss.GetComponent<Animator>().Play("AMining");
            //scissors1_Moss.GetComponent<Animator>().Play("AMining1");
            earth.transform.parent.GetComponent<Earth>().calculateMiningSpeed();
            //Debug.Log("Activated");
        }

        else if (earth.transform.parent.GetComponent<Earth>().isProcessed == true)
        {
            GameObject bucket_Water = earth.transform.parent.Find("Bucket with water").gameObject;
            GameObject bucket_Manure = earth.transform.parent.Find("Bucket with manure").gameObject;
            GameObject scissors_Moss = earth.transform.parent.Find("Scissors").gameObject;
            GameObject scissors1_Moss = earth.transform.parent.Find("Scissors1").gameObject;
            earth.transform.parent.GetComponent<Earth>().isProcessed = false;
            earth.transform.parent.GetComponent<Earth>().adjustMiningSpeedNegative();
            bucket_Water.GetComponent<Animator>().Play("NewState");
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
}
