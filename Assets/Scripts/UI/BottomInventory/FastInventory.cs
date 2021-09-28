using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastInventory : MonoBehaviour
{
    private const int SLOTS = 9;
    

    public event EventHandler<InventoryItemEventArgs> ItemAdded;

    public event EventHandler<InventoryItemEventArgs> ItemRemoved;

    public List<IInventoryItem> mItems = new List<IInventoryItem>();
    

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider.enabled)
            {
                
                collider.enabled = false;

                //mItems.Add(item);

                item.OnPickUp();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryItemEventArgs(item));
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