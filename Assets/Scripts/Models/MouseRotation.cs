using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseRotation : MonoBehaviour
{
    //Rotation
    public GameObject HUD;
    public GameObject shopInventoryInterface;
    public float yRotation;
    public float mouseSensitivity = 100f;
    public Transform characterBody;
    

    //Mining Resources
    public CharacterAnimation characterAnimator;
    public float oreDetectedDistance;
    public IInventoryItem oreDetectedInstance;
    public GameObject oreDetectedInstanceGO;
    public Health oreDetectedHealth;
    public GameObject itemDetectedGO;
    public float itemDetectedDistance;
    public IInventoryItem itemDetected;
    private GameObject currentHitObject;
    public float currentHitObjectDistance;
    public float maxDistance = 3;
    public LayerMask currentObjectLayerMask;
    public PersonMovement personMovement;
    public PersonMovement personMovement1;
    //Alchemy Mining
    public Animator alchemyEnter;

    //typer of resources



    //Images
    public Image InventoryLockState;
    public Sprite Unlocked;
    public Sprite Locked;

    //UIInventories
    public Inventory inventory;
    public BottomInventory bottomInventory;

    //HealthBar
    public Animator anim;

    //Interract
    public Text clickToInterractText;
    public event Action rockOreDetected = delegate { }; //Send notification that character found rock ore to first quest
    public event Action mossDetected = delegate { }; //Send notification that character found moss to eighth quest

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(358.33429f, 34.6216583f, 359.453156f);
    }

    // Update is called once per frame
    void Update()
    {
        if (HUD.GetComponent<CanvasGroup>().alpha == 1 || shopInventoryInterface.activeInHierarchy)
        {
            
        }
        else {
            float xRot = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            float yRot = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

            yRotation -= yRot * 2;
            yRotation = Mathf.Clamp(yRotation, -90f, 50f);

            transform.localRotation = Quaternion.Euler(yRotation, 40.6216583f, 0f);

            characterBody.Rotate(Vector3.up * xRot * 3);
        }

        RaycastHit hit = new RaycastHit();
        
        if (Physics.SphereCast(transform.position, 1.0f, transform.TransformDirection(Vector3.forward * 3), out hit, 2f, currentObjectLayerMask))
        {
            GameObject Object = hit.transform.gameObject;
            double distanceFromObject = hit.distance;
            
            if (hit.collider != null )
            {
                
                itemDetectedGO = hit.collider.gameObject;
                itemDetectedDistance = hit.distance;
                clickToInterractText.gameObject.SetActive(true);
                Debug.Log(itemDetectedGO.transform.parent.GetComponent<IResource>());
                Debug.Log(itemDetectedGO.transform.parent.GetComponent<IResource>().Type);
                //Detecting rock ore for the first quest
                if (rockOreDetected != null && itemDetectedGO.GetComponent<IResource>() != null && itemDetectedGO.GetComponent<IResource>().Type == "Boulder")
                {
                    rockOreDetected(); //Send notification that character found rock ore to first quest
                } else if (mossDetected != null && itemDetectedGO.transform.parent.GetComponent<IResource>() != null && itemDetectedGO.transform.parent.GetComponent<IResource>().Type == "Moss")
                {
                    //Debug.Log("Something");
                    mossDetected(); //Send notification that character found moss to eighth quest
                } else { }
                
                /*
                if (itemDetected.Type == "Ore" && hit.distance < 2.2 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
                {
                    //Debug.Log("Seeing ore");
                    characterAnimator.seeingOre();
                }
                if (itemDetected.Type == "Ore" && hit.distance < 2.2 && characterAnimator.beingBusy == false && characterAnimator.IS_ALCHEMY_MINED == 1)
                {
                    //Debug.Log("Seeing ore");
                    characterAnimator.seeingOre();
                    alchemyEnter.Play("AlchemyEnter");
                    itemDetectedGO.transform.GetChild(0).gameObject.SetActive(true);
                    itemDetectedGO.transform.GetChild(1).gameObject.SetActive(false);
                }
                if (itemDetected.Type == "Tree" && hit.distance < 1.2 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
                {
                    //Debug.Log("Seeing tree");
                    characterAnimator.seeingTree();
                }
                if (itemDetected.Type == "Grass" && hit.distance < 1.6 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
                {
                    //Debug.Log("Seeing grass");
                    characterAnimator.seeingGrass();
                }*/
            }
            


            //Debug.Log(currentHitObject);
        }
        else
        {
            double distanceFromObject = maxDistance;
            GameObject Object = null;
            //Debug.Log("Nothing really detected");
            clickToInterractText.gameObject.SetActive(false);
        }
        //Camera controll


        /*
        RaycastHit hit = new RaycastHit();

        if (Physics.SphereCast(transform.position, 1.0f, transform.TransformDirection(Vector3.forward * 3), out hit, maxDistance, currentObjectLayerMask))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitObjectDistance = hit.distance;
            //Debug.Log(currentHitObject);
        } 
        else
        {
            currentHitObjectDistance = maxDistance;
            currentHitObject = null;
        }

        if ( hit.collider != null) {
            itemDetected = hit.collider.GetComponent<IInventoryItem>();
            itemDetectedGO = hit.collider.gameObject;
            itemDetectedDistance = hit.distance;
            if (itemDetected.Type == "Ore" && hit.distance < 2.2 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
            {
                //Debug.Log("Seeing ore");
                characterAnimator.seeingOre();
            }
            if (itemDetected.Type == "Ore" && hit.distance < 2.2 && characterAnimator.beingBusy == false && characterAnimator.IS_ALCHEMY_MINED == 1)
            {
                //Debug.Log("Seeing ore");
                characterAnimator.seeingOre();
                alchemyEnter.Play("AlchemyEnter");
                itemDetectedGO.transform.GetChild(0).gameObject.SetActive(true);
                itemDetectedGO.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (itemDetected.Type == "Tree" && hit.distance < 1.2 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
            {
                //Debug.Log("Seeing tree");
                characterAnimator.seeingTree();
            }
            if (itemDetected.Type == "Grass" && hit.distance < 1.6 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
            {
                //Debug.Log("Seeing grass");
                characterAnimator.seeingGrass();
            }
        } */
    }
    
    public GameObject detectObject()
    {
        
        RaycastHit hit = new RaycastHit();

        if (Physics.SphereCast(transform.position, 1.0f, transform.TransformDirection(Vector3.forward * 3), out hit, maxDistance, currentObjectLayerMask))
        {
            GameObject Object = hit.transform.gameObject;
            double distanceFromObject = hit.distance;
            
            if (hit.collider != null)
            {
                itemDetected = hit.collider.GetComponent<IInventoryItem>();
                itemDetectedGO = hit.collider.gameObject;
                itemDetectedDistance = hit.distance;
                return itemDetectedGO;
                /*
                if (itemDetected.Type == "Ore" && hit.distance < 2.2 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
                {
                    //Debug.Log("Seeing ore");
                    characterAnimator.seeingOre();
                }
                if (itemDetected.Type == "Ore" && hit.distance < 2.2 && characterAnimator.beingBusy == false && characterAnimator.IS_ALCHEMY_MINED == 1)
                {
                    //Debug.Log("Seeing ore");
                    characterAnimator.seeingOre();
                    alchemyEnter.Play("AlchemyEnter");
                    itemDetectedGO.transform.GetChild(0).gameObject.SetActive(true);
                    itemDetectedGO.transform.GetChild(1).gameObject.SetActive(false);
                }
                if (itemDetected.Type == "Tree" && hit.distance < 1.2 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
                {
                    //Debug.Log("Seeing tree");
                    characterAnimator.seeingTree();
                }
                if (itemDetected.Type == "Grass" && hit.distance < 1.6 && characterAnimator.beingBusy == false && characterAnimator.IS_MINED == 1)
                {
                    //Debug.Log("Seeing grass");
                    characterAnimator.seeingGrass();
                }*/
            } else { return null; }

            
            //Debug.Log(currentHitObject);
        }
        else
        {
            double distanceFromObject = maxDistance;
            GameObject Object = null;
            return null;
        }
    }

    public void detectToItems()
    {
        oreDetectedInstance = itemDetected;
        oreDetectedInstanceGO = itemDetectedGO;
        oreDetectedDistance = itemDetectedDistance;
        oreDetectedHealth = itemDetectedGO.GetComponent<Health>();

        
        //Debug.Log(oreDetectedInstanceGO);
    }

    public void mineAlchemyResources()
    {
        //if (oreDetectedHealth.currentHealth == 0) { alchemyEnter.Play("New State"); anim.speed = 0.01f; }
        
    }

    public void stopMineAlchemyResources()
    {
        //if (oreDetectedHealth.currentHealth == 0) { alchemyEnter.Play("New State"); anim.speed = 0.01f; }
        itemDetectedGO.transform.GetChild(0).gameObject.SetActive(false);
        itemDetectedGO.transform.GetChild(1).gameObject.SetActive(true);
        oreDetectedInstanceGO.transform.Find("HealthBarCanvas").gameObject.SetActive(true);
        oreDetectedInstanceGO.transform.Find("HealthBarCanvas").gameObject.GetComponent<HealthBar>().showBar();
    }

    public void startParticles()
    {
        if (oreDetectedHealth.currentHealth <= 0)
        {
            anim.speed = 0.03f;
            personMovement.speed = 10;
            mouseSensitivity = 100;
        }
    }
    public void stopParticles()
    {
        if (oreDetectedHealth.currentHealth <= 0)
        {
            anim.speed = 0.03f;
            //particleSystemMined.Stop();
        }
    }
    public void startCollectAlchemyResources()
    {
        modifyHealth();
        oreDetectedInstanceGO.GetComponent<Animator>().Play("RockMinedShatter");
        
        if (oreDetectedHealth.currentHealth <= 0)
        {
            anim.speed = 0.001f;
            oreDetectedInstanceGO.GetComponent<Animator>().speed = 2;
            oreDetectedInstanceGO.GetComponent<Animator>().Play("Rock_Dissapper");
            
        }
    }

    public void TriggerFallingAnimation()
    {
        if (oreDetectedInstance.Type == "Tree")
        {
            oreDetectedInstanceGO.GetComponentInParent<TreeAnimation>().triggerTreeFallingAnimation();
        }
        if (oreDetectedInstance.Type == "Grass")
        {
            oreDetectedInstanceGO.GetComponentInParent<GrassAnimation>().triggerTreeFallingAnimation();
        }
    }
    
    public void collectToItems()
    {
        if (oreDetectedHealth.currentHealth <= 0)
        {
            //Debug.Log(item);
            if (oreDetectedInstance != null)
            {

                //Debug.Log((oreDetectedInstance as MonoBehaviour).GetComponent<MeshCollider>());
                //Debug.Log((oreDetectedInstance as MonoBehaviour).GetComponent<MeshCollider>().enabled);

                if (InventoryLockState.sprite == Locked)
                {
                    inventory.AddItem(oreDetectedInstance);
                }
                else if (InventoryLockState.sprite == Unlocked)
                {
                    bottomInventory.AddItem(oreDetectedInstance);
                    if (bottomInventory.mItems.Count > 8)
                    {
                        inventory.AddItem(oreDetectedInstance);
                    }
                }
            }
        } else if (oreDetectedHealth.currentHealth > 0)
        {
            Debug.Log(oreDetectedHealth.currentHealth);
        }

        
    }

    public void modifyHealth() //modifing health of an item after atack
    {
        int x = 0;
        int y = 0;
        int z1 = 0;
        int z2 = 0;
        if (oreDetectedInstanceGO.GetComponent<ItemHealthCounter>().firstSphereFilled) 
        {
            x = -1;
        }
        if (oreDetectedInstanceGO.GetComponent<ItemHealthCounter>().lowSphereFilled)
        {
            y = -7;
        }
        if (oreDetectedInstanceGO.GetComponent<ItemHealthCounter>().middleSphereFilled1)
        {
            z1 = -20;
        }
        if (oreDetectedInstanceGO.GetComponent<ItemHealthCounter>().middleSphereFilled2)
        {
            z2 = -20;
        }
        int summ = x + y + z1 + z2;
        //Debug.Log(summ);
        oreDetectedHealth.ModifyHealth(summ);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * currentHitObjectDistance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * currentHitObjectDistance, 1.0f);
        
    }
    
    //Vector3(2.11702943,101.237633,352.082001)Vector3Vector3(358.617737,57.7537231,356.447815)Vector3(358.33429,34.6216583,359.453156)
}
