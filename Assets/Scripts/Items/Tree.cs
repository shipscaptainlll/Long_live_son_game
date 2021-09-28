using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Tree";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public string Type
    {
        get
        {
            return "Tree";
        }
    }

    public int Cost
    {
        get
        {
            return 10;
        }
    }

    public void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    
}
