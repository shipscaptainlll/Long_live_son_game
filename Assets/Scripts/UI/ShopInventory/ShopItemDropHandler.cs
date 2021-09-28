using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemDropHandler : MonoBehaviour, IDropHandler, System.ICloneable
{
    public ShopInventory _Inventory;

    public Inventory mainInventory;

    public BottomInventory fastInventory;

    public Sprite originalImageSwapTo;

    public ItemDragHandler originalItemDragHandlerSwapTo;

    public Transform UpperInventoryPanel;

    

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

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition) && !RectTransformUtility.RectangleContainsScreenPoint((UpperInventoryPanel as RectTransform), Input.mousePosition))
        {
            //Debug.Log("Outside main panel, bottom drop handler");
            IInventoryItem item = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;

            if (item != null)
            {
                _Inventory.RemoveItem(item);
            }
        }
        else if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition) && RectTransformUtility.RectangleContainsScreenPoint((UpperInventoryPanel as RectTransform), Input.mousePosition))
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

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

                IInventoryItem item = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;
                mainInventory.mItems.Add(item);
                _Inventory.mItems.Remove(item);
                imageSwapTo.enabled = true;
                imageSwapTo.sprite = imageSwapFrom.sprite;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;

                imageSwapFrom.sprite = null;
                imageSwapFrom.enabled = false;

                imageSwapFromDragHandler.Item = null;
            }

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_IMAGE")
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

                int justStart = 0;
                if (justStart == 0)
                {
                    originalImageSwapTo = imageSwapTo.sprite;
                    originalItemDragHandlerSwapTo = (ItemDragHandler)imageSwapToDragHandler.Clone();

                    justStart++;
                }
                IInventoryItem itemSwapFrom = eventData.pointerDrag.GetComponent<ItemDragHandler>().Item;
                mainInventory.mItems.Add(itemSwapFrom);
                _Inventory.mItems.Remove(itemSwapFrom);

                IInventoryItem itemSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>().Item;
                mainInventory.mItems.Remove(itemSwapTo);
                _Inventory.mItems.Add(itemSwapTo);

                imageSwapTo.sprite = imageSwapFrom.sprite;
                imageSwapFrom.sprite = originalImageSwapTo;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;
                imageSwapFromDragHandler.Item = originalItemDragHandlerSwapTo.Item;

                originalImageSwapTo = null;
                originalItemDragHandlerSwapTo.Item = null;
                justStart = 0;
            }
        }

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            var pointer = new PointerEventData(EventSystem.current);

            pointer.position = Input.mousePosition;

            List<RaycastResult> objectUnderMouse = new List<RaycastResult>();

            Image draggedImage = eventData.pointerDrag.transform.GetComponent<Image>();

            draggedImage.raycastTarget = false;

            EventSystem.current.RaycastAll(pointer, objectUnderMouse);

            draggedImage.raycastTarget = true;

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_BORDER" && objectUnderMouse.First().gameObject != eventData.pointerDrag.transform.parent.gameObject)
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetChild(0).GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetChild(0).GetComponent<ItemDragHandler>();

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

                imageSwapTo.enabled = true;
                imageSwapTo.sprite = imageSwapFrom.sprite;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;

                imageSwapFrom.sprite = null;
                imageSwapFrom.enabled = false;

                imageSwapFromDragHandler.Item = null;
            }

            if (objectUnderMouse.Count > 0 && objectUnderMouse.First().gameObject.tag == "IS_IMAGE" && objectUnderMouse.First().gameObject != eventData.pointerDrag.transform.parent.gameObject)
            {
                Image imageSwapTo = objectUnderMouse.First().gameObject.transform.GetComponent<Image>();
                ItemDragHandler imageSwapToDragHandler = objectUnderMouse.First().gameObject.transform.GetComponent<ItemDragHandler>();

                Image imageSwapFrom = eventData.pointerDrag.GetComponent<Image>();
                ItemDragHandler imageSwapFromDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

                int justStart = 0;
                if (justStart == 0)
                {
                    originalImageSwapTo = imageSwapTo.sprite;
                    originalItemDragHandlerSwapTo = (ItemDragHandler)imageSwapToDragHandler.Clone();

                    justStart++;
                }

                imageSwapTo.sprite = imageSwapFrom.sprite;
                imageSwapFrom.sprite = originalImageSwapTo;

                imageSwapToDragHandler.Item = imageSwapFromDragHandler.Item;
                imageSwapFromDragHandler.Item = originalItemDragHandlerSwapTo.Item;

                originalImageSwapTo = null;
                originalItemDragHandlerSwapTo.Item = null;
                justStart = 0;
            }
        }
    }
}

