using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElementDragger : MonoBehaviour
{

    private void Update()
    {
        
    }
    public List<RaycastResult> objectHit = new List<RaycastResult>();

    public const string OBJECT_DRAGGABLE = "IS_DRAGGABLE";

    private GameObject GetObjectUnderMouse()
    {
        
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, objectHit);

        if (objectHit.Count <= 0) return null;

        return objectHit.First().gameObject;
    }

    private Transform ObjectIsDraggable()
    {
        GameObject objectIsDraggable = GetObjectUnderMouse();

        if (objectIsDraggable != null && objectIsDraggable.tag == OBJECT_DRAGGABLE)
        {
            return objectIsDraggable.transform;
        }

        return null;

    }
}
