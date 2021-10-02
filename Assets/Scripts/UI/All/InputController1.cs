using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController1 : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (OnClicked != null)
            {
                OnClicked();
                Debug.Log("Second Controller");
            }
        }
    }
}
