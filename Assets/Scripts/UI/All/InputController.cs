using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public int low = 1;
    public InteractionController interactionController;
    public event Action<int> countChanged= delegate { };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            interactionController.InteractWithObject();
            
        }

    }
}
