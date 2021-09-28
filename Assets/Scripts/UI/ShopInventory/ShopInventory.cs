using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    private const int SLOTS = 63;

    public event EventHandler<InventoryItemEventArgs> ItemAdded;

    public event EventHandler<InventoryItemEventArgs> ItemRemoved;

    public List<IInventoryItem> mItems = new List<IInventoryItem>();

    public void AddItem(IInventoryItem item)
    {
        //Debug.Log("Bottom inventory");
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider.enabled)
            {
                //Debug.Log("Collider Enabled");
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickUp();

                if (ItemAdded != null)
                {
                    //Debug.Log("Item Added");
                    ItemAdded(this, new InventoryItemEventArgs(item));
                }
            }
        }
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