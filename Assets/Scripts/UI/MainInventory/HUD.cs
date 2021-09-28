using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory inventory;
    private int add = 3;
    public Text CounterText;
    // Start is called before the first frame update

    public object Clone()
    {
        return MemberwiseClone();
        
    }

    void Start()
    {
        
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }
    
    public void InventoryScript_ItemAdded(object sender, InventoryItemEventArgs e)
    {
         
        Transform invPanel = transform.Find("InventoryPanel");
        Transform rowPanel = invPanel.GetChild(0);
        foreach (Transform row in invPanel)
        {
            bool isAdded = false;
            foreach (Transform slot in row)
            {
                
                Transform imgPanel = slot.GetChild(0).GetChild(0);
                Image Image = imgPanel.GetComponent<Image>();
                CounterText = slot.GetChild(0).GetChild(1).GetComponent<Text>();
                ItemCounter CounterInt = slot.GetChild(0).GetChild(1).GetComponent<ItemCounter>();

                ItemDragHandler itemDragHandler = imgPanel.GetComponent<ItemDragHandler>();

                if (Image.enabled && CounterInt.itemsCount < 10 && CounterInt.Type == e.Item.Type)
                {
                    //Image.enabled = true;
                    //Image.sprite = e.Item.Image;

                    //itemDragHandler.Item = e.Item;
                    CounterInt.addItemInSlot(e.Item);
                    CounterInt.addToCounter(1);
                    isAdded = true;
                    break;
                    
                }
            }
            if (isAdded == true) { break; }
            foreach (Transform slot in row)
            {

                Transform imgPanel = slot.GetChild(0).GetChild(0);
                Image Image = imgPanel.GetComponent<Image>();
                CounterText = slot.GetChild(0).GetChild(1).GetComponent<Text>();
                ItemCounter CounterInt = slot.GetChild(0).GetChild(1).GetComponent<ItemCounter>();



                ItemDragHandler itemDragHandler = imgPanel.GetComponent<ItemDragHandler>();

                /*if (Image.enabled && CounterInt.itemsCount < 10 && CounterInt.Type == e.Item.Type)
                {
                    CounterInt.increaseCounter();
                    CounterInt.addItemInSlot(e.Item);
                    CounterText.text = CounterInt.itemsCount.ToString();
                    isAdded = true;
                    //Debug.Log(e.Item.Type);
                    //Debug.Log(CounterText.GetComponent<ItemCounter>().itemsCount);
                    break;
                } else if */

                if (!Image.enabled)
                {
                    //CounterInt.increaseCounter(); this
                    Debug.Log(CounterInt.itemsInSlot);

                    CounterInt.addToCounter(1);
                    //CounterText.text = CounterInt.itemsCount.ToString(); this

                    CounterInt.Type = e.Item.Type;
                    //Debug.Log(CounterInt.Type);
                    inventory.AddItemToList(e.Item);
                    //Debug.Log(inventory.mItems.Last());
                    //CounterInt.addItemInSlot(inventory.mItems.Last());
                    //Debug.Log(CounterInt.itemsInSlot.Last());
                    Image.enabled = true;
                    Image.sprite = e.Item.Image;

                    itemDragHandler.Item = e.Item;
                    isAdded = true;
                    break;

                }
            }
            
            if (isAdded == true) { break; }
        }
        
    }

    

    public void InventoryScript_ItemRemoved(object sender, InventoryItemEventArgs e)
    {
        //Debug.Log("InventoryScript_ItemRemoved");
        Transform invPanel = transform.Find("InventoryPanel");
        
        //Transform row = invPanel.GetChild(0);
        foreach (Transform row in invPanel) {
            bool isRemoved = false;
            foreach (Transform slot in row)
            {
                Transform imgPanel = slot.GetChild(0).GetChild(0);
                Image Image = imgPanel.GetComponent<Image>();

                ItemDragHandler itemDragHandler = imgPanel.GetComponent<ItemDragHandler>();
                //Debug.Log(itemDragHandler.Item);
                //Debug.Log(e.Item);
                if (itemDragHandler.Item != null && itemDragHandler.Item.Equals(e.Item))
                {
                    //Debug.Log("itemDragHandler.Item.Equals(e.Item)");
                    Image.enabled = false;
                    Image.sprite = null;

                    itemDragHandler.Item = null;
                    isRemoved = true;
                    break;
                }
            }
            if (isRemoved == true) { break; }
        }
    }
}
