using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 63;

    public event EventHandler<InventoryItemEventArgs> ItemAdded;

    public event EventHandler<InventoryItemEventArgs> ItemSwapped;

    public event EventHandler<InventoryItemEventArgs> ItemRemoved;

    public List<IInventoryItem> mItems = new List<IInventoryItem>();

    public Inventory mainInventory;

    public BottomInventory bottomInventory;

    //Coin Purse

    public CoinPurse mainCoinPurse;
    public Text coinPurseIndicator;

    //Merchant
    public GameObject shopInventoryInterface;

    //Method, defines what will hapen, when player clicks on item (Ctrl + mouseClick/ Alt+mouseClick)
    public void ClickHandler()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        List<RaycastResult> objectUnderMouse = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, objectUnderMouse);

        //Debug.Log(objectUnderMouse.First().gameObject);

        if (Input.GetKey(KeyCode.LeftControl))
        /*Defines what will happen if you LeftCtrl+Click on item in main inventory;*/
        {
            if (objectUnderMouse.First().gameObject.tag == "IS_IMAGE" && bottomInventory.mItems.Count < 9)
            {
                Image imageSwapFrom = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();
                IInventoryItem itemSwapFrom = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>().Item;
                ItemCounter CounterIntSwapFrom = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<Text>();

                mainInventory.mItems.Remove(itemSwapFrom);
                
                bottomInventory.AddItem(itemSwapFrom, "CtrlSwap");

                imageSwapFrom.enabled = false;
                imageSwapFromDragHandler.Item = null;
                CounterIntSwapFrom.itemsCount = 0;
                CounterIntTextSwapFrom.text = "";
                CounterIntSwapFrom.itemsInSlot = null;
                CounterIntSwapFrom.Type = null;
            }
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        /*Defines what will happen if you LeftAlt+Click on item in main inventory;*/
        {
            if (objectUnderMouse.First().gameObject.tag == "IS_IMAGE" && shopInventoryInterface.gameObject.activeSelf == true)
            {
                Image imageSwapFrom = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();
                IInventoryItem itemSwapFrom = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>().Item;
                ItemCounter CounterIntSwapFrom = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<Text>();

                mainCoinPurse.AddAmmount(imageSwapFromDragHandler.Item.Cost * CounterIntSwapFrom.itemsCount);
                coinPurseIndicator.text = mainCoinPurse.CoinAmmount.ToString();

                mainInventory.mItems.Remove(itemSwapFrom);

                imageSwapFrom.enabled = false;
                imageSwapFromDragHandler.Item = null;
                CounterIntSwapFrom.itemsCount = 0;
                CounterIntTextSwapFrom.text = "";
                CounterIntSwapFrom.itemsInSlot = null;
                CounterIntSwapFrom.Type = null;
            }
        }
    }

    public void AddItem(IInventoryItem item)
    {
        
        if (mItems.Count < SLOTS)
        {
            MeshCollider collider = (item as MonoBehaviour).GetComponent<MeshCollider>();

            if (collider.enabled)
            {
                collider.enabled = false;
                item.OnPickUp();
            }
                //mItems.Add(item);

            if(ItemAdded != null)
            {
                ItemAdded(this, new InventoryItemEventArgs(item));
                //Debug.Log("Collider is enabled");
            }
        }
    }

    public void AddItem(IInventoryItem item, string process)
    {
        
        if (mItems.Count < SLOTS)
        {
            MeshCollider collider = (item as MonoBehaviour).GetComponent<MeshCollider>();

            if (collider.enabled)
            {
                collider.enabled = false;
                item.OnPickUp();
            }
            //mItems.Add(item);

            if (ItemAdded != null)
            {
                if (process == "CtrlSwap")
                {
                    
                    ItemSwapped(this, new InventoryItemEventArgs(item));
                } else
                {
                    
                    //ItemAdded(this, new InventoryItemEventArgs(item));
                }
                
                //Debug.Log("Collider is enabled");
            }
        }
    }

    public void AddItemToList(IInventoryItem item)
    {
        mItems.Add(item);
    }

    public void RemoveItem(IInventoryItem item)
    {
        //Debug.Log("RemoveItem");
        if (mItems.Contains(item))
        {
            //Debug.Log("mItems.Contains(item)");
            mItems.Remove(item);

            item.OnDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (!collider.enabled)
            {
                collider.enabled = true;
            }

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryItemEventArgs(item));
            }
        }
    }
}
