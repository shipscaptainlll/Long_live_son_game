using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDShopInventory : MonoBehaviour
{
    public ShopInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += InventoryScript_ItemRemoved;

    }

    public void InventoryScript_ItemAdded(object sender, InventoryItemEventArgs e)
    {
        Transform invPanel = transform.Find("InventoryPanel2");
        Transform rowPanel = invPanel.GetChild(0);
        foreach (Transform row in invPanel)
        {
            bool isAdded = false;
            foreach (Transform slot in row)
            {

                Transform imgPanel = slot.GetChild(0).GetChild(0);
                Image Image = imgPanel.GetComponent<Image>();

                ItemDragHandler itemDragHandler = imgPanel.GetComponent<ItemDragHandler>();

                if (!Image.enabled)
                {
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
        Transform invPanel = transform.Find("InventoryPanel2");

        //Transform row = invPanel.GetChild(0);
        foreach (Transform row in invPanel)
        {
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
