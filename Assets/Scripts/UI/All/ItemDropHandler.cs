using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropHandler : MonoBehaviour, IDropHandler, System.ICloneable
{
    public Inventory _Inventory;

    public Sprite originalImageSwapTo;

    public ItemDragHandler originalItemDragHandlerSwapTo;

    public int originalSwapToCounter;

    public Transform LowerInventoryPanel;

    public Transform ShopPanel;

    public BottomInventory fastInventory;

    //coin counter;
    public CoinPurse mainCoinPurse;
    public Text coinPurseIndicator;
    public object Clone()
    {
        return MemberwiseClone();
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = (transform as RectTransform);

        //Debug.Log("Transform Name " + transform.name);

        int slotOfDraggedImageOriginalIndex = eventData.pointerDrag.GetComponent<ItemDragHandler>().slotOfDraggedImageIndex;
        int rowOfDraggedImageOriginalIndex = eventData.pointerDrag.GetComponent<ItemDragHandler>().rowOfDraggedImageIndex;
         
        Transform slotOfDraggedImage = eventData.pointerDrag.transform.parent.parent;

        slotOfDraggedImage.SetSiblingIndex(slotOfDraggedImageOriginalIndex);

        Transform rowOfDraggedImage = slotOfDraggedImage.parent;

        rowOfDraggedImage.SetSiblingIndex(rowOfDraggedImageOriginalIndex);

        eventData.pointerDrag.GetComponent<ItemDragHandler>().justStarted = 0;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition) && !RectTransformUtility.RectangleContainsScreenPoint((LowerInventoryPanel as RectTransform), Input.mousePosition) && !RectTransformUtility.RectangleContainsScreenPoint((ShopPanel as RectTransform), Input.mousePosition))
        {
            IInventoryItem item = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;
            
            if (item != null)
            {
                _Inventory.RemoveItem(item);
            }
        } else if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition) && RectTransformUtility.RectangleContainsScreenPoint((LowerInventoryPanel as RectTransform), Input.mousePosition))
        {
            //Debug.Log("Inside main panel, bottom drop handler");
            var pointer = new PointerEventData(EventSystem.current);

            pointer.position = Input.mousePosition;

            List<RaycastResult> objectUnderMouse = new List<RaycastResult>();

            Image draggedImage = eventData.pointerDrag.transform.GetComponent<Image>();

            draggedImage.raycastTarget = false;

            EventSystem.current.RaycastAll(pointer, objectUnderMouse);

            draggedImage.raycastTarget = true;

            //Debug.Log(objectUnderMouse.First().gameObject);

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_BORDER")
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(0).GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetChild(0).GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(1).GetComponent<Text>();

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<Text>();

                IInventoryItem item = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;
                fastInventory.mItems.Add(item);
                _Inventory.mItems.Remove(item);
                imageSwapTo.enabled = true;
                imageSwapTo.sprite = imageSwapFrom.sprite;
                CounterIntSwapTo.itemsCount = CounterIntSwapFrom.itemsCount;

                CounterIntTextSwapTo.text = CounterIntSwapTo.itemsCount.ToString();
                CounterIntSwapTo.itemsInSlot = CounterIntSwapFrom.itemsInSlot;
                CounterIntSwapTo.Type = CounterIntSwapFrom.Type;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;

                imageSwapFrom.sprite = null;
                imageSwapFrom.enabled = false;
                CounterIntSwapFrom.itemsCount = 0;
                CounterIntTextSwapFrom.text = "";
                CounterIntSwapFrom.itemsInSlot = null;
                CounterIntSwapFrom.Type = null;

                imageSwapFromDragHandler.Item = null;
            }

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_IMAGE")
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapTo = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapTo = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).transform.GetComponent<Text>();

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<Text>();

                int justStart = 0;
                if (justStart == 0)
                {
                    originalImageSwapTo = imageSwapTo.sprite;
                    originalItemDragHandlerSwapTo = (ItemDragHandler)imageSwapToDragHandler.Clone();
                    originalSwapToCounter = CounterIntSwapTo.itemsCount;
                    justStart++;
                }
                IInventoryItem itemSwapFrom = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;
                fastInventory.mItems.Add(itemSwapFrom);
                _Inventory.mItems.Remove(itemSwapFrom);

                IInventoryItem itemSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>().Item;
                fastInventory.mItems.Remove(itemSwapTo);
                _Inventory.mItems.Add(itemSwapTo);

                imageSwapTo.sprite = imageSwapFrom.sprite;
                CounterIntSwapTo.itemsCount = CounterIntSwapFrom.itemsCount;

                imageSwapFrom.sprite = originalImageSwapTo;
                CounterIntTextSwapTo.text = CounterIntSwapTo.itemsCount.ToString();
                CounterIntSwapTo.itemsInSlot = CounterIntSwapFrom.itemsInSlot;
                CounterIntSwapTo.Type = CounterIntSwapFrom.Type;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;
                imageSwapFromDragHandler.Item = originalItemDragHandlerSwapTo.Item;
                CounterIntSwapFrom.itemsCount = originalSwapToCounter;
                CounterIntTextSwapFrom.text = CounterIntSwapFrom.itemsCount.ToString();
                CounterIntSwapFrom.itemsInSlot = CounterIntSwapTo.itemsInSlot;
                CounterIntSwapFrom.Type = CounterIntSwapTo.Type;

                originalImageSwapTo = null;
                originalItemDragHandlerSwapTo.Item = null;
                justStart = 0;

            }
        }
        else if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition) && RectTransformUtility.RectangleContainsScreenPoint((ShopPanel as RectTransform), Input.mousePosition))
        {
            //Debug.Log("Inside main panel, bottom drop handler");
            var pointer = new PointerEventData(EventSystem.current);

            pointer.position = Input.mousePosition;

            List<RaycastResult> objectUnderMouse = new List<RaycastResult>();

            Image draggedImage = eventData.pointerDrag.transform.GetComponent<Image>();

            draggedImage.raycastTarget = false;

            EventSystem.current.RaycastAll(pointer, objectUnderMouse);

            draggedImage.raycastTarget = true;

            //Debug.Log(objectUnderMouse.First().gameObject);



            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_IMAGE_SHOP")
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<Text>();

                IInventoryItem itemSwapFrom = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;

                mainCoinPurse.AddAmmount(imageSwapFromDragHandler.Item.Cost * CounterIntSwapFrom.itemsCount);
                coinPurseIndicator.text = mainCoinPurse.CoinAmmount.ToString();
                _Inventory.mItems.Remove(itemSwapFrom);

                imageSwapFrom.sprite = null;
                imageSwapFrom.enabled = false;
                CounterIntSwapFrom.itemsCount = 0;
                CounterIntTextSwapFrom.text = "";
                CounterIntSwapFrom.itemsInSlot = null;
                CounterIntSwapFrom.Type = null;

                imageSwapFromDragHandler.Item = null;

                
            }
        }

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            var pointer = new PointerEventData(EventSystem.current);

            pointer.position = Input.mousePosition;

            List<RaycastResult> objectUnderMouse = new List<RaycastResult>();

            Image draggedImage = eventData.pointerDrag.transform.GetComponent<Image>();

            draggedImage.raycastTarget = false ;

            EventSystem.current.RaycastAll(pointer, objectUnderMouse);

            draggedImage.raycastTarget = true;

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_BORDER" && objectUnderMouse.First().gameObject != eventData.pointerDrag.transform.parent.gameObject)
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(0).GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetChild(0).GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(1).GetComponent<Text>();
                //Debug.Log(CounterIntSwapTo.itemsCount);

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<Text>();
                
                imageSwapTo.enabled = true;
                imageSwapTo.sprite = imageSwapFrom.sprite;
                CounterIntSwapTo.itemsCount = CounterIntSwapFrom.itemsCount;
                
                CounterIntTextSwapTo.text = CounterIntSwapTo.itemsCount.ToString();
                CounterIntSwapTo.itemsInSlot = CounterIntSwapFrom.itemsInSlot;
                CounterIntSwapTo.Type = CounterIntSwapFrom.Type;
                //Debug.Log(CounterIntSwapTo.itemsCount); 

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;

                imageSwapFrom.sprite = null;
                imageSwapFrom.enabled = false;
                CounterIntSwapFrom.itemsCount = 0;
                CounterIntTextSwapFrom.text = "";
                CounterIntSwapFrom.itemsInSlot = null;
                CounterIntSwapFrom.Type = null;

                imageSwapFromDragHandler.Item = null;
            }

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_IMAGE" && objectUnderMouse.First().gameObject != eventData.pointerDrag.transform.parent.gameObject)
            {
                //Debug.Log("Swapped");
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapTo = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapTo = objectUnderMouse.First().gameObject.transform.parent.GetChild(1).transform.GetComponent<Text>();
                //Debug.Log(CounterIntSwapTo.itemsCount);

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
                ItemCounter CounterIntSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<ItemCounter>();
                Text CounterIntTextSwapFrom = eventData.pointerDrag.transform.parent.GetChild(1).GetComponent<Text>();
                //Debug.Log(CounterIntSwapFrom.itemsCount);

                int justStart = 0;
                if (justStart == 0)
                {
                    originalImageSwapTo = imageSwapTo.sprite;
                    //originalCounterImageSwapTo
                    originalItemDragHandlerSwapTo = (ItemDragHandler)imageSwapToDragHandler.Clone();
                    originalSwapToCounter = CounterIntSwapTo.itemsCount;
                    justStart++;
                }

                imageSwapTo.sprite = imageSwapFrom.sprite;
                CounterIntSwapTo.itemsCount = CounterIntSwapFrom.itemsCount;
                imageSwapFrom.sprite = originalImageSwapTo;
                CounterIntTextSwapTo.text = CounterIntSwapTo.itemsCount.ToString();
                CounterIntSwapTo.itemsInSlot = CounterIntSwapFrom.itemsInSlot;
                CounterIntSwapTo.Type = CounterIntSwapFrom.Type;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;
                imageSwapFromDragHandler.Item = originalItemDragHandlerSwapTo.Item;
                CounterIntSwapFrom.itemsCount = originalSwapToCounter;
                CounterIntTextSwapFrom.text = CounterIntSwapFrom.itemsCount.ToString();
                CounterIntSwapFrom.itemsInSlot = CounterIntSwapTo.itemsInSlot;
                CounterIntSwapFrom.Type = CounterIntSwapTo.Type;
                //Debug.Log(originalSwapToCounter);
                originalImageSwapTo = null;
                originalItemDragHandlerSwapTo.Item = null;
                justStart = 0;
            }
        }
    }
}

