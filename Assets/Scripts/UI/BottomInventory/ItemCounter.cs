using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCounter : MonoBehaviour
{
    public int itemsCount = 0;
    public string Type;
    public List<IInventoryItem> itemsInSlot = new List<IInventoryItem>();
    public int itemsAdded = 0;
    public int itemsAddingPerObject = 5;
    public HUDBottomInventory hudBottomInventory;
    public HUD inventory;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addToCounter(int i)
    {
        StartCoroutine(addItems(i));
    }

    public IEnumerator addItems(int i)
    {
        if (i == 0)
        {
            itemsAdded = 0;
            while (itemsAdded < itemsAddingPerObject)
            {
                itemsAdded++;
                itemsCount += 1;
                hudBottomInventory.CounterText.text = itemsCount.ToString();
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
                inventory.CounterText.text = itemsCount.ToString();
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
        if (itemsCount == 0) { itemsInSlot.Add(item); }
    }

    
    
    // Update is called once per frame
    
}
