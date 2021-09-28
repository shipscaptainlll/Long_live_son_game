using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Grass";
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
            return "Grass";
        }
    }

    public int Cost
    {
        get
        {
            return 1;
        }
    }

    public void OnDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.position = hit.point;
            transform.gameObject.SetActive(true);
        }

    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }
}
