using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manure : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Manure";
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
            return "Manure";
        }
    }
    public string Pickable
    {
        get
        {
            return "Yes";
        }
    }
    public int CountPerObject
    {
        get
        {
            return 2;
        }
    }
    public int Cost
    {
        get
        {
            return 50;
        }
    }

    public int Count { get; set; }
    public void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
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
