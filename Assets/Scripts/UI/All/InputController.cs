using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public int low = 1;
    public InteractionController interactionController;
    public event Action<int> countChanged= delegate { };
    //public event Action<float> somethingDetected = delegate { };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //somethingDetected(1f);
            interactionController.InteractWithObject();
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            interactionController.InteractManuallyWithObject();
        } 

    }
}