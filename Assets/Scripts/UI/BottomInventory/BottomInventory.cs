using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomInventory : MonoBehaviour
{
    private const int SLOTS = 9;

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
    public GameObject mainInventoryInterface;

    public void ClickHandler()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        List<RaycastResult> objectUnderMouse = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, objectUnderMouse);

        if (Input.GetKey(KeyCode.LeftControl))
        /*Defines what will happen if you LeftCtrl+Click on item in main inventory;*/
        {
            if (objectUnderMouse.First().gameObject.tag == "IS_IMAGE" && bottomInventory.mItems.Count <= 9 && mainInventoryInterface.gameObject.activeSelf == true)
            {
                Image imageSwapFrom = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();
                IInventoryItem itemSwapFrom = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>().Item;
                ItemCounter CounterIntSwapFrom = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<Text>();
                
                mainInventory.AddItem(itemSwapFrom, "CtrlSwap");
                bottomInventory.mItems.Remove(itemSwapFrom);
                
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
                bottomInventory.mItems.Remove(itemSwapFrom);

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
                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryItemEventArgs(item));
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
            if (ItemAdded != null)
            {
                if (process == "CtrlSwap")
                {
                    ItemSwapped(this, new InventoryItemEventArgs(item));
                }
                else
                {
                    //ItemAdded(this, new InventoryItemEventArgs(item));
                }
            }
        }
    }

    public void AddItemToList(IInventoryItem item)
    {
        mItems.Add(item);
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
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