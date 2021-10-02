using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (OnClicked != null)
            {
                OnClicked();
                Debug.Log("First Controller");
            }
        }

       
    }
}
