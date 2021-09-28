using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCounter : MonoBehaviour
{
    public int itemsCount = 0;
    public string Type;
    public string Name;
    public List<IInventoryItem> itemsInSlot = new List<IInventoryItem>();
    public int itemsAdded = 0;
    public int itemsAddingPerObject;
    public HUDBottomInventory hudBottomInventory;
    public HUD inventory;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addToCounter(int i, IInventoryItem item)
    {
        StartCoroutine(addItems(i, item));
    }

    public IEnumerator addItems(int i, IInventoryItem item)
    {
        if (i == 0)
        {
            itemsAdded = 0;
            itemsAddingPerObject = item.CountPerObject;
            while (itemsAdded < itemsAddingPerObject)
            {
                
                
                itemsAdded++;
                itemsCount += 1;
                item.Count = itemsCount;
                itemsInSlot.First().Count = itemsCount;
                
                hudBottomInventory.CounterText.text = itemsInSlot.First().Count.ToString();
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        if (i == 1)
        {
            itemsAdded = 0;
            while (itemsAdded < itemsAddingPerObject)
            {
                itemsAdded++;
                itemsCount += 1;
                item.Count = itemsCount;
                itemsInSlot.First().Count = itemsCount;

                inventory.CounterText.text = itemsInSlot.First().Count.ToString();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void increaseCounter()
    {
        itemsCount += 5;
    }

    public void addItemInSlot(IInventoryItem item)
    {
        //Debug.Log(item);
        if (itemsInSlot == null) { itemsInSlot = new List<IInventoryItem>(); }
        
        if (itemsCount == 0) { itemsInSlot.Add(item); }
        //Debug.Log(itemsInSlot.First());
    }

    
    
    // Update is called once per frame
    
}
