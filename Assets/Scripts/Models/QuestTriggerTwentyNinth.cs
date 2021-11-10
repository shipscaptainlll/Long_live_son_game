using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerTwentyNinth : MonoBehaviour
{
    public event Action CharacterReachedRectangle = delegate { };
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (CharacterReachedRectangle != null)
        {
            CharacterReachedRectangle();
        }
    }
}
