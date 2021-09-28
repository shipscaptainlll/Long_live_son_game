using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BottomItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, System.ICloneable
{
    public int slotOfDraggedImageIndex;
    public int rowOfDraggedImageIndex;
    public int justStarted;
    public IInventoryItem Item { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;


        if (justStarted == 0) { slotOfDraggedImageIndex = -1; rowOfDraggedImageIndex = -1; justStarted++; }

        Transform slotOfDraggedImage = eventData.pointerDrag.transform.parent.parent;
        if (slotOfDraggedImageIndex == -1) { slotOfDraggedImageIndex = slotOfDraggedImage.GetSiblingIndex(); }

        Transform rowOfDraggedImage = slotOfDraggedImage.parent;

        if (rowOfDraggedImageIndex == -1) { rowOfDraggedImageIndex = rowOfDraggedImage.GetSiblingIndex(); }
        //Debug.Log("rowOfDraggedImageIndex: " + rowOfDraggedImageIndex + "justStarted: " + justStarted + "slotOfDraggedImageIndex: " + slotOfDraggedImageIndex);

        slotOfDraggedImage.SetAsLastSibling();
        rowOfDraggedImage.SetAsLastSibling();


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");


        transform.localPosition = Vector3.zero;



        //Image draggedImage = eventData.pointerDrag.transform.GetComponent<Image>();

        //draggedImage.raycastTarget = true;

    }
}
