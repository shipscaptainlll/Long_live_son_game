using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMovement : MonoBehaviour
{
    //movement
    public CharacterController controller;
    public float speed = 10f;
    public bool isGrounded;
    public Transform checkGround;
    public float checkRadius = 0.2f;
    public LayerMask checkLayer;
    Vector3 velocity;
    public float gravity = -14f;
    public float jumpHeight = 2f;
    public int inventoryCountController = 1;

    //UIInventories
    public Inventory inventory;
    public BottomInventory bottomInventory;
    public GameObject HUD;
    public GameObject UpgradePanel;
    public GameObject MeditationPanel;
    public GameObject TreeUpgradePanel;
    public GameObject FarmUpgradePanel;

    public CoinPurse mainCoinPurse;
    public Text coinCounterInventory;

    //Resources
    public RockResourceCounter rockResourceCounter;
    public LogResourceCounter logResourceCounter;
    public RShardResourceCounter rShardResourceCounter;
    public GoldResourceCounter goldResourceCounter;
    public WaterResourceCounter waterResourceCounter;
    public GrassResourceCounter grassResourceCounter;
    public ManureResourceCounter manureResourceCounter;
    public MBagResourceCounter mBagResourceCounter;
    public CarrotResourceCounter carrotResourceCounter;
    public AppleResourceCounter appleResourceCounter;
    public CSeedsResourceCounter cSeedsResourceCounter;
    public ASeedsResourceCounter aSeedsResourceCounter;

    //Images
    public Image InventoryLockState;
    public Sprite Unlocked;
    public Sprite Locked;

    //Merchant 
    public ShopInventory shopInventory;
    public GameObject shopInventoryInterface;
    public Text contactMerchant;
    public int contactMarchantCountController = 0;
    public Merchant merchant;

    //Button controller
    public Text ButtonController;
    public Text ButtonController2;

    //Alchemy enter
    public GameObject Panel1Enter;

    //Force field
    public bool isCastingSphere;
    public GameObject forceField;
    Vector3 speedForceField = new Vector3(1.0f, 1.0f, 1.0f);
    public ParticleSystem magicFog;
    ParticleSystem.ShapeModule ps;

    //Hand
    public GameObject hand;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    //QuickAcess
    public GameObject QuickAccessPanel;
    public QuickAccessController quickAccessController;

    //Cargo panel
    public GameObject CargoPanel;

    //Contacting with trees
    // public TreeAnimation treeAnimation;

    //Alchemy Mining
    public Animator alchemyEnter;

    //UI animation
    public AnimationCalledUI animationUI;

    //Other

    // Start is called before the first frame update
    void Start()
    {

        //HUD.SetActive(false);
        HUD.SetActive(true);
        mainCoinPurse.coinAmmount = 0;
        ButtonController.text = null;
        coinCounterInventory.text = mainCoinPurse.coinAmmount.ToString();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Transform tool in hand.transform)
            {
                tool.gameObject.SetActive(false);
            }
            string toolNeeded = slot1.GetComponent<ItemDragHandler>().Item.Name.ToString();
            hand.transform.Find(toolNeeded).gameObject.SetActive(true);
            Debug.Log(slot1.GetComponent<ItemDragHandler>().Item.Name.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (Transform tool in hand.transform)
            {
                tool.gameObject.SetActive(false);
            }
            string toolNeeded = slot2.GetComponent<ItemDragHandler>().Item.Name.ToString();
            hand.transform.Find(toolNeeded).gameObject.SetActive(true);
            Debug.Log(slot1.GetComponent<ItemDragHandler>().Item.Name.ToString());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (Transform tool in hand.transform)
            {
                tool.gameObject.SetActive(false);
            }
            string toolNeeded = slot3.GetComponent<ItemDragHandler>().Item.Name.ToString();
            hand.transform.Find(toolNeeded).gameObject.SetActive(true);
            Debug.Log(slot1.GetComponent<ItemDragHandler>().Item.Name.ToString());
        }
        if (Input.GetMouseButtonDown(1))
        {

            if (quickAccessController.currentlyOpened == null && quickAccessController.QAPIsOpened == false)
            {
                quickAccessController.openPanel();
            } else if (quickAccessController.isToolOpened == true && quickAccessController.QAPIsOpened == false)
            {
                quickAccessController.closeCurrentlyOpened();
                quickAccessController.openPanel();
            }
            else if (quickAccessController.isPanelOpened == true && quickAccessController.QAPIsOpened == false)
            {
                quickAccessController.closeCurrentlyOpened();
            }
            else if (quickAccessController.currentlyOpened == null && quickAccessController.QAPIsOpened == true)
            {
                quickAccessController.closePanel();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (quickAccessController.currentlyOpened != null && quickAccessController.QAPIsOpened == false)
            {
                quickAccessController.closeCurrentlyOpened();
            } else if (quickAccessController.currentlyOpened == null && quickAccessController.QAPIsOpened == true)
            {
                quickAccessController.closePanel();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            forceField.GetComponent<ForceField>().openSphere();
        }
        
        if (HUD.GetComponent<CanvasGroup>().alpha == 1 || shopInventoryInterface.activeInHierarchy || UpgradePanel.GetComponent<CanvasGroup>().alpha == 1 || QuickAccessPanel.GetComponent<CanvasGroup>().alpha == 1 ||
            CargoPanel.GetComponent<CanvasGroup>().alpha == 1 || MeditationPanel.GetComponent<CanvasGroup>().alpha == 1 || TreeUpgradePanel.GetComponent<CanvasGroup>().alpha == 1 || FarmUpgradePanel.GetComponent<CanvasGroup>().alpha == 1 )
        {
            Cursor.lockState = CursorLockMode.Confined;
        } else { Cursor.lockState = CursorLockMode.Locked;
                 }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            ButtonController.text = "LeftAlt";
            if (Input.GetMouseButtonDown(0))
            {
                ButtonController2.text = "LMB";
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            ButtonController.text = " ";
            ButtonController2.text = " ";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ButtonController.text = "E";
            ButtonController2.text = " ";
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ButtonController.text = " ";
            ButtonController2.text = " ";
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ButtonController.text = "I";
            ButtonController2.text = " ";
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            rockResourceCounter.AddToCounter(10000);
            logResourceCounter.AddToCounter(10000);
            rShardResourceCounter.AddToCounter(10000);
            goldResourceCounter.AddToCounter(10000);
            waterResourceCounter.AddToCounter(10000);
            grassResourceCounter.AddToCounter(10000);
            manureResourceCounter.AddToCounter(10000);
            mBagResourceCounter.AddToCounter(10000);
            carrotResourceCounter.AddToCounter(10000);
            appleResourceCounter.AddToCounter(10000);
            cSeedsResourceCounter.AddToCounter(10000);
            aSeedsResourceCounter.AddToCounter(10000);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            ButtonController.text = " ";
            ButtonController2.text = " ";
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ButtonController.text = "LeftShift";
            if (Input.GetKeyDown(KeyCode.X))
            {
                ButtonController2.text = "X";
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ButtonController.text = " ";
            ButtonController2.text = " ";
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            ButtonController.text = "LeftControl";
            if (Input.GetMouseButton(0))
            {
                ButtonController2.text = "LMB";
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ButtonController.text = " ";
            ButtonController2.text = " ";
        }

        if (Input.GetKeyDown(KeyCode.E) && !shopInventoryInterface.activeSelf)
        {
            if (contactMerchant.IsActive())
            {
                shopInventoryInterface.transform.localPosition = new Vector3(1200, 200, 0);
                HUD.transform.localPosition = new Vector3(-400, 243.75f, 0);
                shopInventoryInterface.gameObject.SetActive(true);
                animationUI.callAnimation(shopInventoryInterface);
                contactMerchant.gameObject.SetActive(false);
            }

        } else if (Input.GetKeyDown(KeyCode.E) && shopInventoryInterface.activeSelf)
        {
            shopInventoryInterface.transform.localPosition = new Vector3(2000, 2000, 0);
            HUD.transform.localPosition = new Vector3(0, 243.75f, 0);
            shopInventoryInterface.gameObject.SetActive(false);

            if (merchant.CONTACT_MERCHANT_TO_UPLOAD == 1 && HUD.GetComponent<CanvasGroup>().alpha == 0) { contactMerchant.gameObject.SetActive(true); }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryCountController == 0)
            {
                HUD.GetComponent<CanvasGroup>().alpha = 0;
                contactMerchant.gameObject.SetActive(true);
                merchant.CONTACT_MERCHANT_TO_UPLOAD = 1;
                HUD.transform.localPosition = new Vector3(2000, 0, 0);
                inventoryCountController++;
            }
            else if (inventoryCountController == 1)
            {
                HUD.GetComponent<CanvasGroup>().alpha = 1;
                animationUI.callAnimation(HUD);
                contactMerchant.gameObject.SetActive(false);
                merchant.CONTACT_MERCHANT_TO_UPLOAD = 1;
                if (shopInventoryInterface.activeInHierarchy)
                {
                    HUD.transform.localPosition = new Vector3(-400, 143.75f, 0);
                } else if (!shopInventoryInterface.activeInHierarchy)
                { HUD.transform.localPosition = new Vector3(0, 143.75f, 0); }
                inventoryCountController--;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.X))
        {
            if (InventoryLockState.sprite == Unlocked)
            {
                InventoryLockState.sprite = Locked;
            } else if (InventoryLockState.sprite == Locked)
            {
                InventoryLockState.sprite = Unlocked;
            }
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(checkGround.position, checkRadius, checkLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        //Debug.Log(item);
        if (item != null && item.Type == "Tool")
        {
            
            if (InventoryLockState.sprite == Locked)
            {
                inventory.AddItem(item);
            } else if (InventoryLockState.sprite == Unlocked)
            {
                bottomInventory.AddItem(item);
                if (bottomInventory.mItems.Count > 8)
                {
                    inventory.AddItem(item);
                }
            }
        }
        //if (hit.collider.tag == "MERCHANT")
        //{
        //    merchant.transform.localPosition = new Vector3(2000, 2000, 2000);
        //}
    }
    
}
