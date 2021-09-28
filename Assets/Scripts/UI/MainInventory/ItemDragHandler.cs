using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, System.ICloneable
{
    public int slotOfDraggedImageIndex;
    public int rowOfDraggedImageIndex;
    public int justStarted;
    public IInventoryItem Item { get; set; }
    public Transform InventoryPanel2;
    
    public object Clone()
    {
        return MemberwiseClone();
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        

        transform.parent.GetChild(1).position = Input.mousePosition + new Vector3(25f, -25f, 0f);
        //transform.parent.GetChild(1).SetAsLastSibling();
        transform.position = Input.mousePosition;
        
        
        
        if (justStarted == 0) { slotOfDraggedImageIndex = -1; rowOfDraggedImageIndex = -1; justStarted++; }
        
        Transform slotOfDraggedImage = eventData.pointerDrag.transform.parent.parent;
        if(slotOfDraggedImageIndex == -1) { slotOfDraggedImageIndex = slotOfDraggedImage.GetSiblingIndex(); }
        
        Transform rowOfDraggedImage = slotOfDraggedImage.parent;
        
        if (rowOfDraggedImageIndex == -1) { rowOfDraggedImageIndex = rowOfDraggedImage.GetSiblingIndex(); }
        //Debug.Log("rowOfDraggedImageIndex: " + rowOfDraggedImageIndex + "justStarted: " + justStarted + "slotOfDraggedImageIndex: " + slotOfDraggedImageIndex);

        Transform HUDOfDraggedImage = rowOfDraggedImage.parent.parent;

        slotOfDraggedImage.SetAsLastSibling();
        rowOfDraggedImage.SetAsLastSibling();
        HUDOfDraggedImage.SetAsLastSibling();

        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        List<RaycastResult> objectUnderMouse = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, objectUnderMouse);

        Debug.Log(objectUnderMouse.First().gameObject);


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        
        
        transform.localPosition = Vector3.zero;
        transform.parent.GetChild(1).localPosition = new Vector3(38f, -44f, 0f);
        transform.parent.GetChild(1).SetAsLastSibling();
        //transform.parent.GetChild(1).localPosition = Vector3.zero;


        //Image draggedImage = eventData.pointerDrag.transform.GetComponent<Image>();

        //draggedImage.raycastTarget = true;

    }
}
